using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{
    public Text Objective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Objective.text = "---- Victory! Congratulations You Won! ----";
            Destroy(gameObject);
        }
    }
}
