using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //this code makes the energyball move to the left
        transform.Translate(Vector3.down* Time.deltaTime * 7);
        StartCoroutine(PowerupCountdownRoutine());

    }

    IEnumerator PowerupCountdownRoutine()
    {
        //This code makes the energyball move to the chosen direction for 7 seconds before being deleted. 
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}