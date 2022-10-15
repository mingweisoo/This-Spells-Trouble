using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour, SpellController
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Components
    private Rigidbody2D laserBody;

    // Physics
    public float aimAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float damage;
    float force;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onLaserCastPlaySound;
    public GameEvent onLaserHitPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        laserBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.laserDamageL2L3;
                force = gameConstants.laserForceL1L2;
                break;
            case 3:
                damage = gameConstants.laserDamageL2L3;
                force = gameConstants.laserForceL3;
                break;
            default:
                damage = gameConstants.laserDamageL1;
                force = gameConstants.laserForceL1L2;
                break;
        }

        // Laser movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        laserBody.AddForce(movement * gameConstants.laserSpeed, ForceMode2D.Impulse);
        transform.Rotate(0f,0f,aimAngle);
        onLaserCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, gameConstants.laserDestroyTime);
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onLaserHitPlaySound.Raise();
                    Destroy(gameObject);
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = force * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(movement * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onLaserHitPlaySound.Raise();
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
