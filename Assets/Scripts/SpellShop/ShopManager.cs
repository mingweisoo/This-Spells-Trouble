using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public PlayerInputsArr playerInputsArr;
    public IntArrVariable playersChars;
    public IntVariable currentRound;
    public IntVariable currentMap;
    public IntArrVariable playersGold;
    public BoolArrVariable playersReady;

    // Components
    private AudioSource audioSource;

    // GameObjects
    public Text countdownText;
    public GameObject[] playersGameObjects;
    public GameObject[] characters;
    public GameObject[] P1SlotIcons;
    public GameObject[] P2SlotIcons;
    public GameObject[] P3SlotIcons;
    public GameObject[] P4SlotIcons;
    // public List<GameObject> spellInfos;
    public Text[] spellNameTexts;
    public Text[] spellCostTexts;
    public Text[] spellDescTexts;
    public Text[] spellUpgradeTexts;
    public Text[] goldTexts;
    public GameObject[] readyPanelObjects;
    public GameObject[] p1S1Levels;
    public GameObject[] p1S2Levels;
    public GameObject[] p1S3Levels;
    public GameObject[] p1S4Levels;
    public GameObject[] p2S1Levels;
    public GameObject[] p2S2Levels;
    public GameObject[] p2S3Levels;
    public GameObject[] p2S4Levels;
    public GameObject[] p3S1Levels;
    public GameObject[] p3S2Levels;
    public GameObject[] p3S3Levels;
    public GameObject[] p3S4Levels;
    public GameObject[] p4S1Levels;
    public GameObject[] p4S2Levels;
    public GameObject[] p4S3Levels;
    public GameObject[] p4S4Levels;

    // public List<GameObject> P1SkillStatus1;
    // public List<GameObject> P1SkillStatus2;
    // public List<GameObject> P1SkillStatus3;
    // public List<GameObject> P1SkillStatus4;
    // public List<GameObject> P2SkillStatus1;
    // public List<GameObject> P2SkillStatus2;
    // public List<GameObject> P2SkillStatus3;
    // public List<GameObject> P2SkillStatus4;
    // public List<GameObject> P3SkillStatus1;
    // public List<GameObject> P3SkillStatus2;
    // public List<GameObject> P3SkillStatus3;
    // public List<GameObject> P3SkillStatus4;
    // public List<GameObject> P4SkillStatus1;
    // public List<GameObject> P4SkillStatus2;
    // public List<GameObject> P4SkillStatus3;
    // public List<GameObject> P4SkillStatus4;

    // Animation
    public RuntimeAnimatorController[] animatorControllers;

    // Game State
    bool allReady = false;
    IEnumerator countdownCoroutine = null;

    void Awake() {
        // Set frame rate to be 50 FPS
	    Application.targetFrameRate =  50;
        
        // Get components
        audioSource = GetComponent<AudioSource>();

        currentRound.ApplyChange(1);
        for (int i = 0; i < playerInputsArr.GetLength(); i++) {
            if (playerInputsArr.GetValue(i) == null) {
                // Set unused UI objects inactive
                playersGameObjects[i].SetActive(false);
                continue;
            }
            // Render correct animator for characters
            Animator animator = characters[i].GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorControllers[playersChars.GetValue(i)];
            // Pass in GameObjects accordingly
            GameObject player = playerInputsArr.GetValue(i).gameObject;
            ShopController controller = player.GetComponent<ShopController>();
            // controller.spellInfo = spellInfos[i];
            controller.spellNameText = spellNameTexts[i];
            controller.spellCostText = spellCostTexts[i];
            controller.spellDescText = spellDescTexts[i];
            controller.spellUpgradeText = spellUpgradeTexts[i];
            controller.goldText = goldTexts[i];
            controller.readyPanelObject = readyPanelObjects[i];
            switch (i) {
                case 0:
                    controller.slotIcons = P1SlotIcons;
                    controller.s1Levels = p1S1Levels;
                    controller.s2Levels = p1S2Levels;
                    controller.s3Levels = p1S3Levels;
                    controller.s4Levels = p1S4Levels;
                    break;
                case 1:
                    controller.slotIcons = P2SlotIcons;
                    controller.s1Levels = p2S1Levels;
                    controller.s2Levels = p2S2Levels;
                    controller.s3Levels = p2S3Levels;
                    controller.s4Levels = p2S4Levels;
                    break;
                case 2:
                    controller.slotIcons = P3SlotIcons;
                    controller.s1Levels = p3S1Levels;
                    controller.s2Levels = p3S2Levels;
                    controller.s3Levels = p3S3Levels;
                    controller.s4Levels = p3S4Levels;
                    break;
                case 3:
                    controller.slotIcons = P4SlotIcons;
                    controller.s1Levels = p4S1Levels;
                    controller.s2Levels = p4S2Levels;
                    controller.s3Levels = p4S3Levels;
                    controller.s4Levels = p4S4Levels;
                    break;
            }
            // Disable previous script and activate current script and relevant components
            player.GetComponent<CharSelectionController>().enabled = false;
            player.GetComponent<BattleController>().enabled = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<ShopController>().enabled = true;
            // Change default actionmap
            playerInputsArr.GetValue(i).actions.FindActionMap("CharSelection").Disable();
            playerInputsArr.GetValue(i).actions.FindActionMap("Battle").Disable();
            playerInputsArr.GetValue(i).actions.FindActionMap("SpellShop").Enable();
        }
        // string toShow = "";
        // for (int i = 0; i < 4; i++) {
        //     toShow += "Player " + (i+1) + " gold: " + playersGold.GetValue(i) + "\n";
        // }
        // Debug.Log(toShow);

        // Initialise all values to false
        for (int i = 0; i < playersReady.GetLength(); i++) {
            playersReady.SetValue(i, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all ready, then countdown
        allReady = true;
        for (int i = 0; i < playerInputsArr.GetLength(); i++) {
            if (playerInputsArr.GetValue(i) != null) {
                if (playersReady.GetValue(i) == false) {
                    allReady = false;
                    break;
                }
            }
        }
        if (allReady) {
            // Start countdown, if not started
            if (countdownCoroutine == null) {
                countdownCoroutine = Countdown();
                StartCoroutine(countdownCoroutine);
                audioSource.Play();
            }
        } else {
            // Stop countdown, if running
            if (countdownCoroutine != null) {
                audioSource.Stop();
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
                countdownText.text = "Spell Shop";
            }
        }
    }

    private IEnumerator Countdown() {
        // for (int i = 0; i < gameConstants.shopCountdownTime; i++) {
        //     countdownText.text = "" + (gameConstants.shopCountdownTime-i);
        //     yield return new WaitForSeconds(1);
        // }
        for (int i = 0; i < gameConstants.countdownTime; i++) {
            countdownText.text = "" + (gameConstants.countdownTime-i);
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "Loading...";
        switch(currentRound.Value) {
            case 1:
                currentMap.SetValue(1);
                StartCoroutine(ChangeScene("BattleScene1"));
                break;
            case 2:
                currentMap.SetValue(2);
                StartCoroutine(ChangeScene("BattleScene2"));
                break;
            case 3:
                currentMap.SetValue(3);
                StartCoroutine(ChangeScene("BattleScene3"));
                break;
            case 4:
                currentMap.SetValue(4);
                StartCoroutine(ChangeScene("BattleScene4"));
                break;
            case 5:
                currentMap.SetValue(5);
                StartCoroutine(ChangeScene("BattleScene5"));
                break;
            default:
                Debug.Log("CurrentRound=" + currentRound.Value + ". Tie breaker stage.");
                // Randomly pick one of the battle scenes
                int randomInt = Random.Range(1, 6);
                switch (randomInt) {
                    case 1:
                        currentMap.SetValue(1);
                        StartCoroutine(ChangeScene("BattleScene1"));
                        break;
                    case 2:
                        currentMap.SetValue(2);
                        StartCoroutine(ChangeScene("BattleScene2"));
                        break;
                    case 3:
                        currentMap.SetValue(3);
                        StartCoroutine(ChangeScene("BattleScene3"));
                        break;
                    case 4:
                        currentMap.SetValue(4);
                        StartCoroutine(ChangeScene("BattleScene4"));
                        break;
                    case 5:
                        currentMap.SetValue(5);
                        StartCoroutine(ChangeScene("BattleScene5"));
                        break;
                    default:
                        Debug.Log("RandomInt=" + randomInt + ". Something is wrong.");
                        break;
                }
                break;
        }
    }

    private IEnumerator ChangeScene(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
