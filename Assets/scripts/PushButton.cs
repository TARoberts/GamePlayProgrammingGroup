using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public Animator animatorButton;
    public Animator animatorDoor;
    public Animator playerAnimator;
    public GameObject player;

    private bool doorOpen = false;
    private bool inZone = false;

    void OnTriggerEnter(Collider other)
    {       
        if (other.tag == "Player")
        {
            inZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
        }
    }
    void Update()
    {
        ThirdPersonMovement movement = player.GetComponent<ThirdPersonMovement>();

        if (Input.GetButtonDown("Interact") && inZone && (movement.controller.velocity.magnitude == 0))
        {
            if (!movement.inCS)
            {

                CSFunction();
            }
        }

    }

    void CSFunction()
    {
        ThirdPersonMovement movement = player.GetComponent<ThirdPersonMovement>();
        
        movement.inCS = true;
        StartCoroutine(Delay());

    }

    IEnumerator Delay()
    {
        ThirdPersonMovement movement = player.GetComponent<ThirdPersonMovement>();
        movement.CSCam1.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        
        playerAnimator.Play("Button", 0, 0.0f);
        yield return new WaitForSeconds(0.5f);
        animatorButton.Play("ButtonPush", 0, 0.0f);

        

        if (!doorOpen)
        {
            movement.CSCam2.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            animatorDoor.Play("Door Slide Down", 0, 0.0f);
            doorOpen = true;
            yield return new WaitForSeconds(2.5f);
          
        }

        movement.CSCam1.gameObject.SetActive(false);
        movement.CSCam2.gameObject.SetActive(false);
        movement.inCS = false;
        movement.inPos = false;
        

        yield return null;
        
    }

}
