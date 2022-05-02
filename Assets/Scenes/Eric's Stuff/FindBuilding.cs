using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindBuilding : MonoBehaviour
{
    public Text Objective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Objective.text = "Objective : Find the Entrance to the Red Building";
            Destroy(gameObject);
        }
    }
}
