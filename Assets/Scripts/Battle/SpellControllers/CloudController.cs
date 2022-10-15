using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour, SpellController
{
    // Start is called before the first frame update
    // ScriptableObjects
    public GameConstants gameConstants;
    // Components
    private Rigidbody2D cloudBody;

    // Physics
    public float aimAngle;
    public Vector2 movement;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    float destroyTime;

    //Game events
    public GameEvent onCloudCastPlaySound;
    // public GameEvent onCloudHitPlaySound;
    
    void Start()
    {
        // Get Components
        cloudBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                destroyTime = gameConstants.cloudDestroyTimeL2L3;
                break;
            case 3:
                destroyTime = gameConstants.cloudDestroyTimeL2L3;
                break;
            default:
                destroyTime = gameConstants.cloudDestroyTimeL1;
                break;
        }

        // Movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        cloudBody.AddForce(movement * gameConstants.cloudSpeed, ForceMode2D.Impulse);
        onCloudCastPlaySound.Raise();
        // Rotate projectile accordingly top aim angle
        transform.Rotate(0f,0f,aimAngle);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Spell") {
            int spellPlayerID = other.gameObject.GetComponent<SpellController>().srcPlayerID;
            if (spellPlayerID != srcPlayerID) {
                // onCloudHitPlaySound.Raise();
                Destroy(other.gameObject);
            }
        }
    }
}
