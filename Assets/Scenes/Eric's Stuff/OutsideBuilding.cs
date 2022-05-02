using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideBuilding : MonoBehaviour
{
    public Text Objective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Objective.text = "Objective : Find the Button to Open the Door";
            Destroy(gameObject);
        }
    }
}
