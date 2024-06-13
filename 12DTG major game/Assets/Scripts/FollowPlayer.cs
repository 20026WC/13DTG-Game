using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    //This is teyh distance between the player ands the camera. 
    private Vector3 offset = new Vector3(0, 5, -7);

    // Update is called once per frame
    void Update()
    {
        //This makes the camera follows behind the player
        transform.position = player.transform.position + offset;
    }
}
