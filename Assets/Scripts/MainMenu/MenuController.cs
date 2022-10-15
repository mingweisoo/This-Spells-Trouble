
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPreviousButton() {
        menuManager.previousButton();
    }

    void OnNextButton() {
        menuManager.nextButton();
    }

    void OnClickButton() {
        menuManager.clickButton();
    }

    void OnPreviousPage() {
        menuManager.previousPage();
    }

    void OnNextPage() {
        menuManager.nextPage();
    }

    void OnCloseHelp() {
        menuManager.closeHelp();
    }
}
