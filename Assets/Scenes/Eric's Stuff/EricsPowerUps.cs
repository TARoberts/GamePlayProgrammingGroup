using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EricsPowerUps : MonoBehaviour
{
    public bool PowerUpActive = false;
    public bool DestroyPowerUp = false;
    public bool SpeedBoost = false;
    public bool DoubleJump = false;
    public bool HealthPowerUp = false;
    public Material SpeedMaterial;
    public Material JumpMaterial;
    public Material HealthMaterial;
    public bool OneTimeUse = false;
    public int RespawnTimer = 0;

    private void Start()
    {
        if (SpeedBoost)
        {
            gameObject.GetComponent<MeshRenderer>().material = SpeedMaterial;
        }
        else if (DoubleJump)
        {
            gameObject.GetComponent<MeshRenderer>().material = JumpMaterial;
        }
        else if (HealthPowerUp)
        {
            gameObject.GetComponent<MeshRenderer>().material = HealthMaterial;
        }
    }


    void LateUpdate()
    {
        if (DestroyPowerUp == false)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
            if (SpeedBoost && PowerUpActive)
            {
                gameObject.GetComponent<MeshRenderer>().material = SpeedMaterial;
                GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().playerSpeed = 9.0f;
                GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().speedParticle.SetActive(true);
                StartCoroutine(PowerUpDuration(RespawnTimer));
            }

            else if (DoubleJump && PowerUpActive)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().canDoubleJump = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().jumpParticle.SetActive(true);
                StartCoroutine(PowerUpDuration(RespawnTimer));
            }

            else if (HealthPowerUp && PowerUpActive)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().PlayerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().PlayerHP + 5;
                GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().HealingParticle.SetActive(true);
                StartCoroutine(PowerUpDuration(RespawnTimer));
            }
        }

        if (SpeedBoost)
        {
            gameObject.GetComponent<MeshRenderer>().material = SpeedMaterial;
        }
        else if (DoubleJump)
        {
            gameObject.GetComponent<MeshRenderer>().material = JumpMaterial;
        }
        else if (HealthPowerUp)
        {
            gameObject.GetComponent<MeshRenderer>().material = HealthMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerUpActive = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerUpActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerUpActive = true;
        }
    }

    IEnumerator PowerUpDuration(int seconds)
    {
        DestroyPowerUp = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(seconds);

        PowerUpActive = false;
        DestroyPowerUp = false;

        if (OneTimeUse)
        {
            Destroy(gameObject);
        }

        if (DoubleJump)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().canDoubleJump = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().jumpParticle.SetActive(false);
        }
        else if (SpeedBoost)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().playerSpeed = 3.0f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().speedParticle.SetActive(false);
        }
        else if (HealthPowerUp)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().HealingParticle.SetActive(false);
        }
    }

}
