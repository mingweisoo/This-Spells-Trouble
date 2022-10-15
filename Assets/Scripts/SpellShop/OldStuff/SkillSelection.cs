using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    public SpellModel[] offensiveSpells;
    public SpellModel[] defensiveSpells;
    private List<GameObject> offensiveGameObjectList;
    private List<GameObject> defensiveGameObjectList;
    private List<GameObject> emptyGameObjectList;
    public GameObject emptySprite;
    public Text skillNameText;
    public Text skillCostText;
    public Text skillDescText;
    public Text upgradeText;
    public List<GameObject> skillStatus1;
    public List<GameObject> skillStatus2;
    public List<GameObject> skillStatus3;
    public List<GameObject> skillStatus4;
    public List<List<GameObject>> skillStatus;
    private int slotSelected = 0;
    private int[] skillMapping; // Maps slot to GameObjects
    private int previousSelection;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise Lists
        offensiveGameObjectList = new List<GameObject>();
        defensiveGameObjectList = new List<GameObject>();
        emptyGameObjectList = new List<GameObject>();
        skillStatus = new List<List<GameObject>>{skillStatus1, skillStatus2, skillStatus3, skillStatus4};
        initialPosition = this.transform.localPosition;

        // Array containing the index of selected skill
        skillMapping = new int[4];

        // Instantiate Offensive Spells
        foreach (var skillModel in offensiveSpells)
        {
            // GameObject skill = Instantiate(skillModel.SkillPrefab, this.transform.position, Quaternion.identity);
            // skill.transform.SetParent(this.transform, true);
            // skill.SetActive(false);
            // offensiveGameObjectList.Add(skill);
        }

        // Instantiate Defensive Spells
        foreach (var skillModel in defensiveSpells)
        {
            // GameObject skill = Instantiate(skillModel.SkillPrefab, this.transform.position, Quaternion.identity);
            // skill.transform.SetParent(this.transform, true);
            // skill.SetActive(false);
            // defensiveGameObjectList.Add(skill);
        }

        // Set empty sprite for Slots 2 to 4
        for (var i = 1; i < 4; i++)
        {
            GameObject empty = Instantiate(emptySprite, this.transform.position + new Vector3(0f, -1.38f * i, 0f), Quaternion.identity);
            // empty.transform.SetParent(this.transform, true);
            emptyGameObjectList.Add(empty);
            skillMapping[i] = -1;
        }

        // Fixed first skill as fireball 
        skillMapping[slotSelected] = 0;
        offensiveGameObjectList[skillMapping[slotSelected]].SetActive(true);
        offensiveGameObjectList[skillMapping[slotSelected]].transform.parent = this.transform.parent;
        skillStatus[slotSelected][0].GetComponent<SpriteRenderer>().color = Color.green;
        updateSkillPopup();
        zoomSelected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            int currentIndex = skillMapping[slotSelected];
            GameObject currentSkill;
            GameObject newSkill;

            // Fireball slot fixed
            if (slotSelected == 0)
            {
                return;
            }

            // Defensive Spell Slot
            else if (slotSelected == 3)
            {
                if (currentIndex == -1) { currentSkill = emptyGameObjectList[slotSelected - 1]; currentIndex = 0; }
                else { currentSkill = defensiveGameObjectList[currentIndex]; };

                // Slot is fixed until they sell
                if (currentSkill.transform.parent == this.transform.parent)
                {
                    return;
                }
                currentIndex -= 1;
                if (currentIndex < 0)
                {
                    currentIndex += defensiveGameObjectList.Count;
                };
                newSkill = defensiveGameObjectList[currentIndex];
                skillMapping[slotSelected] = currentIndex;
            }

            // Offensive Spell Slots
            else
            {
                if (currentIndex == -1) { currentSkill = emptyGameObjectList[slotSelected - 1]; currentIndex = 0; }
                else { currentSkill = offensiveGameObjectList[currentIndex]; };

                // Slot is fixed until they sell
                if (currentSkill.transform.parent == this.transform.parent)
                {
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
        if (Input.GetKeyDown("right"))
        {
            int currentIndex = skillMapping[slotSelected];
            GameObject currentSkill;
            GameObject newSkill;

            // Fireball slot fixed
            if (slotSelected == 0)
            {
                return;
            }

            // Defensive Spell Slot
            else if (slotSelected == 3)
            {
                if (currentIndex == -1) { currentSkill = emptyGameObjectList[slotSelected - 1]; }
                else { currentSkill = defensiveGameObjectList[currentIndex]; };

                // Slot is fixed until they sell
                if (currentSkill.transform.parent == this.transform.parent)
                {
                    return;
                }

                newSkill = defensiveGameObjectList[(currentIndex + 1) % defensiveGameObjectList.Count];
                skillMapping[slotSelected] = (currentIndex + 1) % defensiveGameObjectList.Count;
            }

            // Offensive Spell Slots
            else
            {
                if (currentIndex == -1) { currentSkill = emptyGameObjectList[slotSelected - 1]; }
                else { currentSkill = offensiveGameObjectList[currentIndex]; };

                // Slot is fixed until they sell
                if (currentSkill.transform.parent == this.transform.parent)
                {
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
        if (Input.GetKeyDown("up"))
        {
            // Reset icon to display skill bought
            resetSelection();

            // Decrease slot index selected
            slotSelected -= 1;
            if (slotSelected < 0)
            {
                slotSelected += skillMapping.Length;
            };

            // Shift the gameObject group position
            this.transform.localPosition = new Vector3(0, initialPosition.y - slotSelected * 70f, 0);

            // Keep track of skill bought
            previousSelection = skillMapping[slotSelected];
            updateSkillPopup();
            zoomSelected();
        }
        if (Input.GetKeyDown("down"))
        {
            // Reset icon to display skill bought
            resetSelection();

            // Increase slot index selected
            slotSelected = (slotSelected + 1) % skillMapping.Length;

            // Shift the gameObject group position
            this.transform.localPosition = new Vector3(0, initialPosition.y - slotSelected * 70f, 0);

            // Keep track of skill bought
            previousSelection = skillMapping[slotSelected];
            updateSkillPopup();
            zoomSelected();
        }

        // Buy/Upgrade a Skill
        if (Input.GetKeyDown("a"))
        {
            // Prevent buying on empty slot
            if (skillMapping[slotSelected] == -1) { return; }

            // Removing the bought skill from the gameObject group so that it is always displayed
            if (slotSelected == 3) { defensiveGameObjectList[skillMapping[slotSelected]].transform.parent = this.transform.parent; }
            else { offensiveGameObjectList[skillMapping[slotSelected]].transform.parent = this.transform.parent; }
            previousSelection = skillMapping[slotSelected];

            // Update upgrade bar
            foreach (var skillLevel in skillStatus[slotSelected])
            {
                // Set the next skill bar to green
                if (skillLevel.GetComponent<SpriteRenderer>().color != Color.green){
                    skillLevel.GetComponent<SpriteRenderer>().color = Color.green;
                    return;
                }
            }

        }

        // Sell a Skill
        if (Input.GetKeyDown("b"))
        {
            // Prevent buying on empty slot
            if (skillMapping[slotSelected] == -1 || slotSelected == 0) { return; }

            // Adding the bought skill to the gameObject group so that it can be browsed
            if (slotSelected == 3) { defensiveGameObjectList[skillMapping[slotSelected]].transform.parent = this.transform; }
            else { offensiveGameObjectList[skillMapping[slotSelected]].transform.parent = this.transform; }
            // Reset to empty sprite
            previousSelection = -1;

            // Update upgrade bar
            foreach (var skillLevel in skillStatus[slotSelected])
            {
                // Set all skill bar to gray
                skillLevel.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }

    void updateSkillPopup()
    {
        //  Empty slot
        if (skillMapping[slotSelected] == -1)
        {
            skillCostText.text = "";
            skillNameText.text = "";
            skillDescText.text = "";
            upgradeText.text = "";
        }

        // Defensive spells
        else if (slotSelected == 3)
        {
            skillCostText.text = defensiveSpells[skillMapping[slotSelected]].Cost.ToString();
            skillNameText.text = defensiveSpells[skillMapping[slotSelected]].Name;
            skillDescText.text = defensiveSpells[skillMapping[slotSelected]].Description;
            upgradeText.text = "Next Upgrade: " + defensiveSpells[skillMapping[slotSelected]].Upgrade;
        }

        // Offensive spells
        else
        {
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
        if (previousSelection != skillMapping[slotSelected])
        {
            if (slotSelected == 3) { currentSkill = defensiveGameObjectList[skillMapping[slotSelected]]; }
            else { currentSkill = offensiveGameObjectList[skillMapping[slotSelected]]; }
            currentSkill.SetActive(false);

            if (previousSelection == -1) { prevSkill = emptyGameObjectList[slotSelected - 1]; skillMapping[slotSelected] = -1; }
            else if (slotSelected == 3) { prevSkill = defensiveGameObjectList[previousSelection]; }
            else { prevSkill = offensiveGameObjectList[previousSelection]; }
            prevSkill.SetActive(true);
        }

    }

    int getNextAvailableSkill(List<GameObject> gameObjects, int currentIndex)
    {
        currentIndex = (currentIndex + 1) % gameObjects.Count;
        for (var i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[currentIndex].activeSelf) { currentIndex = (currentIndex + 1) % gameObjects.Count; }
            else { return currentIndex; }

        }
        return -1;
    }

    int getPrevAvailableSkill(List<GameObject> gameObjects, int currentIndex)
    {
        currentIndex -= 1;
        if (currentIndex < 0)
        {
            currentIndex += gameObjects.Count;
        };
        for (var i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[currentIndex].activeSelf)
            {
                currentIndex -= 1;
                if (currentIndex < 0)
                {
                    currentIndex += gameObjects.Count;
                };
            }
            else { return currentIndex; }
        }
        return -1;
    }

    // Zoom into the slot currently
    void zoomSelected()
    {

        for (var i = 0; i < 4; i++)
        {
            if (i == slotSelected)
            {
                GameObject currentSkill;
                if (skillMapping[i] == -1) { currentSkill = emptyGameObjectList[i - 1]; }
                else if (i == 3) { currentSkill = defensiveGameObjectList[skillMapping[i]]; }
                else { currentSkill = offensiveGameObjectList[skillMapping[i]]; };

                // Scale
                Transform tempTransform = currentSkill.transform.parent;
                currentSkill.transform.parent = null;
                currentSkill.transform.localScale = new Vector3(7f, 7f, 7f);
                currentSkill.transform.parent = tempTransform;

            }
            else
            {
                GameObject currentSkill;
                if (skillMapping[i] == -1) { currentSkill = emptyGameObjectList[i - 1]; }
                else if (i == 3) { currentSkill = defensiveGameObjectList[skillMapping[i]]; }
                else { currentSkill = offensiveGameObjectList[skillMapping[i]]; };

                Transform tempTransform = currentSkill.transform.parent;
                currentSkill.transform.parent = null;
                currentSkill.transform.localScale = new Vector3(5f, 5f, 0f);
                currentSkill.transform.parent = tempTransform;
            }
        }
    }
}
