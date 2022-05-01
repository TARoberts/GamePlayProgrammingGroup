using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEnd : MonoBehaviour
{
    public GameObject player;
    public GameObject pathStart;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        pathStart.gameObject.GetComponent<BoxCollider>().enabled = true;
        player.gameObject.GetComponent<BezierSolution.BezierAttachment>().enabled = false;
    }
}
