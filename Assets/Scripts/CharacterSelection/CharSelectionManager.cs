using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelectionManager : MonoBehaviour
{
    // UI References
    public Text countdownText;

    // ScriptableObjects
    public BoolArrVariable playersJoined;
    public BoolArrVariable playersReady;
    public GameConstants gameConstants;
    public IntVariable currentRound;
    public IntVariable currentMap;
    
    // Components
    private AudioSource audioSource;

    // Game State
    bool allReady = false;
    IEnumerator countdownCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        // Set frame rate to be 50 FPS
	    Application.targetFrameRate =  50;

        // Get components
        audioSource = GetComponent<AudioSource>();
        
        // Initialise all values to false
        for (int i = 0; i < playersJoined.GetLength(); i++) {
            playersJoined.SetValue(i, false);
        }
        for (int i = 0; i < playersReady.GetLength(); i++) {
            playersReady.SetValue(i, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // At least 2 players have joined
        if (playersJoined.GetNumTrue() < 2) {
            allReady = false;
            return;
        }
        // Check if all ready, then countdown
        allReady = true;
        for (int i = 0; i < playersJoined.GetLength(); i++) {
            if (playersJoined.GetValue(i) == true) {
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
                countdownText.text = "Character Selection";
            }
        }
    }

    private IEnumerator Countdown() {
        for (int i = 0; i < gameConstants.countdownTime; i++) {
            countdownText.text = "" + (gameConstants.countdownTime-i);
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "Loading...";
        currentRound.SetValue(0);
        currentMap.SetValue(0);
        StartCoroutine(ChangeScene("SpellShopScene"));
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
