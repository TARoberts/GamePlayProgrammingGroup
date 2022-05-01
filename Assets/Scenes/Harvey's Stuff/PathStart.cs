using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathStart : MonoBehaviour
{
    public GameObject player;
    public GameObject pathEnd;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        pathEnd.gameObject.GetComponent<BoxCollider>().enabled = true;
        player.gameObject.GetComponent<BezierSolution.BezierAttachment>().enabled = true;
        player.gameObject.GetComponent<BezierSolution.BezierAttachment>().normalizedT = 0f;
    }
}
