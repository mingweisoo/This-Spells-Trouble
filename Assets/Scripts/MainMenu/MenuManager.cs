using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // ScriptableObjects
    public IntArrVariable playersChars;
    public PlayersSpells playersSpells;
    public PlayersSpellLevels playersSpellLevels;
    public IntArrVariable playersGold;
    public IntArrVariable playersPoints;

    // GameObjects
    public Button[] buttons;
    public GameObject helpPopup;
    public GameObject[] helpPages;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onReadyButtonPlaySound;
    public GameEvent onArrowButtonPlaySound;
    public GameEvent onFlipPagePlaySound;
    public GameEvent onOpenHelpPlaySound;
    public GameEvent onCloseHelpPlaySound;

    // Game State
    int selectedButton = 0;
    bool helpShown = false;
    int pageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set frame rate to be 50 FPS
	    Application.targetFrameRate =  50;

        // Initialise: Close helpm, Select first button
        helpPopup.SetActive(false);
        buttons[0].Select();
        buttons[0].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        // Initialise values for all scriptable objects
        for (int playerID = 0; playerID < 4; playerID++) {
            playersChars.SetValue(playerID, -1);
            playersGold.SetValue(playerID, -1);
            playersPoints.SetValue(playerID, -1);
            for (int slot = 0; slot < 4; slot++) {
                playersSpells.SetSpell(playerID, slot, Spell.nullSpell);
            }
            int numSpells = playersSpells.GetNumSpells();
            for (int spellInt = 0; spellInt < numSpells; spellInt++) {
                playersSpellLevels.SetSpellLevel(playerID, (Spell) spellInt, -1);
            }
            // TODO: Maybe move currentRound and currentMap from CharSelectionManager into here?
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        GameObject playerObject = playerInput.gameObject;
        playerObject.GetComponent<MenuController>().menuManager = this;
    }

    public void previousButton() {
        if (helpShown) {
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
        if (helpShown) {
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
        if (helpShown) {
            return;
        }
        ExecuteEvents.Execute(buttons[selectedButton].gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        onReadyButtonPlaySound.Raise();
    }

    public void previousPage() {
        if (!helpShown) {
            return;
        }
        helpPages[pageIndex].SetActive(false);
        pageIndex -= 1;
        if (pageIndex < 0) {
            pageIndex += helpPages.Length;
        }
        helpPages[pageIndex].SetActive(true);
        onFlipPagePlaySound.Raise();
    }

    public void nextPage() {
        if (!helpShown) {
            return;
        }
        helpPages[pageIndex].SetActive(false);
        pageIndex += 1;
        if (pageIndex >= helpPages.Length) {
            pageIndex -= helpPages.Length;
        }
        helpPages[pageIndex].SetActive(true);
        onFlipPagePlaySound.Raise();
    }

    public void closeHelp() {
        if (!helpShown) {
            return;
        }
        helpPopup.SetActive(false);
        helpShown = false;
        onCloseHelpPlaySound.Raise();
    }

    public void PlayGame() {
        StartCoroutine(ChangeScene("CharSelectionScene"));
        Debug.Log("Loading Scene...");
    }

    public void HelpGame() {
        helpPopup.SetActive(true);
        helpShown = true;
        for (int page = 0; page < helpPages.Length; page++) {
            helpPages[page].SetActive(false);
        }
        helpPages[0].SetActive(true);
        pageIndex = 0;
        onOpenHelpPlaySound.Raise();
    }

    public void QuitGame() {
        Application.Quit();
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
