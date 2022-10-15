using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShopControllerOld : MonoBehaviour
{
    // ScriptableObjects
    public PlayersSpells playersSpells;
    public PlayersSpellLevels playersSpellLevels;

    // Others
    public SpellModel[] offensiveSpells;
    public SpellModel[] defensiveSpells;
    private GameObject[] offensiveGameObjectList;
    private GameObject[] defensiveGameObjectList;
    private GameObject[] emptyGameObjectList;
    public GameObject emptySprite;
    public Text skillNameText;
    public Text skillCostText;
    public Text skillDescText;
    public Text upgradeText;
    public List<GameObject> skillStatus1;
    public List<GameObject> skillStatus2;
    public List<GameObject> skillStatus3;
    public List<GameObject> skillStatus4;
    private List<List<GameObject>> skillStatus;
    private int slotSelected = 0;
    private int[] skillMapping; // Maps slot to GameObjects
    private int previousSelection;
    public GameObject skillsController;
    public Vector3 initialPosition;

    // Game State
    public int playerID;

    private void OnPreviousSlot() {
        // Reset icon to display skill bought
        resetSelection();
        // Decrease slot index selected
        slotSelected -= 1;
        if (slotSelected < 0) {
            slotSelected += skillMapping.Length;
        };
        // Shift the gameObject group position
        skillsController.transform.localPosition = new Vector3(0, initialPosition.y - slotSelected * 70f, 0);
        // Keep track of skill bought
        previousSelection = skillMapping[slotSelected];
        updateSkillPopup();
        zoomSelected();
    }

    private void OnNextSlot() {
        // Reset icon to display skill bought
        resetSelection();
        // Increase slot index selected
        slotSelected = (slotSelected + 1) % skillMapping.Length;
        // Shift the gameObject group position
        skillsController.transform.localPosition = new Vector3(0, initialPosition.y - slotSelected * 70f, 0);
        // Keep track of skill bought
        previousSelection = skillMapping[slotSelected];
        updateSkillPopup();
        zoomSelected();
}

    private void OnPreviousSpell() {
        int currentIndex = skillMapping[slotSelected];
        GameObject currentSkill;
        GameObject newSkill;

        // Fireball slot fixed
        if (slotSelected == 1) {
            return;
        }

        // Defensive Spell Slot
        else if (slotSelected == 0) {
            if (currentIndex == -1) {
                currentSkill = emptyGameObjectList[slotSelected];
                currentIndex = 0;
            }
            else {
                currentSkill = defensiveGameObjectList[currentIndex];
            }

            // Slot is fixed until they sell
            if (currentSkill.transform.parent == skillsController.transform.parent) {
                return;
            }
            currentIndex -= 1;
            if (currentIndex < 0) {
                currentIndex += defensiveGameObjectList.Length;
            }
            newSkill = defensiveGameObjectList[currentIndex];
            skillMapping[slotSelected] = currentIndex;
        }

        // Offensive Spell Slots
        else {
            if (currentIndex == -1) {
                currentSkill = emptyGameObjectList[slotSelected];
                currentIndex = 0;
            }
            else {
                currentSkill = offensiveGameObjectList[currentIndex];
            }

            // Slot is fixed until they sell
            if (currentSkill.transform.parent == skillsController.transform.parent) {
                return;
            }

            int prevAvailableSkill = getPrevAvailableSkill(offensiveGameObjectList, currentIndex);
            newSkill = offensiveGameObjectList[prevAvailableSkill];
            skillMapping[slotSelected] = prevAvailableSkill;
        };

        // Display next skill in list
        currentSkill.SetActive(false);
        newSkill.SetActive(true);
        updateSkillPopup();
        zoomSelected();
    }

    private void OnNextSpell() {
        int currentIndex = skillMapping[slotSelected];
        GameObject currentSkill;
        GameObject newSkill;

        // Fireball slot fixed
        if (slotSelected == 1) {
            return;
        }

        // Defensive Spell Slot
        else if (slotSelected == 0) {
            if (currentIndex == -1) {
                currentSkill = emptyGameObjectList[slotSelected];
            }
            else {
                currentSkill = defensiveGameObjectList[currentIndex];
            }

            // Slot is fixed until they sell
            if (currentSkill.transform.parent == skillsController.transform.parent) {
                return;
            }

            newSkill = defensiveGameObjectList[(currentIndex + 1) % defensiveGameObjectList.Length];
            skillMapping[slotSelected] = (currentIndex + 1) % defensiveGameObjectList.Length;
        }

        // Offensive Spell Slots
        else {
            if (currentIndex == -1) {
                currentSkill = emptyGameObjectList[slotSelected];
            }
            else {
                currentSkill = offensiveGameObjectList[currentIndex];
            };

            // Slot is fixed until they sell
            if (currentSkill.transform.parent == skillsController.transform.parent) {
                return;
            }

            int nextAvailableSkill = getNextAvailableSkill(offensiveGameObjectList, currentIndex);
            newSkill = offensiveGameObjectList[nextAvailableSkill];
            skillMapping[slotSelected] = nextAvailableSkill;
        };

        // Display next skill in list
        currentSkill.SetActive(false);
        newSkill.SetActive(true);
        updateSkillPopup();
        zoomSelected();
    }

    private void OnBuySpell() {
        // Prevent buying on empty slot
        if (skillMapping[slotSelected] == -1) {
            return;
        }

        // Removing the bought skill from the gameObject group so that it is always displayed
        if (slotSelected == 0) {
            defensiveGameObjectList[skillMapping[slotSelected]].transform.parent = skillsController.transform.parent;
        }
        else {
            offensiveGameObjectList[skillMapping[slotSelected]].transform.parent = skillsController.transform.parent;
        }
        previousSelection = skillMapping[slotSelected];

        // Update upgrade bar
        foreach (var skillLevel in skillStatus[slotSelected]) {
            // Set the next skill bar to green
            if (skillLevel.GetComponent<SpriteRenderer>().color != Color.green){
                skillLevel.GetComponent<SpriteRenderer>().color = Color.green;
                return;
            }
        }
    }

    private void OnSellSpell() {
        // Prevent buying on empty slot
        if (skillMapping[slotSelected] == -1 || slotSelected == 1) {
            return;
        }

        // Adding the bought skill to the gameObject group so that it can be browsed
        if (slotSelected == 0) {
            defensiveGameObjectList[skillMapping[slotSelected]].transform.parent = skillsController.transform;
        }
        else {
            offensiveGameObjectList[skillMapping[slotSelected]].transform.parent = skillsController.transform;
        }
        // Reset to empty sprite
        previousSelection = -1;

        // Update upgrade bar
        foreach (var skillLevel in skillStatus[slotSelected]) {
            // Set all skill bar to gray
            skillLevel.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialise Lists
        skillStatus = new List<List<GameObject>>{skillStatus1, skillStatus2, skillStatus3, skillStatus4};
        initialPosition = skillsController.transform.localPosition;

        // Array containing the index of selected skill
        skillMapping = new int[4];

        // Instantiate Offensive Spells
        offensiveGameObjectList = new GameObject[offensiveSpells.Length];
        for (int i = 0; i < offensiveSpells.Length; i++) {
            // GameObject skill = Instantiate(offensiveSpells[i].SkillPrefab, skillsController.transform.position, Quaternion.identity);
            // skill.transform.SetParent(skillsController.transform, true);
            // skill.SetActive(false);
            // offensiveGameObjectList[i] = skill;
        }

        // Instantiate Defensive Spells
        defensiveGameObjectList = new GameObject[defensiveSpells.Length];
        for (int i = 0; i < defensiveSpells.Length; i++) {
            // GameObject skill = Instantiate(defensiveSpells[i].SkillPrefab, skillsController.transform.position, Quaternion.identity);
            // skill.transform.SetParent(skillsController.transform, true);
            // skill.SetActive(false);
            // defensiveGameObjectList[i] = skill;
        }

        // Set empty sprite for Slots 1, 3 and 4
        emptyGameObjectList = new GameObject[4];
        for (int i = 0; i < 4; i++) {
            GameObject empty = Instantiate(emptySprite, skillsController.transform.position + new Vector3(0f, -1.38f * i, 0f), Quaternion.identity);
            emptyGameObjectList[i] = empty;
            if (i != 1) {
                skillMapping[i] = -1;
            }
        }

        // Fixed first skill as fireball 
        // skillMapping[slotSelected] = 0;
        // offensiveGameObjectList[skillMapping[slotSelected]].SetActive(true);
        // offensiveGameObjectList[skillMapping[slotSelected]].transform.parent = skillsController.transform.parent;
        // skillStatus[slotSelected][0].GetComponent<SpriteRenderer>().color = Color.green;
        skillMapping[1] = 0;
        offensiveGameObjectList[skillMapping[1]].SetActive(true);
        offensiveGameObjectList[skillMapping[1]].transform.parent = skillsController.transform.parent;
        skillStatus[1][0].GetComponent<SpriteRenderer>().color = Color.green;
        updateSkillPopup();
        zoomSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateSkillPopup() {
        //  Empty slot
        if (skillMapping[slotSelected] == -1) {
            skillCostText.text = "";
            skillNameText.text = "";
            skillDescText.text = "";
            upgradeText.text = "";
        }

        // Defensive spells
        else if (slotSelected == 0) {
            skillCostText.text = defensiveSpells[skillMapping[slotSelected]].Cost.ToString();
            skillNameText.text = defensiveSpells[skillMapping[slotSelected]].Name;
            skillDescText.text = defensiveSpells[skillMapping[slotSelected]].Description;
            upgradeText.text = "Next Upgrade: " + defensiveSpells[skillMapping[slotSelected]].Upgrade;
        }

        // Offensive spells
        else {
            skillCostText.text = offensiveSpells[skillMapping[slotSelected]].Cost.ToString();
            skillNameText.text = offensiveSpells[skillMapping[slotSelected]].Name;
            skillDescText.text = offensiveSpells[skillMapping[slotSelected]].Description;
            upgradeText.text = "Next Upgrade: " + offensiveSpells[skillMapping[slotSelected]].Upgrade;
        }
    }
    void resetSelection()
    {
        GameObject currentSkill;
        GameObject prevSkill;
        // Set the last known item to be active if no purchase was made
        if (previousSelection != skillMapping[slotSelected]) {
            if (skillMapping[slotSelected] == -1) {
                currentSkill = emptyGameObjectList[slotSelected];
            } else {
                if (slotSelected == 0) {
                    currentSkill = defensiveGameObjectList[skillMapping[slotSelected]];
                } else {
                    currentSkill = offensiveGameObjectList[skillMapping[slotSelected]];
                }
            }
            currentSkill.SetActive(false);

            if (previousSelection == -1) {
                prevSkill = emptyGameObjectList[slotSelected];
                skillMapping[slotSelected] = -1;
            } else if (slotSelected == 0) {
                prevSkill = defensiveGameObjectList[previousSelection];
            } else {
                prevSkill = offensiveGameObjectList[previousSelection];
            }
            prevSkill.SetActive(true);
        }

    }

    int getNextAvailableSkill(GameObject[] gameObjects, int currentIndex)
    {
        currentIndex = (currentIndex + 1) % gameObjects.Length;
        for (var i = 0; i < gameObjects.Length; i++) {
            if (gameObjects[currentIndex].activeSelf) { currentIndex = (currentIndex + 1) % gameObjects.Length; }
            else { return currentIndex; }

        }
        return -1;
    }

    int getPrevAvailableSkill(GameObject[] gameObjects, int currentIndex) {
        currentIndex -= 1;
        if (currentIndex < 0) {
            currentIndex += gameObjects.Length;
        };
        for (var i = 0; i < gameObjects.Length; i++) {
            if (gameObjects[currentIndex].activeSelf) {
                currentIndex -= 1;
                if (currentIndex < 0) {
                    currentIndex += gameObjects.Length;
                };
            }
            else {
                return currentIndex;
            }
        }
        return -1;
    }

    // Zoom into the slot currently
    void zoomSelected() {

        for (var i = 0; i < 4; i++) {
            if (i == slotSelected) {
                GameObject currentSkill;
                if (skillMapping[i] == -1) {
                    currentSkill = emptyGameObjectList[i];
                } else if (i == 0) {
                    currentSkill = defensiveGameObjectList[skillMapping[i]];
                }
                else {
                    currentSkill = offensiveGameObjectList[skillMapping[i]];
                }

                // Scale
                Transform tempTransform = currentSkill.transform.parent;
                currentSkill.transform.parent = null;
                currentSkill.transform.localScale = new Vector3(7f, 7f, 7f);
                currentSkill.transform.parent = tempTransform;

            }
            else {
                GameObject currentSkill;
                if (skillMapping[i] == -1) {
                    currentSkill = emptyGameObjectList[i];
                }
                else if (i == 3) {
                    currentSkill = defensiveGameObjectList[skillMapping[i]];
                }
                else {
                    currentSkill = offensiveGameObjectList[skillMapping[i]];
                };

                Transform tempTransform = currentSkill.transform.parent;
                currentSkill.transform.parent = null;
                currentSkill.transform.localScale = new Vector3(5f, 5f, 0f);
                currentSkill.transform.parent = tempTransform;
            }
        }
    }
}
