using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public IntArrVariable playersChars;
    public KnockbackArr playersKnockback;
    public PlayersSpells playersSpells;
    public PlayersSpellLevels playersSpellLevels;
    public BoolArrVariable playersAreAlive;
    public BoolVariable roundEnded;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onLavaPlaySound;
    public GameEvent onRegenPlaySound;
    public GameEvent onPlayerDeathPlaySound;

    // GameObjects
    MapManager mapManager;
    public GameObject mageObject;
    public GameObject aimObject;
    // public GameObject knockbackObject;
    public Text knockbackText;
    public Text playerText;
    public Image[] cooldownImages;
    public GameObject[] spellIcons;
    
    // Components
    private  Rigidbody2D rigidBody;
    Animator animator;
    SpriteRenderer knockbackSpriteRenderer;

    // Animation
    public RuntimeAnimatorController[] animatorControllers;
    public static readonly string[] idleDirections = {"Idle_N", "Idle_W", "Idle_S", "Idle_E"};
    public static readonly string[] walkDirections = {"Walk_N", "Walk_W", "Walk_S", "Walk_E"};
    public static readonly string[] attackDirections = {"Attack_N", "Attack_W", "Attack_S", "Attack_E"};
    public static readonly string[] hurtDirections = {"Hurt_N", "Hurt_W", "Hurt_S", "Hurt_E"};
    public static readonly string[] deathDirections = {"Death_N", "Death_W", "Death_S", "Death_E"};
    public static readonly string[] victoryDirections = {"Victory_N", "Victory_W", "Victory_S", "Victory_E"};
    int lastDirection = 0;
    bool castingSpell = false;
    IEnumerator spellAnimationCoroutine = null;
    bool hurting = false;   // This is for hurt by spell animation
    IEnumerator hurtAnimationCoroutine = null;

    // Physics
    Vector2 move;
    float moveAngle;
    Vector2 aim;
    float aimAngle;

    // Game State
    public int playerID;
    bool[] spellsReady = {true, true, true, true};
    float[] cooldownDurations = {-1, -1, -1, -1};
    float knockback;
    public bool onPlatform = true;
    bool isDead = false;
    public bool invulnerable = false;
    IEnumerator damageCoroutine = null;     // For LAVA only
    IEnumerator checkRegenCoroutine = null;
    IEnumerator regenCoroutine = null;

    // Others
    public SpellModel[] allSpellModels;
    public Texture emptySprite;
    
    // Controller functions
    private void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }

    private void OnAim(InputValue value) {
        aim = value.Get<Vector2>();
    }
    
    private void OnSpell1() {
        if (!isDead && !roundEnded.Value) {
            executeSpell(0);
        }
    }

    private void OnSpell2() {
        if (!isDead && !roundEnded.Value) {
            executeSpell(1);
        }
    }

    private void OnSpell3() {
        if (!isDead && !roundEnded.Value) {
            executeSpell(2);
        }
    }

    private void OnSpell4() {
        if (!isDead && !roundEnded.Value) {
            executeSpell(3);
        }
    }

    void Awake() {
        // Components
        rigidBody = GetComponent<Rigidbody2D>();
        animator = mageObject.GetComponent<Animator>();
    }

    void OnEnable() {
        refreshBattleController();
    }

    void refreshBattleController() {
        // GameObjects
        mapManager = FindObjectOfType<MapManager>();
        mageObject.SetActive(true);
        aimObject.SetActive(true);
        knockbackText.transform.gameObject.SetActive(true);
        // Initialise values
        isDead = false;
        invulnerable = false;
        playersKnockback.SetValue(playerID, 0);
        for (int i = 0; i < 4; i++) {
            cooldownImages[i].fillAmount = 0;
            spellIcons[i].GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
            switch(playersSpells.GetSpell(playerID, i)) {
                case Spell.fireball:
                    cooldownDurations[i] = gameConstants.fireballCooldown;
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.fireball].Icon;
                    break;
                case Spell.teleport:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.teleport) >= 3) {
                        cooldownDurations[i] = gameConstants.teleportCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.teleportCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.teleport].Icon;
                    break;
                case Spell.lightning:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.lightning) >= 3) {
                        cooldownDurations[i] = gameConstants.lightningProjectileCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.lightningProjectileCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.lightning].Icon;
                    break;
                case Spell.tornado:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.tornado) >= 3) {
                        cooldownDurations[i] = gameConstants.tornadoCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.tornadoCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.tornado].Icon;
                    break;
                case Spell.rush:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.rush) >= 3) {
                        cooldownDurations[i] = gameConstants.rushCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.rushCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.rush].Icon;
                    break;
                case Spell.arc:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.arc) >= 3) {
                        cooldownDurations[i] = gameConstants.arcCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.arcCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.arc].Icon;
                    break;
                case Spell.splitter:
                    cooldownDurations[i] = gameConstants.splitterCooldown;
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.splitter].Icon;
                    break;
                case Spell.boomerang:
                    cooldownDurations[i] = gameConstants.boomerangCooldown;
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.boomerang].Icon;
                    break;
                case Spell.laser:
                    cooldownDurations[i] = gameConstants.laserCooldown;
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.laser].Icon;
                    break;
                case Spell.cloud:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.cloud) >= 3) {
                        cooldownDurations[i] = gameConstants.cloudCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.cloudCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.cloud].Icon;
                    break;
                case Spell.minethrow:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.minethrow) >= 3) {
                        cooldownDurations[i] = gameConstants.mineThrowCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.mineThrowCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.minethrow].Icon;
                    break;
                case Spell.groundattack:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.groundattack) >= 3) {
                        cooldownDurations[i] = gameConstants.groundAttackCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.groundAttackCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.groundattack].Icon;
                    break;
                case Spell.iceattack:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.iceattack) >= 3) {
                        cooldownDurations[i] = gameConstants.iceAttackCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.iceAttackCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.iceattack].Icon;
                    break;
                case Spell.shockwave:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.shockwave) >= 3) {
                        cooldownDurations[i] = gameConstants.shockwaveCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.shockwaveCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.shockwave].Icon;
                    break;
                case Spell.wall:
                    if (playersSpellLevels.GetSpellLevel(playerID, Spell.wall) >= 3) {
                        cooldownDurations[i] = gameConstants.wallCooldownL3;
                    } else {
                        cooldownDurations[i] = gameConstants.wallCooldownL1L2;
                    }
                    spellIcons[i].GetComponent<RawImage>().texture = allSpellModels[(int) Spell.wall].Icon;
                    break;
                default:
                    spellIcons[i].GetComponent<RawImage>().texture = emptySprite;
                    break;
            }
        }
    }

    void onDisable() {
        mageObject.SetActive(true);
        aimObject.SetActive(true);
        knockbackText.transform.gameObject.SetActive(true);
    }

    void Start() {
        // Render the correct character sprite & animation
        animator.runtimeAnimatorController = animatorControllers[playersChars.GetValue(playerID)];
        // Render correct player text number and color
        playerText.text = "P" + (playerID + 1);
        switch (playerID) {
            case 0:
                playerText.color = new Color(0.96875f, 0.82422f, 0.47656f, 1);
                break;
            case 1:
                playerText.color = new Color(0.51563f, 0.87891f, 0.99216f, 1);
                break;
            case 2:
                playerText.color = new Color(0.99216f, 0.51563f, 0.91406f, 1);
                break;
            case 3:
                playerText.color = new Color(0.54297f, 0.99216f, 0.51563f, 1);
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        if (roundEnded.Value && !isDead) {
            rigidBody.velocity = new Vector3(0, 0, 0);
            // Stop all coroutine, if running
            if (damageCoroutine != null) {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            if (checkRegenCoroutine != null) {
                StopCoroutine(checkRegenCoroutine);
                checkRegenCoroutine = null;
            }
            if (regenCoroutine != null) {
                StopCoroutine(regenCoroutine);
                regenCoroutine = null;
            }
            // Victory animation
            animator.Play(victoryDirections[lastDirection]);
            return;
        }

        if (isDead) {
            rigidBody.velocity = new Vector3(0, 0, 0);
            return;
        }

        // Damage if outside platform
        onPlatform = !mapManager.GetTileDealsDamage(transform.position);
        if (!onPlatform) {
            // Start damage coroutine, if not started
            if (damageCoroutine == null) {
                damageCoroutine = Damage();
                StartCoroutine(damageCoroutine);
            }
        } else {
            // Stop damage coroutine, if running
            if (damageCoroutine != null) {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }

        // Death if knockback is 100 and not on platform
        knockback = playersKnockback.GetValue(playerID);
        if (knockback >= 100 && !onPlatform) {
            // Stop all coroutine, if running
            if (damageCoroutine != null) {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            if (checkRegenCoroutine != null) {
                StopCoroutine(checkRegenCoroutine);
                checkRegenCoroutine = null;
            }
            if (regenCoroutine != null) {
                StopCoroutine(regenCoroutine);
                regenCoroutine = null;
            }
            isDead = true;
            playersAreAlive.SetValue(playerID, false);
            animator.Play(deathDirections[lastDirection]);
            Debug.Log("Player " + (playerID+1) + " has died.");
            onPlayerDeathPlaySound.Raise();
        }

        // Regen
        if (!hurting && onPlatform && knockback != 0) {
            // Start checkregen coroutine, if check regen AND regen not running
            if (checkRegenCoroutine == null && regenCoroutine == null) {
                checkRegenCoroutine = CheckRegen();
                StartCoroutine(checkRegenCoroutine);
            }
        } else {
            // Stop checkregen coroutine, if running
            if (checkRegenCoroutine != null) {
                StopCoroutine(checkRegenCoroutine);
                checkRegenCoroutine = null;
            }
            // Stop regen coroutine, if running
            if (regenCoroutine != null) {
                StopCoroutine(regenCoroutine);
                regenCoroutine = null;
            }
        }

        // Idle/Walk Animation
        if (!castingSpell && !hurting && onPlatform) {
            string[] directionArray = null;
            if (move.x == 0 && move.y == 0) {
                directionArray = idleDirections;
            } else {
                directionArray = walkDirections;
                lastDirection = DirectionToIndex(new Vector2(move.x, move.y), 4);
            }
            animator.Play(directionArray[lastDirection]);
        }

        // Movement
        if (move.x != 0 && move.y != 0) {
            moveAngle = Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg;
            // Vector2 movement = new Vector2(move.x, move.y) * gameConstants.moveSpeed * Time.deltaTime;
            // transform.Translate(movement, Space.World);
            Vector2 movement = new Vector2(move.x, move.y) * gameConstants.playerSpeed;
            rigidBody.AddForce(new Vector2(move.x, move.y) * gameConstants.playerSpeed, ForceMode2D.Impulse);
        }

        // Aim
        if (aim.x != 0 && aim.y != 0) {
            aimAngle = Mathf.Atan2(-aim.x, aim.y) * Mathf.Rad2Deg;
            // Uncomment next 2 lines and comment 3rd line if u want a turn lag
            //Quaternion aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, aimRotation, turnSpeed * Time.time);
            aimObject.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        }

        // Knockback Text
        knockbackText.text = "" + knockback + "%";
        if (knockback == 0) {
            // white
            knockbackText.color = new Color(1, 1, 1, 1);
        }
        else if (knockback < 50) {
            // gold 
            knockbackText.color = new Color(1, 0.8f, 0, 1);
        } else if (knockback <= 99) {
            // orange
            knockbackText.color = new Color(1, 0.4f, 0, 1);
        } else {
            // red
            knockbackText.color = new Color(1, 0, 0, 1);
        }

        // Cooldowns
        if (!spellsReady[0]) {
            cooldownImages[0].fillAmount -= 1 / cooldownDurations[0] * Time.deltaTime;
        }
        if (!spellsReady[1]) {
            cooldownImages[1].fillAmount -= 1 / cooldownDurations[1] * Time.deltaTime;
        }
        if (!spellsReady[2]) {
            cooldownImages[2].fillAmount -= 1 / cooldownDurations[2] * Time.deltaTime;
        }
        if (!spellsReady[3]) {
            cooldownImages[3].fillAmount -= 1 / cooldownDurations[3] * Time.deltaTime;
        }
    }

    // Helper functions
    private void executeSpell(int slot) {
        if (!spellsReady[slot]) {
            return;
        }
        Spell spell = playersSpells.GetSpell(playerID, slot);
        if (spell == Spell.nullSpell) {
            return;
        }
        // Stop checkregen coroutine, if running
        if (checkRegenCoroutine != null) {
            StopCoroutine(checkRegenCoroutine);
            checkRegenCoroutine = null;
        }
        // Stop regen coroutine, if running
        if (regenCoroutine != null) {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }
        // Animation
        castingSpell = true;
        if (spellAnimationCoroutine != null) {
            StopCoroutine(spellAnimationCoroutine);
        }
        spellAnimationCoroutine = SpellAnimation(spell);
        StartCoroutine(spellAnimationCoroutine);
        // Cast spell
        switch (spell) {
            case Spell.fireball:
                CastFireball(slot);
                break;
            case Spell.teleport:
                CastTeleport(slot);
                break;
            case Spell.lightning:
                CastLightning(slot);
                break;
            case Spell.tornado:
                CastTornado(slot);
                break;
            case Spell.rush:
                CastRush(slot);
                break;
            case Spell.arc:
                CastArc(slot);
                break;
            case Spell.splitter:
                CastSplitter(slot);
                break;
            case Spell.boomerang:
                CastBoomerang(slot);
                break;
            case Spell.laser:
                CastLaser(slot);
                break;
            case Spell.cloud:
                CastCloud(slot);
                break;
            case Spell.minethrow:
                CastMineThrow(slot);
                break;
            case Spell.groundattack:
                CastGroundAttack(slot);
                break;
            case Spell.iceattack:
                CastIceAttack(slot);
                break;
            case Spell.shockwave:
                CastShockwave(slot);
                break;
            case Spell.wall:
                CastWall(slot);
                break;
            default:
                break;
        }
    }

    // Knockback stuff
    public IEnumerator Damage() {
        // Damage by lava ONLY
        while (true) {
            playersKnockback.ApplyChange(playerID, gameConstants.lavaDamage);
            onLavaPlaySound.Raise();
            // Casting spells and hurt by spells animation takes precedence over hurt by lava animation
            if (!castingSpell && !hurting) {
                if (move.x != 0 || move.y != 0) {
                    lastDirection = DirectionToIndex(new Vector2(move.x, move.y), 4);
                }
                animator.Play(hurtDirections[lastDirection]);
            }
            yield return new WaitForSeconds(gameConstants.lavaDamageInverval);
        }
    }

    public IEnumerator CheckRegen() {
        yield return new WaitForSeconds(gameConstants.regenWait);
        checkRegenCoroutine = null;
        regenCoroutine = Regen();
        StartCoroutine(regenCoroutine);
    }
    
    public IEnumerator Regen() {
        while (true) {
            // onRegenPlaySound.Raise();
            playersKnockback.ApplyChange(playerID, -1 * gameConstants.regenValue);
            yield return new WaitForSeconds(gameConstants.regenInterval);
        }
    }

    // Animation stuff
    int DirectionToIndex(Vector2 dir, int sliceCount) {
        // get the normalized direction
        Vector2 normDir = dir.normalized;
        // calculate how many degrees one slice is
        float step = 360f / sliceCount;
        // calculate how many degrees half a slice is
        // we need this to offset the pie, so that the North slice is aligned in the center
        float halfStep = step / 2;
        // get the angle from -180 to 180 of the direction vector relative to the Up vector
        // this will return the angle between dir and North
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        // add the halfslice offset
        angle += halfStep;
        // if angle is negative, add the wraparound
        if (angle < 0) {
            angle += 360;
        }
        // calculate the amount of steps required to reach this angle
        float stepCount = angle / step;
        // round it off to get the answer
        return Mathf.FloorToInt(stepCount);
    }

    IEnumerator SpellAnimation(Spell spell) {
        Vector2 aimVector = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        string clip;
        switch (spell) {
            case Spell.teleport:
            case Spell.rush:
                clip = attackDirections[lastDirection];
                break;
            default:
                int aimDirection = DirectionToIndex(aimVector, 4);
                clip = attackDirections[aimDirection];
                break;
        }
        animator.Play(clip);
        yield return new WaitForSeconds(gameConstants.spellAnimationDuration);
        castingSpell = false;
        spellAnimationCoroutine = null;
    }

    public void Hurt() {
        hurting = true;
        if (hurtAnimationCoroutine != null) {
            StopCoroutine(hurtAnimationCoroutine);
        }
        hurtAnimationCoroutine = HurtAnimation();
        StartCoroutine(hurtAnimationCoroutine);
    }

    IEnumerator HurtAnimation() {
        string clip = hurtDirections[lastDirection];
        animator.Play(clip);
        yield return new WaitForSeconds(gameConstants.hurtAnimationDuration);
        hurting = false;
        hurtAnimationCoroutine = null;
    }

    // Spell stuff
    IEnumerator SpellCooldown(int slot, float duration) {
        spellsReady[slot] = false;
        cooldownImages[slot].fillAmount = 1;
        spellIcons[slot].GetComponent<RawImage>().color = new Color(0.35156f, 0.35156f, 0.35156f, 1);
        yield return new WaitForSeconds(duration);
        spellsReady[slot] = true;
        spellIcons[slot].GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
    }

    IEnumerator Invulnerable(float duration) {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }

    void CastFireball(int slot) {
        GameObject fireballObject = Instantiate(allSpellModels[(int) Spell.fireball].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        fireballObject.GetComponent<FireballController>().srcPlayerID = playerID;
        fireballObject.GetComponent<FireballController>().aimAngle = aimAngle;
        fireballObject.GetComponent<FireballController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.fireball);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastTeleport(int slot) {
        GameObject teleportObject = Instantiate(allSpellModels[(int) Spell.teleport].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        teleportObject.GetComponent<TeleportController>().srcPlayerID = playerID;
        // teleportObject.GetComponent<TeleportController>().aimAngle = aimAngle;
        teleportObject.GetComponent<TeleportController>().aimAngle = moveAngle;
        teleportObject.GetComponent<TeleportController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.teleport);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastLightning(int slot) {
        GameObject lightningObject = Instantiate(allSpellModels[(int) Spell.lightning].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        lightningObject.GetComponent<LightningProjectileController>().srcPlayerID = playerID;
        lightningObject.GetComponent<LightningProjectileController>().aimAngle = aimAngle;
        lightningObject.GetComponent<LightningProjectileController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.lightning);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastTornado(int slot) {
        GameObject tornadoObject = Instantiate(allSpellModels[(int) Spell.tornado].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        tornadoObject.GetComponent<TornadoController>().srcPlayerID = playerID;
        tornadoObject.GetComponent<TornadoController>().aimAngle = aimAngle;
        tornadoObject.GetComponent<TornadoController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.tornado);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastRush(int slot) {
        GameObject rushObject = Instantiate(allSpellModels[(int) Spell.rush].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        rushObject.GetComponent<RushController>().srcPlayerID = playerID;
        // rushObject.GetComponent<RushController>().aimAngle = aimAngle;
        rushObject.GetComponent<RushController>().aimAngle = moveAngle;
        rushObject.GetComponent<RushController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.rush);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
        StartCoroutine(Invulnerable(gameConstants.rushDestroyTime));
    }

    void CastArc(int slot) {
        GameObject arcObject = Instantiate(allSpellModels[(int) Spell.arc].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        arcObject.GetComponent<ArcController>().srcPlayerID = playerID;
        arcObject.GetComponent<ArcController>().aimAngle = aimAngle;
        arcObject.GetComponent<ArcController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.arc);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastSplitter(int slot) {
        GameObject splitterObject = Instantiate(allSpellModels[(int) Spell.splitter].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        splitterObject.GetComponent<SplitterController>().srcPlayerID = playerID;
        splitterObject.GetComponent<SplitterController>().aimAngle = aimAngle;
        splitterObject.GetComponent<SplitterController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.splitter);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastBoomerang(int slot) {
        GameObject boomerangObject = Instantiate(allSpellModels[(int) Spell.boomerang].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        boomerangObject.GetComponent<BoomerangController>().srcPlayerID = playerID;
        boomerangObject.GetComponent<BoomerangController>().aimAngle = aimAngle;
        boomerangObject.GetComponent<BoomerangController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.boomerang);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

    void CastLaser(int slot) {
        GameObject laserObject = Instantiate(allSpellModels[(int) Spell.laser].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        laserObject.GetComponent<LaserController>().srcPlayerID = playerID;
        laserObject.GetComponent<LaserController>().aimAngle = aimAngle;
        laserObject.GetComponent<LaserController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.laser);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }
    void CastCloud(int slot) {
        GameObject cloudObject = Instantiate(allSpellModels[(int) Spell.cloud].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        cloudObject.GetComponent<CloudController>().srcPlayerID = playerID;
        cloudObject.GetComponent<CloudController>().aimAngle = aimAngle;
        cloudObject.GetComponent<CloudController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.cloud);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }
    void CastMineThrow(int slot) {
        GameObject mineObject = Instantiate(allSpellModels[(int) Spell.minethrow].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        mineObject.GetComponent<MineThrowController>().srcPlayerID = playerID;
        mineObject.GetComponent<MineThrowController>().aimAngle = aimAngle;
        mineObject.GetComponent<MineThrowController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.minethrow);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }
    void CastGroundAttack(int slot) {
        GameObject groundObject = Instantiate(allSpellModels[(int) Spell.groundattack].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        groundObject.GetComponent<GroundAttackController>().srcPlayerID = playerID;
        groundObject.GetComponent<GroundAttackController>().aimAngle = aimAngle;
        groundObject.GetComponent<GroundAttackController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.groundattack);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }
    void CastIceAttack(int slot) {
        GameObject iceObject = Instantiate(allSpellModels[(int) Spell.iceattack].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        iceObject.GetComponent<IceAttackController>().srcPlayerID = playerID;
        iceObject.GetComponent<IceAttackController>().aimAngle = aimAngle;
        iceObject.GetComponent<IceAttackController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.iceattack);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }
    void CastShockwave(int slot) {
        GameObject shockwaveObject = Instantiate(allSpellModels[(int) Spell.shockwave].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        shockwaveObject.GetComponent<ShockwaveController>().srcPlayerID = playerID;
        shockwaveObject.GetComponent<ShockwaveController>().aimAngle = aimAngle;
        shockwaveObject.GetComponent<ShockwaveController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.shockwave);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }
    void CastWall(int slot) {
        GameObject wallObject = Instantiate(allSpellModels[(int) Spell.wall].Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        wallObject.GetComponent<WallController>().srcPlayerID = playerID;
        wallObject.GetComponent<WallController>().aimAngle = aimAngle;
        wallObject.GetComponent<WallController>().spellLevel = playersSpellLevels.GetSpellLevel(playerID, Spell.wall);
        StartCoroutine(SpellCooldown(slot, cooldownDurations[slot]));
    }

}
