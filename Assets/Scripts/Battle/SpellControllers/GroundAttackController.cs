using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackController : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onGroundAttackCastPlaySound;
    public GameEvent onGroundAttackHitPlaySound;

    // Components
    private Rigidbody2D infernalBody;
    private CircleCollider2D attackCollider;

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
        infernalBody = GetComponent<Rigidbody2D>();
        attackCollider = GetComponent<CircleCollider2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.groundAttackDamageL2L3;
                break;
            case 3:
                damage = gameConstants.groundAttackDamageL2L3;
                break;
            default:
                damage = gameConstants.groundAttackDamageL1;
                break;
        }

        // Fireball movement
        //movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.fireballDistance, Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.fireballDistance);
        //fireballBody.AddForce(movement * gameConstants.fireballSpeed, ForceMode2D.Impulse);
        // Fireball instantiation
        infernalBody.transform.position = new Vector3(transform.position.x - Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.groundAttackDistance, 
                                                        transform.position.y + Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.groundAttackDistance, 
                                                        transform.position.z);
        StartCoroutine(WaitForAppearance());
        onGroundAttackCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = infernalBody.transform.position;
        Destroy(gameObject, gameConstants.groundAttackDestroyTime);
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                // Debug.Log("Collided with other player!");
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onGroundAttackHitPlaySound.Raise();
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.groundAttackForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                knockbackPosition = other.transform.position - transform.position;
                knockbackPosition.Normalize();
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackPosition * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onGroundAttackHitPlaySound.Raise();
                //Destroy(gameObject);
            }
        }
    //     // hits other spells or obstacles and spells destroys itself
    //     if (other.gameObject.tag == "Spell" || other.gameObject.tag == "Obstacle") {
    //         Destroy(gameObject);
    //     }
    }

    IEnumerator WaitForAppearance() {
        yield return new WaitForSeconds(1f);
        attackCollider.enabled = true;
        transform.position = new Vector2 (transform.position.x+0.000001f, transform.position.y);
    }
}
