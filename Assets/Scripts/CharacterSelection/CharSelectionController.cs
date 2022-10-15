using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectionController : MonoBehaviour
{
    // ScriptableObjects
    public BoolArrVariable playersReady;
    public IntArrVariable playersChars;
    public PlayersSpells playersSpells;
    public PlayersSpellLevels playersSpellLevels;
    public IntArrVariable playersGold;
    public IntArrVariable playersPoints;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onReadyButtonPlaySound;
    public GameEvent onJoinButtonPlaySound;
    public GameEvent onArrowButtonPlaySound;

    // Game State
    public int playerID;
    public GameObject[] characters;
    // public Text text;
    public GameObject joinTextObject;
    public GameObject readyPanelObject;
    int selectedChar = 0;
    bool ready = false;
    

    private void OnNextCharacter() {
        if (ready) {
            return;
        }
        characters[selectedChar].SetActive(false);
        selectedChar = (selectedChar + 1) % characters.Length;
        characters[selectedChar].SetActive(true);
        playersChars.SetValue(playerID, selectedChar);
        onArrowButtonPlaySound.Raise();
    }

    private void OnPreviousCharacter() {
        if (ready) {
            return;
        }
        characters[selectedChar].SetActive(false);
        selectedChar-= 1;
        if (selectedChar < 0){
            selectedChar += characters.Length;
        };
        characters[selectedChar].SetActive(true);
        playersChars.SetValue(playerID, selectedChar);
        onArrowButtonPlaySound.Raise();
    }

    private void OnReady() {
        ready = !ready;
        playersReady.SetValue(playerID, ready);
        if (ready) {
            readyPanelObject.SetActive(true);
            onReadyButtonPlaySound.Raise();
        } else {
            readyPanelObject.SetActive(false);
        }
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        joinTextObject.SetActive(false);
        characters[selectedChar].SetActive(true);
        // Initialise some values
        playersChars.SetValue(playerID, 0);
        playersGold.SetValue(playerID, 0);
        playersPoints.SetValue(playerID, 0);
        playersSpells.SetSpell(playerID, 1, Spell.fireball);
        int numSpells = playersSpells.GetNumSpells();
        for (int spellInt = 0; spellInt < numSpells; spellInt++) {
            if ((Spell) spellInt == Spell.fireball) {
                playersSpellLevels.SetSpellLevel(playerID, (Spell) spellInt, 1);
            } else {
                playersSpellLevels.SetSpellLevel(playerID, (Spell) spellInt, 0);
            }
        }
        onJoinButtonPlaySound.Raise();
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }
}
