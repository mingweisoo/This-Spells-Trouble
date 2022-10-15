using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{
    // ScriptableObjects
    public BoolArrVariable playersAreAlive;
    public BoolVariable roundEnded;
    public IntArrVariable playersPoints;
    public IntVariable currentRound;
    public PlayerInputsArr playerInputsArr;

    // GameObjects
    public GameObject victoryPanel;
    public Text victoryText;

    // Game state
    bool ended = false;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onVictoryPlaySound;

    // Start is called before the first frame update
    void Awake()
    {
        roundEnded.SetValue(false);
        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ended) {
            if (playersAreAlive.GetNumTrue() == 1) {
                int winnerID = -1;
                for (int i = 0; i < playersAreAlive.GetLength(); i++) {
                    if (playersAreAlive.GetValue(i)) {
                        winnerID = i;
                        break;
                    }
                }
                ended = true;
                roundEnded.SetValue(true);
                playersPoints.SetValue(winnerID, playersPoints.GetValue(winnerID) + 1);
                StartCoroutine(Victory());
            } else if (playersAreAlive.GetNumTrue() == 0) {
                // No one is awarded points
                ended = true;
                roundEnded.SetValue(true);
                StartCoroutine(Victory());
            }
        }
    }

    private IEnumerator Victory() {
        onVictoryPlaySound.Raise();
        string toShow = "";
        for (int i = 0; i < 4; i++) {
            if (playersPoints.GetValue(i) == -1) {
                break;
            }
            if (i != 0) {
                toShow += "\n";
            }
            toShow += "Player " + (i+1) + " score: " + playersPoints.GetValue(i);
        }
        victoryPanel.SetActive(true);
        victoryText.text = toShow;
        yield return new WaitForSeconds(10);
        // if (currentRound.Value < 1) {
        if (currentRound.Value < 5) {
            StartCoroutine(ChangeScene("SpellShopScene"));
        } else {
            // Check if there is a tie at the 1st place
            int maxPoint = 0;
            int numWinners = 0;
            for (int i = 0; i < 4; i++) {
                if (playersPoints.GetValue(i) > maxPoint) {
                    maxPoint = playersPoints.GetValue(i);
                }
            }
            for (int i = 0; i < 4; i++) {
                if (playersPoints.GetValue(i) == maxPoint) {
                    numWinners += 1;
                }
            }
            if (numWinners > 1) {
                // Tie breaker stage
                StartCoroutine(ChangeScene("SpellShopScene"));
            }
            else {
                // Destroy all playerObjects and move to victory scene
                for (int i = 0; i < 4; i++) {
                    if (playerInputsArr.GetValue(i) != null) {
                        Destroy(playerInputsArr.GetValue(i).gameObject);
                        playerInputsArr.SetValue(i, null);
                    }
                }
                StartCoroutine(ChangeScene("VictoryScene"));
            }
        }
    }

    private IEnumerator ChangeScene(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
