using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour, SpellController
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onShockwaveCastPlaySound;
    public GameEvent onShockwaveHitPlaySound;

    // Components
    private Rigidbody2D shockwaveBody;
    //private ParticleSystem ps;

    // Physics
    public float aimAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        shockwaveBody = GetComponent<Rigidbody2D>();
        //ps = GetComponent<ParticleSystem>();

        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.shockwaveDamageL2L3;
                break;
            case 3:
                damage = gameConstants.shockwaveDamageL2L3;
                break;
            default:
                damage = gameConstants.shockwaveDamageL1;
                break;
        }

        // Shockwave movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        shockwaveBody.AddForce(movement * gameConstants.shockwaveSpeed, ForceMode2D.Impulse);
        transform.Rotate(0f,0f,aimAngle+90f);
        //var main = ps.main;
        //main.startRotation = aimAngle;
        onShockwaveCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, gameConstants.shockwaveDestroyTime);
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onShockwaveHitPlaySound.Raise();
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.shockwaveForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(movement * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                // it doesn't destroy itself upon collision with a player but instead hits in a straight line
                onShockwaveHitPlaySound.Raise();
                //Destroy(gameObject);
            }
        }
        // hits obstacles and spells destroys itself
        if (other.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Spell") {
            int spellPlayerID = other.gameObject.GetComponent<SpellController>().srcPlayerID;
            if (spellPlayerID != srcPlayerID) {
                Destroy(other.gameObject);
            }
        }
    }
}
