using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttackController : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onIceAttackCastPlaySound;
    public GameEvent onIceAttackHitPlaySound;

    // Components
    private Rigidbody2D iceBody;
    private BoxCollider2D attackCollider;

    // Physics
    public float aimAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID;
    public int spellLevel;
    float damage;

    public Vector2 knockbackPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        iceBody = GetComponent<Rigidbody2D>();
        attackCollider = GetComponent<BoxCollider2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.iceAttackDamageL2L3;
                break;
            case 3:
                damage = gameConstants.iceAttackDamageL2L3;
                break;
            default:
                damage = gameConstants.iceAttackDamageL1;
                break;
        }

        // Fireball movement
        //movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.fireballDistance, Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.fireballDistance);
        //fireballBody.AddForce(movement * gameConstants.fireballSpeed, ForceMode2D.Impulse);
        // Fireball instantiation
        iceBody.transform.position = new Vector3(transform.position.x - Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.iceAttackDistance, 
                                                        transform.position.y + Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.iceAttackDistance, 
                                                        transform.position.z);
        transform.Rotate(0f,0f,aimAngle+90f);
        StartCoroutine(WaitForAppearance());
        onIceAttackCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = iceBody.transform.position;
        Destroy(gameObject, gameConstants.iceAttackDestroyTime);
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onIceAttackHitPlaySound.Raise();
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.iceAttackForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                knockbackPosition = other.transform.position - transform.position;
                knockbackPosition.Normalize();
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackPosition * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onIceAttackHitPlaySound.Raise();
                //Destroy(gameObject);
            }
        }
        // // hits obstacles and spells destroys itself
        // if (other.gameObject.tag == "Obstacle") {
        //     Destroy(gameObject);
        // }
   
        }
    IEnumerator WaitForAppearance() {
        yield return new WaitForSeconds(0.5f);
        attackCollider.enabled = true;
        //transform.position = new Vector2 (iceBody.transform.position.x+0.000001f, iceBody.transform.position.y);
        transform.position = new Vector2 (transform.position.x+0.000001f, transform.position.y);
    }
}
