using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public PlayerInputsArr playerInputsArr;

    // Physics
    public float aimAngle;

    // Game state
    public int srcPlayerID;
    public int spellLevel;
    GameObject playerObject;
    float distance;

    // Sound Events
    public GameEvent onTeleportCastPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        // Get constants
        switch (spellLevel) {
            case 2:
                distance = gameConstants.teleportDistanceL2L3;
                break;
            case 3:
                distance = gameConstants.teleportDistanceL2L3;
                break;
            default:
                distance = gameConstants.teleportDistanceL1;
                break;
        }

        // Move player
        playerObject = playerInputsArr.GetValue(srcPlayerID).gameObject;
        playerObject.transform.position = new Vector3(transform.position.x - Mathf.Sin(Mathf.Deg2Rad * aimAngle) * distance, 
                                                        transform.position.y + Mathf.Cos(Mathf.Deg2Rad * aimAngle) * distance, 
                                                        transform.position.z);
        onTeleportCastPlaySound.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObject.transform.position;
        Destroy(gameObject, gameConstants.teleportDestroyTime);
    }
}

// //new teleport implementation 
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TeleportController : MonoBehaviour
// {
//     // ScriptableObjects
//     public GameConstants gameConstants;
//     public PlayerInputsArr playerInputsArr;

//     // Physics
//     public float aimAngle;

//     // Game state
//     public int srcPlayerID;
//     GameObject playerObject;

//     // Sound Events
//     public GameEvent onTeleportCastPlaySound;

//     public LayerMask collisionLayer; //User Layer which contains the Border, Obstacles Tiles

//     // Start is called before the first frame update
//     void Start()
//     {
//         playerObject = playerInputsArr.GetValue(srcPlayerID).gameObject; //Get the corresponding Player Object(Game Object) which is casting the teleport
//         //Get the teleport position which is where the aimAngle is plus the teleport distance
//         Vector2 teleportPosition = new Vector2(transform.position.x - Mathf.Sin(Mathf.Deg2Rad * aimAngle) * gameConstants.teleportDistance,
//                                                         transform.position.y + Mathf.Cos(Mathf.Deg2Rad * aimAngle) * gameConstants.teleportDistance); 

//         //get a directional vector of which direction the player is aiming 
//         Vector2 dir = teleportPosition - (Vector2)playerObject.transform.position;
//         // normalize directional vector to magnitude of 1
//         dir = dir.normalized;
//         //Shoot a raycast from the player to the teleport destination, that checks if a collider is in the way but only on layerMask which is of type collisionLayer. Returns the hit if it found one.
//         var hit = Physics2D.Raycast(playerObject.transform.position, dir, gameConstants.teleportDistance, collisionLayer); 
//         if (hit && hit.transform.gameObject.CompareTag("Obstacle"))
//         {
//             //if player hits an obstacle Teleport in front of the obstacle 
//             playerObject.transform.position = hit.point + dir * gameConstants.playerOffset;
//         }
//         else
//         {
//             //Teleport can happen as no obstacle exists
//             playerObject.transform.position = (Vector2)playerObject.transform.position + dir * gameConstants.teleportDistance;
//         }
//         onTeleportCastPlaySound.Raise();
//         transform.position = playerObject.transform.position;
//         Destroy(gameObject, gameConstants.teleportDestroyTime);
//     }
// }

