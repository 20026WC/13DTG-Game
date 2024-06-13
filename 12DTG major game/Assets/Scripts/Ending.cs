using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] GameObject _TheEnd;
    [SerializeField] GameObject _Restartbutton;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Weapon"))
        {
            Destroy(gameObject);
            _TheEnd.SetActive(true);
            _Restartbutton.SetActive(true);
        }
    }
}
