using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
    public GameObject Plane;
    public GameObject Piller;
    public GameObject Piller2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        System.Random ran = new System.Random();
        int randomInt = ran.Next(1, 3);

        if ( ran = 1)
        {
            Plane.SetActive(false);
        }
        else if (ran = 2)
        {
            Piller.SetActive(false);
        }
        else
        {
            Piller2.SetActive(false);
        }
    }
}
