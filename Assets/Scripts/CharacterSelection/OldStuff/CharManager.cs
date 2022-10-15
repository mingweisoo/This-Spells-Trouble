using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
    public GameObject[] characters;
    public Text text;
    public int selectedChar = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            text.text = "";
            characters[selectedChar].SetActive(true);
        }
        if (Input.GetKeyDown("up"))
        {
            characters[selectedChar].SetActive(false);
            selectedChar = (selectedChar + 1) % characters.Length;
            characters[selectedChar].SetActive(true);
        }
        if (Input.GetKeyDown("down"))
        {
            characters[selectedChar].SetActive(false);
            selectedChar-= 1;
            if (selectedChar < 0){
                selectedChar += characters.Length;
            };
            characters[selectedChar].SetActive(true);
        }
    }
}
