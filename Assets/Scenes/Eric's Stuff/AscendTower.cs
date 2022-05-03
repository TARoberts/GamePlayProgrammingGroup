using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AscendTower : MonoBehaviour
{
    public Text Objective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Objective.text = "Objective : Ascend the Tower";
            Destroy(gameObject);
        }
    }
}
