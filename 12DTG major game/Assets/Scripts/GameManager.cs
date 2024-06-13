using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This is to declear the game objects. 
    public GameObject Firstlevel;
    public GameObject Secondlevel;
    public GameObject Thirdlevel;

    private PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        //This finds the player controller from player. 
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //This deactivates everything when the player chooses the endless option. 
        if (PlayerController.EndlessGameIsActive == true)
        {
            Firstlevel.SetActive(false);
            Secondlevel.SetActive(false);
            Thirdlevel.SetActive(false);
        }
    }
}
