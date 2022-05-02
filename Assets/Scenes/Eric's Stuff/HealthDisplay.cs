using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Text HealthText;
    private void Update()
    {
        HealthText.text = "Health : " + GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().PlayerHP;
    }
}
