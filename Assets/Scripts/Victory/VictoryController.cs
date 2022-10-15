using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public VictoryManager victoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPreviousButton() {
        victoryManager.previousButton();
    }

    void OnNextButton() {
        victoryManager.nextButton();
    }

    void OnClickButton() {
        victoryManager.clickButton();
    }
}
