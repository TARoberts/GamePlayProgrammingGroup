using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EricsPowerUps : MonoBehaviour
{
    public bool SpeedBoost;
    public bool DoubleJump;
    public bool Active;
    public bool OneTimeUse;
    public int RespawnTimer;
    public Material SpeedBoostMaterial;
    public Material DoubleJumpMaterial;

    void Update()
    {
        if (SpeedBoost)
        {
            gameObject.GetComponent<MeshRenderer>().material = SpeedBoostMaterial;
        }
        else if (DoubleJump)
        {
            gameObject.GetComponent<MeshRenderer>().material = DoubleJumpMaterial;
        }

        if (SpeedBoost && Active)
        {
            StartCoroutine(SpeedBoostRoutine(RespawnTimer));
        }

        if (DoubleJump && Active)
        {
            StartCoroutine(DoubleJumpRoutine(RespawnTimer));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with PowerUp");
            Active = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with PowerUp");
            Active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with PowerUp");
            Active = true;
        }
    }

    IEnumerator SpeedBoostRoutine(int seconds)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().playerSpeed = 12.0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().speedParticle.SetActive(true);

        yield return new WaitForSeconds(seconds);

        if (OneTimeUse)
        {
            Destroy(gameObject);
        }

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().playerSpeed = 4.0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().speedParticle.SetActive(false);
        Active = false;
    }

    IEnumerator DoubleJumpRoutine(int seconds)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().canDoubleJump = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().jumpParticle.SetActive(true);

        yield return new WaitForSeconds(seconds);

        if (OneTimeUse)
        {
            Destroy(gameObject);
        }

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().canDoubleJump = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().jumpParticle.SetActive(false);
        Active = false;
    }
}
