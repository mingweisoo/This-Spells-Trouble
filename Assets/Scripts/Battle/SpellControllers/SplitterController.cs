using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterController : MonoBehaviour, SpellController
{
    //Split Projectile
    public GameObject splitProj;
    
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Components
    private Rigidbody2D splitterBody;

    // Physics
    public float aimAngle;
    public Vector2 movement;
    public float splitAngle;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float damage;
    int numProjectiles;

    private IEnumerator splitCoroutine;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onSplitterCastPlaySound;
    public GameEvent onSplitterHitPlaySound;
    //public GameEvent onSplitProjCastPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        splitterBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                numProjectiles = gameConstants.splitterNumberL2L3;
                damage = gameConstants.splitterDamageL1L2;
                break;
            case 3:
                numProjectiles = gameConstants.splitterNumberL2L3;
                damage = gameConstants.splitterDamageL3;
                break;
            default:
                numProjectiles = gameConstants.splitterNumberL1;
                damage = gameConstants.splitterDamageL1L2;
                break;
        }

        // Splitter movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        splitterBody.AddForce(movement * gameConstants.splitterSpeed, ForceMode2D.Impulse);
        //this.transform.Rotate(0f,0f,aimAngle);
        onSplitterCastPlaySound.Raise();
        splitCoroutine = waitandSplit(gameConstants.splitterDestroyTime);
        StartCoroutine(splitCoroutine);
    }

    public IEnumerator waitandSplit(float waitTime){
        //Debug.Log("coroutine");
        yield return new WaitForSeconds(waitTime);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        //this.transform.Rotate(0f,0f,gameConstants.splitterStartAngle);
        splitAngle = aimAngle + gameConstants.splitterStartAngle;
        for (int i = 0; i < numProjectiles; i++){
            // Debug.Log("i = "+splitAngle);
            splitProj = Instantiate(splitProj, transform.position, transform.rotation);
            splitProj.GetComponent<SplitProjController>().srcPlayerID = this.srcPlayerID;
            splitProj.GetComponent<SplitProjController>().startAngle = splitAngle;
            splitProj.GetComponent<SplitProjController>().spellLevel = spellLevel;
            splitAngle += -gameConstants.splitterStartAngle*2/(numProjectiles-1);
            yield return null;
        //onSplitProjCastPlaySound.Raise();
        Destroy(gameObject, 1f);
        }
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                if (other.gameObject.GetComponent<BattleController>().invulnerable) {
                    onSplitterHitPlaySound.Raise();
                    Destroy(gameObject);
                    return;
                }
                StopCoroutine(splitCoroutine);
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.splitterForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(movement * forceMultiplier, ForceMode2D.Impulse);
                playersKnockback.ApplyChange(dstPlayerID, damage);
                other.gameObject.GetComponent<BattleController>().Hurt();
                onSplitterHitPlaySound.Raise();
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
