using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public GameObject player;
    //when player walks in

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && tag == "SpeedBoost")
        {
            ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
            particle.Play();
            StartCoroutine(Boost());
        }
        else if (other.tag == "Player" && tag == "JumpBoost")
        {
            StartCoroutine(Jumps());
        }
    }

    IEnumerator Boost()
    {
        ThirdPersonMovement movement = player.GetComponent<ThirdPersonMovement>();
        movement.speed = 10;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(5f);
        movement.speed = 6;
        gameObject.SetActive(false);

    }

    IEnumerator Jumps()
    {
        ThirdPersonMovement movement = player.GetComponent<ThirdPersonMovement>();
        movement.doubleJump = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);

    }

}
