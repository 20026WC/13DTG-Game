using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
    public GameObject Plane;
    public GameObject Piller;
    public GameObject Piller2;


    public void RandomNumber()
    {
        int ran = UnityEngine.Random.Range(1, 4);

        if (ran == 1)
        {
            Plane.SetActive(false);
        }
        else if (ran == 2)
        {
            Piller.SetActive(false);
        }
        else
        {
            Piller2.SetActive(false);
        }
    }
}
