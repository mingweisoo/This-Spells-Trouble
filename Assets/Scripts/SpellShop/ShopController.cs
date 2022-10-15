using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    // ScriptableObjects
    public PlayersSpells playersSpells;
    public PlayersSpellLevels playersSpellLevels;
    public IntArrVariable playersGold;
    public GameConstants gameConstants;
    public BoolArrVariable playersReady;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onArrowButtonPlaySound;
    public GameEvent onBuySpellPlaySound;
    public GameEvent onSellSpellPlaySound;
    public GameEvent onLockSlotPlaySound;
    public GameEvent onUnlockSlotPlaySound;
    public GameEvent onNotAllowedPlaySound;
    public GameEvent onReadyButtonPlaySound;

    // GameObjects
    public SpellModel[] allSpellModels;
    public Texture emptyIcon;
    public GameObject[] slotIcons;
    public Text spellNameText;
    public Text spellCostText;
    public Text spellDescText;
    public Text spellUpgradeText;
    public Text goldText;
    public GameObject readyPanelObject;
    public GameObject[] s1Levels;
    public GameObject[] s2Levels;
    public GameObject[] s3Levels;
    public GameObject[] s4Levels;

    // Game State
    public int playerID;
    private int selectedSlot = 0;
    private int selectedSpellInt = -1;  // this is w.r.t. index in either offensiveSpellModels or defensiveSpellModels
    private Spell selectedSpell = Spell.nullSpell;
    private bool[] slotTiedToSpell = {false, false, false, false};
    private List<SpellModel> offensiveSpellModels;
    private List<SpellModel> defensiveSpellModels;
    private int goldAmount;
    private bool slotIsLocked = false;
    bool ready = false;

    void OnLeftButton() {
        if (ready) {
            return;
        }
        if (!slotIsLocked) {
            // SELECT SLOT ON THE LEFT
            // Do nothing if a left slot is already selected
            if (selectedSlot == 0 || selectedSlot == 2) {
                return;
            }
            // Zoom and render the correct spell icon
            unrenderAndUnzoom(selectedSlot);
            selectedSlot -= 1;
            renderAndZoom(selectedSlot);
            onArrowButtonPlaySound.Raise();
        } else {
            // PREVIOUS SPELL
            // Do nothing if this is fireball slot OR if a spell is bought and tied to this slot
            if (selectedSlot == 1 || slotTiedToSpell[selectedSlot]) {
                return;
            }
            // Otherwise, render the correct offensive/defensive spell
            if (selectedSpellInt != -1) {
                selectedSpellInt -= 1;
            }
            if (selectedSlot == 0) {
                // Defensive spell slot
                if (selectedSpellInt < 0) {
                    selectedSpellInt += defensiveSpellModels.Count;
                }
                Spell spell = defensiveSpellModels[selectedSpellInt].Spell;
                renderSpell(spell, selectedSlot);
            } else {
                // Offensive spell slot
                if (selectedSpellInt < 0) {
                    selectedSpellInt += offensiveSpellModels.Count;
                }
                Spell spell = offensiveSpellModels[selectedSpellInt].Spell;
                // CHECK IF SPELL IS ALREADY BOUGHT. IF SO, SKIP THIS SPELL
                bool bought = false;
                for (int i = 1; i < 4; i++) {
                    if (spell == playersSpells.GetSpell(playerID, i) && spell != Spell.nullSpell) {
                        bought = true;
                        break;
                    }
                }
                if (bought) {
                    OnLeftButton();
                    return;
                }
                renderSpell(spell, selectedSlot);
            }
            onLockSlotPlaySound.Raise();
        }
    }

    void OnRightButton() {
        if (ready) {
            return;
        }
        if (!slotIsLocked) {
            // SELECT SLOT ON THE RIGHT
            // Do nothing if a right slot is already selected
            if (selectedSlot == 1 || selectedSlot == 3) {
                return;
            }
            // Zoom and render the correct spell icon
            unrenderAndUnzoom(selectedSlot);
            selectedSlot += 1;
            renderAndZoom(selectedSlot);
            onArrowButtonPlaySound.Raise();
        } else {
            // Next spell
            // Do nothing if this is fireball slot OR if a spell is bought for this slot
            if (selectedSlot == 1 || slotTiedToSpell[selectedSlot]) {
                return;
            }
            // Otherwise, render the correct offensive/defensive spell
            selectedSpellInt += 1;
            if (selectedSlot == 0) {
                // Defensive spell slot
                if (selectedSpellInt >= defensiveSpellModels.Count) {
                    selectedSpellInt -= defensiveSpellModels.Count;
                }
                Spell spell = defensiveSpellModels[selectedSpellInt].Spell;
                renderSpell(spell, selectedSlot);
            } else {
                // Offensive spell slot
                if (selectedSpellInt >= offensiveSpellModels.Count) {
                    selectedSpellInt -= offensiveSpellModels.Count;
                }
                Spell spell = offensiveSpellModels[selectedSpellInt].Spell;
                // CHECK IF SPELL IS ALREADY BOUGHT. IF SO, SKIP THIS SPELL
                bool bought = false;
                for (int i = 1; i < 4; i++) {
                    if (spell == playersSpells.GetSpell(playerID, i) && spell != Spell.nullSpell) {
                        bought = true;
                        break;
                    }
                }
                if (bought) {
                    OnRightButton();
                    return;
                }
                renderSpell(spell, selectedSlot);
            }
            onLockSlotPlaySound.Raise();
        }
    }

    void OnUpButton() {
        if (ready) {
            return;
        }
        if (slotIsLocked) {
            return;
        }
        // SELECT SLOT ABOVE
        // Do nothing if a slot at the top is already selected
        if (selectedSlot == 0 || selectedSlot == 1) {
            return;
        }
        // Zoom and render the correct spell icon
        unrenderAndUnzoom(selectedSlot);
        selectedSlot -= 2;
        renderAndZoom(selectedSlot);
        onArrowButtonPlaySound.Raise();
    }

    void OnDownButton() {
        if (ready) {
            return;
        }
        if (slotIsLocked) {
            return;
        }
        // SELECT SLOT BELOW
        // Do nothing if a slot at the bottom is already selected
        if (selectedSlot == 2 || selectedSlot == 3) {
            return;
        }
        // Zoom and render the correct spell icon
        unrenderAndUnzoom(selectedSlot);
        selectedSlot += 2;
        renderAndZoom(selectedSlot);
        onArrowButtonPlaySound.Raise();
    }

    void OnButtonA() {
        if (ready) {
            return;
        }
        // Select slot
        if (!slotIsLocked) {
            slotIsLocked = true;
            slotIcons[selectedSlot].GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
            onLockSlotPlaySound.Raise();
            return;
        }
        // BUY/UPGRADE SPELL
        // Do nothing if slot is not locked OR if it is an empty spell
        if (!slotIsLocked || selectedSpell == Spell.nullSpell) {
            return;
        }
        // Check if enough gold
        if (goldAmount < allSpellModels[(int) selectedSpell].Cost) {
            onNotAllowedPlaySound.Raise();
            return;
        }
        int currSpellLevel = playersSpellLevels.GetSpellLevel(playerID, selectedSpell);
        if (slotTiedToSpell[selectedSlot]) {
            // Upgrade
            if (currSpellLevel >= 3) {
                // Spell level is maxed out
                onNotAllowedPlaySound.Raise();
                return;
            }
        } else {
            // Buy
            slotTiedToSpell[selectedSlot] = true;
            playersSpells.SetSpell(playerID, selectedSlot, selectedSpell);
        }
        playersSpellLevels.SetSpellLevel(playerID, selectedSpell, currSpellLevel + 1);
        renderSpellLevel(selectedSlot, currSpellLevel + 1);
        goldAmount -= allSpellModels[(int) selectedSpell].Cost;
        goldText.text = "Gold: " + goldAmount;
        playersGold.SetValue(playerID, goldAmount);
        onBuySpellPlaySound.Raise();
    }

    void OnButtonB() {
        if (ready) {
            return;
        }
        if (!slotIsLocked) {
            return;
        }
        // Unselect slot
        slotIsLocked = false;
        slotIcons[selectedSlot].GetComponent<RawImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
        // Unrender if nothing is bought
        if (!slotTiedToSpell[selectedSlot]) {
            renderSpell(Spell.nullSpell, selectedSlot);
            selectedSpell = Spell.nullSpell;
            selectedSpellInt = -1;
        }
        onUnlockSlotPlaySound.Raise();
    }

    void OnButtonY() {
        if (ready) {
            return;
        }
        // SELL/DOWNGRADE SPELL
        // Do nothing if slot is not locked OR no spell is bought for that slot
        if (!slotIsLocked || !slotTiedToSpell[selectedSlot]) {
            return;
        }
        Spell soldSpell = selectedSpell;
        int currSpellLevel = playersSpellLevels.GetSpellLevel(playerID, soldSpell);
        if (currSpellLevel > 1) {
            // Downgrade
        } else {
            // Sell
            if (selectedSlot == 1) {
                // Cannot sell if fireball
                onNotAllowedPlaySound.Raise();
                return;
            }
            slotTiedToSpell[selectedSlot] = false;
            playersSpells.SetSpell(playerID, selectedSlot, Spell.nullSpell);
            renderSpell(Spell.nullSpell, selectedSlot);
        }
        // Add gold, reduce spell level and remove icon
        playersSpellLevels.SetSpellLevel(playerID, soldSpell, currSpellLevel - 1);
        renderSpellLevel(selectedSlot, currSpellLevel - 1);
        goldAmount += allSpellModels[(int) soldSpell].Cost;
        goldText.text = "Gold: " + goldAmount;
        playersGold.SetValue(playerID, goldAmount);
        onSellSpellPlaySound.Raise();
    }

    private void OnButtonStart() {
        ready = !ready;
        playersReady.SetValue(playerID, ready);
        if (ready) {
            readyPanelObject.SetActive(true);
            onReadyButtonPlaySound.Raise();
        } else {
            readyPanelObject.SetActive(false);
        }
    }

    void OnEnable() {
        refreshShopController();
    }

    void refreshShopController() {
        // Initialise some values
        for (int slot = 3; slot >= 0; slot--) {
            // Render initial icons. Last one is first spell so no need to re-render.
            Spell spell = playersSpells.GetSpell(playerID, slot);
            renderSpell(spell, slot);
            int spellLevel = playersSpellLevels.GetSpellLevel(playerID, spell);
            renderSpellLevel(slot, spellLevel);
            if ((int) spell == -1) {
                slotTiedToSpell[slot] = false;
            } else {
                slotTiedToSpell[slot] = true;
            }
        }
        // Give gold to players after every round
        goldAmount = playersGold.GetValue(playerID);
        goldAmount += gameConstants.goldIncrement;
        playersGold.SetValue(playerID, goldAmount);
        goldText.text = "Gold: " + goldAmount.ToString();

        // Zoom into first slot, unzoom the others
        ready = false;
        slotIsLocked = false;
        selectedSlot = 0;
        selectedSpellInt = -1;
        selectedSpell = Spell.nullSpell;
        slotIcons[0].transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        slotIcons[1].transform.localScale = new Vector3(0.2772619f, 0.2772619f, 0.2772619f);
        slotIcons[2].transform.localScale = new Vector3(0.2772619f, 0.2772619f, 0.2772619f);
        slotIcons[3].transform.localScale = new Vector3(0.2772619f, 0.2772619f, 0.2772619f);
        slotIcons[0].GetComponent<RawImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
        slotIcons[1].GetComponent<RawImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
        slotIcons[2].GetComponent<RawImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
        slotIcons[3].GetComponent<RawImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
    }

    void Start() {
        // Separate into offensive and defensive spells
        defensiveSpellModels = new List<SpellModel>();
        offensiveSpellModels = new List<SpellModel>();
        for (int i = 0; i < allSpellModels.Length; i++) {
            switch (allSpellModels[i].Spell) {
                case Spell.teleport:
                case Spell.rush:
                case Spell.cloud:
                case Spell.wall:
                    defensiveSpellModels.Add(allSpellModels[i]);
                    break;
                case Spell.fireball:
                case Spell.lightning:
                case Spell.tornado:
                case Spell.arc:
                case Spell.splitter:
                case Spell.boomerang:
                case Spell.laser:
                case Spell.minethrow:
                case Spell.groundattack:
                case Spell.iceattack:
                case Spell.shockwave:
                    offensiveSpellModels.Add(allSpellModels[i]);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderSpell(Spell spell, int selectedSlot) {
        if (spell == Spell.nullSpell) {
            slotIcons[selectedSlot].GetComponent<RawImage>().texture = emptyIcon;
            selectedSpell = Spell.nullSpell;
            spellNameText.text = "";
            spellCostText.text = "";
            spellDescText.text = "";
            spellUpgradeText.text = "";
        } else {
            SpellModel spellModel = allSpellModels[(int) spell];
            selectedSpell = spell;
            slotIcons[selectedSlot].GetComponent<RawImage>().texture = spellModel.Icon;
            spellNameText.text = spellModel.Name;
            spellCostText.text = spellModel.Cost.ToString();
            spellDescText.text = spellModel.Description;
            spellUpgradeText.text = spellModel.Upgrade.Replace("\\n", "\n");
        }
    }

    void renderSpellLevel(int slot, int spellLevel) {
        switch (slot) {
            case 0:
                switch (spellLevel) {
                    case 1:
                        s1Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s1Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s1Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 2:
                        s1Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s1Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s1Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 3:
                        s1Levels[0].GetComponent<SpriteRenderer>().color = Color.green;
                        s1Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s1Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    default:
                        s1Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s1Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s1Levels[2].GetComponent<SpriteRenderer>().color = Color.gray;
                        break;
                }
                break;
            case 1:
                switch (spellLevel) {
                    case 1:
                        s2Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s2Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s2Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 2:
                        s2Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s2Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s2Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 3:
                        s2Levels[0].GetComponent<SpriteRenderer>().color = Color.green;
                        s2Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s2Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    default:
                        s2Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s2Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s2Levels[2].GetComponent<SpriteRenderer>().color = Color.gray;
                        break;
                }
                break;
            case 2:
                switch (spellLevel) {
                    case 1:
                        s3Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s3Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s3Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 2:
                        s3Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s3Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s3Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 3:
                        s3Levels[0].GetComponent<SpriteRenderer>().color = Color.green;
                        s3Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s3Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    default:
                        s3Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s3Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s3Levels[2].GetComponent<SpriteRenderer>().color = Color.gray;
                        break;
                }
                break;
            case 3:
                switch (spellLevel) {
                    case 1:
                        s4Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s4Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s4Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 2:
                        s4Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s4Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s4Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 3:
                        s4Levels[0].GetComponent<SpriteRenderer>().color = Color.green;
                        s4Levels[1].GetComponent<SpriteRenderer>().color = Color.green;
                        s4Levels[2].GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    default:
                        s4Levels[0].GetComponent<SpriteRenderer>().color = Color.gray;
                        s4Levels[1].GetComponent<SpriteRenderer>().color = Color.gray;
                        s4Levels[2].GetComponent<SpriteRenderer>().color = Color.gray;
                        break;
                }
                break;
        }
    }

    void unrenderAndUnzoom(int slot) {
        // Render earlier slot icon to be nothing if no spell is bought and unzoom that slot
        if (!slotTiedToSpell[slot]) {
            renderSpell(Spell.nullSpell, slot);
        }
        slotIcons[slot].transform.localScale = new Vector3(0.2772619f, 0.2772619f, 0.2772619f);
    }

    void renderAndZoom(int slot) {
        // Note: I only care about selectedSpellInt if nothing is bought on that slot (so need to choose spells). 
        // (cont'd) Do not determine selectedSpellInt based on selectedSpell because selectedSpellInt is wrt defensive/offensive spell indexes
        // (cont'd) BUT I need selectedSpell even if a spell is bought, so that this can be used for upgrading spells
        if (slotTiedToSpell[slot] == false) {
            selectedSpellInt = -1;
        }
        selectedSpell = playersSpells.GetSpell(playerID, slot);
        // Render icon to display spell bought or empty if nothing and zoom chosen slot
        renderSpell(selectedSpell, slot);
        slotIcons[slot].transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }
}
