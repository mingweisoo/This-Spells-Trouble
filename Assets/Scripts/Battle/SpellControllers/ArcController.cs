using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jo's first spell, heavily based on ly's fireball :)

public class ArcController : MonoBehaviour, SpellController
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Components
    private Rigidbody2D arcBody;

    // Physics
    public float aimAngle;
    public Vector2 forwardMovement;
    public Vector2 currentDirection;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float damage;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onArcCastPlaySound;
    public GameEvent onArcHitPlaySound;
    void Start()
    {
        // Get components
        arcBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.arcDamageL2L3;
                break;
            case 3:
                damage = gameConstants.arcDamageL2L3;
                break;
            default:
                damage = gameConstants.arcDamageL1;
                break;
        }

        // Arc movement
        forwardMovement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * (aimAngle+gameConstants.arcAngle)), Mathf.Cos(Mathf.Deg2Rad * (aimAngle+gameConstants.arcAngle)));
        arcBody.AddForce(forwardMovement * gameConstants.arcForwardSpeed, ForceMode2D.Impulse);
        this.transform.Rotate(0f,0f,aimAngle+gameConstants.arcAngle);
        arcBody.angularVelocity = -gameConstants.arcAngle*2/gameConstants.arcDestroyTime;
        // Debug.Log("angular velocity is "+ arcBody.angularVelocity);
        onArcCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection = transform.up; 
        arcBody.velocity = currentDirection * arcBody.velocity.magnitude;
        forwardMovement = arcBody.velocity;
        forwardMovement = forwardMovement.normalized;
        Destroy(gameObject, gameConstants.arcDestroyTime);
        //Debug.Log("arcBody velocity is " + arcBody.velocity);
    }


    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onArcHitPlaySound.Raise();
                    Destroy(gameObject);
                    return;
                }
                // TO-DO: the forwardmovement might be wrong but I can't math now - Jo
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.arcForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(forwardMovement * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onArcHitPlaySound.Raise();
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
