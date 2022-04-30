using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpline : MonoBehaviour
{

    [SerializeField] SplineMovement splineMovement;
    [SerializeField] CharacterController player;
    [SerializeField] Transform platform;
    public ThirdPersonMovement moveScript;
    [SerializeField] Animator animator;
    [SerializeField] Transform holder;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            splineMovement.activeSpline = true;
            player.enabled = false;
            player.gameObject.transform.rotation = platform.rotation;
            moveScript.active = false;
            player.transform.parent = platform;
            player.transform.position = holder.position;
            animator.SetBool("Moving", false);
            animator.SetBool("IsGrounded", true);
        }

    }
}
