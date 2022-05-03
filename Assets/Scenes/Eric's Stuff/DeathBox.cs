using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] Transform SpawnPointObject;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<CharacterController>().enabled = false;
            other.transform.position = SpawnPointObject.position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
