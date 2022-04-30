using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Transform newPos;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = newPos.position;
    }
}
