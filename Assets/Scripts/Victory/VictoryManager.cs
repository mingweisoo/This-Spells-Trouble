using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public IntArrVariable playersPoints;
    public IntArrVariable playersChars;

    // GameObjects
    public Button[] buttons;
    public GameObject[] playerGameObjects;
    public TextMeshProUGUI[] playerTexts;
    public TextMeshProUGUI[] playerRankText;
    public GameObject[] spriteObjects;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onReadyButtonPlaySound;
    public GameEvent onArrowButtonPlaySound;


    // Game State
    int selectedButton = 0;
    bool buttonsAreShown = false;

    // Animation
    public RuntimeAnimatorController[] animatorControllers;

    // Start is called before the first frame update
    void Start()
    {
        // Set frame rate to be 50 FPS
	    Application.targetFrameRate =  50;

        // Set unused gameobjects inactive based on number of players
        int[] points = {-1, -1, -1, -1};
        for (int i = 0; i < 4; i++) {
            points[i] = playersPoints.GetValue(i);
        }
        // Debug Here
        // int[] points = {1, 3, 2, -1};
        int numPlayers = 0;
        for (int i = 0; i < 4; i++) {
            if (points[i] != -1) {
                numPlayers += 1;
            }
        }
        switch (numPlayers) {
            case 2:
                playerGameObjects[2].SetActive(false);
                playerGameObjects[3].SetActive(false);
                break;
            case 3:
                playerGameObjects[3].SetActive(false);
                break;
        }

        // Calculate ranking in terms of playersRank and playersLinedUp
        int[] playersRank = {-1, -1, -1, -1};
        int[] pointsDescending = (int[]) points.Clone();
        Array.Sort(pointsDescending);
        Array.Reverse(pointsDescending);
        // Debug.Log(string.Join(", ", points));
        // Debug.Log(string.Join(", ", pointsDescending));
        int previousPoint = -1;
        for (int i = 0; i < 4; i++) {
            int rank = i+1;
            int currentPoint = pointsDescending[i];
            // Quit if there are no more players
            if (currentPoint == -1) {
                break;
            }
            // Skip this iteration because there is a tie, and the previous iteration would have taken care of the same rankings
            if (previousPoint == currentPoint) {
                continue;
            }
            for (int j = 0; j < 4; j++) {
                if (points[j] == currentPoint) {
                    playersRank[j] = rank;
                }
            }
            previousPoint = currentPoint;
        }
        // Debug.Log(string.Join(", ", playerRanking));
        int index = 0;
        int[] playersLinedUp = {-1, -1, -1, -1};
        for (int currentRank = 1; currentRank < 5; currentRank++) {
            for (int j = 0; j < 4; j++) {
                if (playersRank[j] == currentRank) {
                    playersLinedUp[index] = j;
                    index += 1;
                }
            }
        }
        // Debug.Log(string.Join(", ", playersLinedUp));

        // Render ranking
        for (int i = 0; i < 4; i++) {
            int playerID = playersLinedUp[i];
            if (playerID == -1) {
                break;
            }
            playerTexts[i].text = "P" + (playerID + 1);
            int rank = playersRank[playerID];
            switch (rank) {
                case 2:
                    playerRankText[i].text = "2nd";
                    break;
                case 3:
                    playerRankText[i].text = "3rd";
                    break;
                case 4:
                    playerRankText[i].text = "4th";
                    break;
            }
            
        }

        // Render correct animator
        int[] chosenChars = {-1, -1, -1, -1};
        for (int i = 0; i < 4; i++) {
            chosenChars[i] = playersChars.GetValue(i);
        }
        // Debug here
        // int[] chosenChars = {0, 1, 0, -1};
        for (int i = 0; i < 4; i++) {
            int playerID = playersLinedUp[i];
            if (playerID == -1) {
                break;
            }
            Animator animator = spriteObjects[i].GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorControllers[chosenChars[playerID]];
            if (i == 0) {
                animator.Play("Victory_End");
            }
        }
        
        // Only show buttons after a fixed period of time
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].transform.gameObject.SetActive(false);
        }
        StartCoroutine(ShowButtons());

        // set first button
        buttons[0].Select();
        buttons[0].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        GameObject playerObject = playerInput.gameObject;
        playerObject.GetComponent<VictoryController>().victoryManager = this;
    }

    public void previousButton() {
        if (!buttonsAreShown) {
            return;
        }
        buttons[selectedButton].transform.localScale = new Vector3(1, 1, 1);
        selectedButton -= 1;
        if (selectedButton < 0) {
            selectedButton += buttons.Length;
        }
        buttons[selectedButton].Select();
        buttons[selectedButton].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        onArrowButtonPlaySound.Raise();
    }

    public void nextButton() {
        if (!buttonsAreShown) {
            return;
        }
        buttons[selectedButton].transform.localScale = new Vector3(1, 1, 1);
        selectedButton += 1;
        if (selectedButton >= buttons.Length) {
            selectedButton -= buttons.Length;
        }
        buttons[selectedButton].Select();
        buttons[selectedButton].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        onArrowButtonPlaySound.Raise();
    }

    public void clickButton() {
        if (!buttonsAreShown) {
            return;
        }
        ExecuteEvents.Execute(buttons[selectedButton].gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        onReadyButtonPlaySound.Raise();
    }

    public void MainMenu() {
        StartCoroutine(ChangeScene("MainMenuScene"));
        // Debug.Log("Main Menu button clicked!");
    }

    IEnumerator ShowButtons() {
        yield return new WaitForSeconds(gameConstants.showButtonsDuration);
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].transform.gameObject.SetActive(true);
        }
        buttonsAreShown = true;
    }

    IEnumerator ChangeScene(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
