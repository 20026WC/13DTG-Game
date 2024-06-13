using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surprise : MonoBehaviour
{
    public GameObject enemeie;
    public GameObject cylinders;
    private FirstBoss FirstBoss;
    // Start is called before the first frame update
    void Start()
    {
        FirstBoss = GameObject.Find("Yellow First Boss").GetComponent<FirstBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstBoss.BeginAttack == true)
        {
            enemeie.SetActive(true);
            cylinders.SetActive(false);
        }
    }
}
