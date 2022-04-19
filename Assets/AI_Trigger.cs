using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Trigger : MonoBehaviour
{
    [SerializeField] AIScript aiscript;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            aiscript.distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            aiscript.distance = 20000;
        }
        
    }
}
