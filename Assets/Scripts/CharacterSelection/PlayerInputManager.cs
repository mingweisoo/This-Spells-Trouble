using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    // UI References
    public GameObject[] P1Characters;
    public GameObject[] P2Characters;
    public GameObject[] P3Characters;
    public GameObject[] P4Characters;
    public GameObject[] joinTextObjects;
    public GameObject[] readyPanelObjects;

    // ScriptableObjects
    public PlayerInputsArr playerInputsArr;
    public BoolArrVariable playersJoined;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        int playerID = -1;
        for (int i = 0; i < playersJoined.GetLength(); i++) {
            if (playersJoined.GetValue(i) == false) {
                playerID = i;
                playersJoined.SetValue(i, true);
                break;
            }
        }
        if (playerID == -1) {
            Debug.Log("No more player slots left.");
            return;
        }
        playerInputsArr.SetValue(playerID, playerInput);
        // Pass in GameObjects accordingly
        GameObject playerObject = playerInput.gameObject;
        playerObject.GetComponent<CharSelectionController>().playerID = playerID;
        playerObject.GetComponent<BattleController>().playerID = playerID;
        playerObject.GetComponent<ShopController>().playerID = playerID;
        switch (playerID) {
            case 0:
                playerObject.GetComponent<CharSelectionController>().characters = P1Characters;
                playerObject.GetComponent<CharSelectionController>().joinTextObject = joinTextObjects[0];
                playerObject.GetComponent<CharSelectionController>().readyPanelObject = readyPanelObjects[0];
                break;
            case 1:
                playerObject.GetComponent<CharSelectionController>().characters = P2Characters;
                playerObject.GetComponent<CharSelectionController>().joinTextObject = joinTextObjects[1];
                playerObject.GetComponent<CharSelectionController>().readyPanelObject = readyPanelObjects[1];
                break;
            case 2:
                playerObject.GetComponent<CharSelectionController>().characters = P3Characters;
                playerObject.GetComponent<CharSelectionController>().joinTextObject = joinTextObjects[2];
                playerObject.GetComponent<CharSelectionController>().readyPanelObject = readyPanelObjects[2];
                break;
            case 3:
                playerObject.GetComponent<CharSelectionController>().characters = P4Characters;
                playerObject.GetComponent<CharSelectionController>().joinTextObject = joinTextObjects[3];
                playerObject.GetComponent<CharSelectionController>().readyPanelObject = readyPanelObjects[3];
                break;
        }
    }
}
