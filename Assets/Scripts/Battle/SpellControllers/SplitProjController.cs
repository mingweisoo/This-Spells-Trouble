using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitProjController : MonoBehaviour, SpellController
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Components
    private Rigidbody2D splitterBody;

    // Physics
    public float startAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float damage;

    private IEnumerator splitCoroutine;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onSplitProjCastPlaySound;
    public GameEvent onSplitProjHitPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        splitterBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                damage = gameConstants.splitProjDamageL1L2;
                break;
            case 3:
                damage = gameConstants.splitProjDamageL3;
                break;
            default:
                damage = gameConstants.splitProjDamageL1L2;
                break;
        }

        // SplitProj movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * startAngle), Mathf.Cos(Mathf.Deg2Rad * startAngle));
        splitterBody.AddForce(movement * gameConstants.splitProjSpeed, ForceMode2D.Impulse);
        //this.transform.Rotate(0f,0f,startAngle);
        onSplitProjCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, gameConstants.splitProjDestroyTime);
    }
    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onSplitProjHitPlaySound.Raise();
                    Destroy(gameObject);
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.splitProjForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(movement * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onSplitProjHitPlaySound.Raise();
                Destroy(gameObject);
            }
        }
        // hits other spells and spells destroys itself
        // probably need to make the spells not collide with each other on instantation
        if (other.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
    }
}
