using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlsScript : MonoBehaviour
{
    //These lines of code lets the player aign the etxt that will be displayed.
    public TextMeshProUGUI ControlsText;
    public TextMeshProUGUI story;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        //This makes the code try to get the component button.
        button = GetComponent<Button>();
        //This line of code takes the script to the actiavte function if the button is clicked.
        button.onClick.AddListener(Activate);
    }
    //This is the activate function. It is activated when a button is clicked.
    void Activate()
    {
        //This activates the control text making it display.
        ControlsText.gameObject.SetActive(true);
        //Deactiavtes the story
        story.gameObject.SetActive(false);
        //this makes the program wait 2 seconds.
        StartCoroutine(PowerupCountdownRoutine());
    }

    IEnumerator PowerupCountdownRoutine()
    {
        //The 2 second between control text being activated and deactivated. 
        yield return new WaitForSeconds(2);
        //This deactivate sthe control text after 2 seonds
        ControlsText.gameObject.SetActive(false);
        //Reactiavtes the story
        story.gameObject.SetActive(true);
    }
}
