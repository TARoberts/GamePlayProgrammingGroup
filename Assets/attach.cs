using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attach : MonoBehaviour
{
    [SerializeField] Transform parental;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = parental;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
