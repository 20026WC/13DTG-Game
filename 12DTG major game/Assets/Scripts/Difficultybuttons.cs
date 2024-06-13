using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficultybuttons : MonoBehaviour
{
    [SerializeField] GameObject _TitleScreen;
    //This lets difficulty to be inputed to fiot each of the buttons difficultys.
    public int difficulty;
    private Button button;
    //Calls the playercontroller script.
    private PlayerController PlayerController;
    // Start is called before the first frame update

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        //This makes the script search out the player game object and get the player controller script.
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //This is the set difficulty function. It is called when one of the difficultys are chosen.
    void SetDifficulty()
    {
        //This causes a comment to be displayed showing which difficulty the player chose.
        Debug.Log(gameObject.name + " was clicked");
        //This calls the start game function from the playercontroller.
        PlayerController.NormalStartGame(difficulty);
        //When a difficultyis chsoen the title screen is deactiavted.
        _TitleScreen.SetActive(false);
        
    }
}
