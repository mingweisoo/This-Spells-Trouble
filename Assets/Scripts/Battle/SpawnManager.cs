using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    // ScriptableObjects
    public GameConstants gameConstants;
    public PlayerInputsArr playerInputsArr;
    public BoolArrVariable playersAreAlive;
    public IntArrVariable playersChars;
    public IntVariable currentMap;

    // GameObject References
    public Texture[] charIcons;
    public Image[] topLeftCooldownImages;
    public Image[] topRightCooldownImages;
    public Image[] bottomLeftCooldownImages;
    public Image[] bottomRightCooldownImages;
    public GameObject[] topLeftSpellIcons;
    public GameObject[] topRightSpellIcons;
    public GameObject[] bottomLeftSpellIcons;
    public GameObject[] bottomRightSpellIcons;
    public GameObject[] playersObjects;
    public GameObject[] playersCharIcons;

    // Game State
    int numPlayers;
    GameObject[] mages = {null, null, null, null};

    void Awake() {
        // Set frame rate to be 50 FPS
	    Application.targetFrameRate =  50;

        int currIndex = 0;
        for (int i = 0; i < playerInputsArr.GetLength(); i++) {
            if (playerInputsArr.GetValue(i) != null) {
                playersAreAlive.SetValue(i, true);
                // Assign mages references
                mages[currIndex] = playerInputsArr.GetValue(i).gameObject;
                // Change default actionmap to gameplay
                playerInputsArr.GetValue(i).actions.FindActionMap("CharSelection").Disable();
                playerInputsArr.GetValue(i).actions.FindActionMap("SpellShop").Disable();
                playerInputsArr.GetValue(i).actions.FindActionMap("Battle").Enable();
                currIndex += 1;
            } else {
                playersAreAlive.SetValue(i, false);
            }
        }

        // Assign mages' positions accordingly
        numPlayers = playerInputsArr.GetNumPlayers();
        switch (numPlayers) {
            case 2:
                // Set used stuff active and unused stuff inactive
                playersObjects[0].SetActive(true);
                playersObjects[1].SetActive(true);
                playersObjects[2].SetActive(false);
                playersObjects[3].SetActive(false);
                playersCharIcons[0].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(0)];
                playersCharIcons[1].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(1)];
                mages[0].GetComponent<BattleController>().cooldownImages = topLeftCooldownImages;
                mages[1].GetComponent<BattleController>().cooldownImages = topRightCooldownImages;
                mages[0].GetComponent<BattleController>().spellIcons = topLeftSpellIcons;
                mages[1].GetComponent<BattleController>().spellIcons = topRightSpellIcons;
                switch (currentMap.Value) {
                    case 2:
                        mages[0].transform.position = gameConstants.Map2P4Position;
                        mages[1].transform.position = gameConstants.Map2P2Position;
                        break;
                    default:
                        mages[0].transform.position = gameConstants.topLeftPosition;
                        mages[1].transform.position = gameConstants.topRightPosition;
                        break;
                }
                break;
            case 3:
                // Set used stuff active and unused stuff inactive
                playersObjects[0].SetActive(true);
                playersObjects[1].SetActive(true);
                playersObjects[2].SetActive(true);
                playersObjects[3].SetActive(false);
                playersCharIcons[0].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(0)];
                playersCharIcons[1].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(1)];
                playersCharIcons[2].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(2)];
                mages[0].GetComponent<BattleController>().cooldownImages = topLeftCooldownImages;
                mages[1].GetComponent<BattleController>().cooldownImages = topRightCooldownImages;
                mages[2].GetComponent<BattleController>().cooldownImages = bottomLeftCooldownImages;
                mages[0].GetComponent<BattleController>().spellIcons = topLeftSpellIcons;
                mages[1].GetComponent<BattleController>().spellIcons = topRightSpellIcons;
                mages[2].GetComponent<BattleController>().spellIcons = bottomLeftSpellIcons;
                switch (currentMap.Value) {
                    case 2:
                        mages[0].transform.position = gameConstants.Map2P1Position;
                        mages[1].transform.position = gameConstants.Map2P2Position;
                        mages[2].transform.position = gameConstants.Map2P3Position;
                        break;
                    default:
                        mages[0].transform.position = gameConstants.topLeftPosition;
                        mages[1].transform.position = gameConstants.topRightPosition;
                        mages[2].transform.position = gameConstants.bottomLeftPosition;
                        break;
                }
                break;
            case 4:
                // Set used stuff active and unused stuff inactive
                playersObjects[0].SetActive(true);
                playersObjects[1].SetActive(true);
                playersObjects[2].SetActive(true);
                playersObjects[3].SetActive(true);
                playersCharIcons[0].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(0)];
                playersCharIcons[1].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(1)];
                playersCharIcons[2].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(2)];
                playersCharIcons[3].GetComponent<RawImage>().texture = charIcons[playersChars.GetValue(3)];
                mages[0].GetComponent<BattleController>().cooldownImages = topLeftCooldownImages;
                mages[1].GetComponent<BattleController>().cooldownImages = topRightCooldownImages;
                mages[2].GetComponent<BattleController>().cooldownImages = bottomLeftCooldownImages;
                mages[3].GetComponent<BattleController>().cooldownImages = bottomRightCooldownImages;
                mages[0].GetComponent<BattleController>().spellIcons = topLeftSpellIcons;
                mages[1].GetComponent<BattleController>().spellIcons = topRightSpellIcons;
                mages[2].GetComponent<BattleController>().spellIcons = bottomLeftSpellIcons;
                mages[3].GetComponent<BattleController>().spellIcons = bottomRightSpellIcons;
                switch (currentMap.Value) {
                    case 2:
                        mages[0].transform.position = gameConstants.Map2P1Position;
                        mages[1].transform.position = gameConstants.Map2P2Position;
                        mages[2].transform.position = gameConstants.Map2P3Position;
                        mages[3].transform.position = gameConstants.Map2P4Position;
                        break;
                    default:
                        mages[0].transform.position = gameConstants.topLeftPosition;
                        mages[1].transform.position = gameConstants.topRightPosition;
                        mages[2].transform.position = gameConstants.bottomLeftPosition;
                        mages[3].transform.position = gameConstants.bottomRightPosition;
                        break;
                }
                break;
        }
        for (int i = 0; i < 4; i++) {
            if (mages[i] == null) {
                continue;
            }
            // Disable previous script and activate current script and relevant components
            mages[i].GetComponent<CharSelectionController>().enabled = false;
            mages[i].GetComponent<ShopController>().enabled = false;
            mages[i].GetComponent<BattleController>().enabled = true;
            mages[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
