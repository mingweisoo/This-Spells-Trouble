using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;
    public BoolArrVariable playersAreAlive;

    // Components
    private Rigidbody2D wallBody;

    // Physics
    public float aimAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID;
    public int spellLevel;
    float destroyTime;

    public Vector2 knockbackPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        wallBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                destroyTime = gameConstants.wallDestroyTimeL2L3;
                break;
            case 3:
                destroyTime = gameConstants.wallDestroyTimeL2L3;
                break;
            default:
                destroyTime = gameConstants.wallDestroyTimeL1;
                break;
        }

        // Wall position
        //movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.fireballDistance, Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.fireballDistance);
        //fireballBody.AddForce(movement * gameConstants.fireballSpeed, ForceMode2D.Impulse);
        // Fireball instantiation
        wallBody.transform.position = new Vector3(transform.position.x - Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.wallDistance, 
                                                        transform.position.y + Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.wallDistance, 
                                                        transform.position.z);
        //transform.Rotate(0f,0f,aimAngle);
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = wallBody.transform.position;

    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int dstPlayerID = other.gameObject.GetComponent<BattleController>().playerID;
            if (srcPlayerID != dstPlayerID) {
                if (!playersAreAlive.GetValue(dstPlayerID)) {
                    return;
                }
                float knockback = playersKnockback.GetValue(dstPlayerID);
                float forceMultiplier = gameConstants.wallForce * (gameConstants.knockbackInitial + gameConstants.knockbackMultiplier * Mathf.Log(knockback + 1));
                knockbackPosition = other.transform.position - transform.position;
                knockbackPosition.Normalize();
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackPosition * forceMultiplier, ForceMode2D.Impulse);
                //playersKnockback.ApplyChange(dstPlayerID, damage);
                //other.gameObject.GetComponent<BattleController>().Hurt();
                //Destroy(gameObject);
            }
        }
        // hits other spells or obstacles and spells destroys itself
        if (other.gameObject.tag == "Spell") {
            Destroy(other.gameObject);
        }
    }
}

