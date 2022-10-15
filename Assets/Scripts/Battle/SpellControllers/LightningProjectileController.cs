using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectileController : MonoBehaviour, SpellController
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Components
    private Rigidbody2D lightningProjectileBody;

    // Physics
    public float aimAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float damage;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onLightningCastPlaySound;
    public GameEvent onLightningHitPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        lightningProjectileBody = GetComponent<Rigidbody2D>();
        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.lightningProjectileDamageL2L3;
                break;
            case 3:
                damage = gameConstants.lightningProjectileDamageL2L3;
                break;
            default:
                damage = gameConstants.lightningProjectileDamageL1;
                break;
        }
        // LightningProjectile movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        lightningProjectileBody.AddForce(movement * gameConstants.lightningProjectileSpeed, ForceMode2D.Impulse);
        onLightningCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, gameConstants.lightningProjectileDestroyTime);
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onLightningHitPlaySound.Raise();
                    Destroy(gameObject);
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.lightningProjectileForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(movement * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onLightningHitPlaySound.Raise();
                Destroy(gameObject);
            }
    
        }
        // hits other spells or obstacles and spells destroys itself
        // if (other.gameObject.tag == "Spell" || other.gameObject.tag == "Obstacle") {
        //     Destroy(gameObject);
        // }
        if (other.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
    }
}
