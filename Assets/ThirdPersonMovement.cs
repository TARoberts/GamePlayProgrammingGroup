using System.Collections;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;
    bool isJumping;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {  
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded && (velocity.y < 0))
        {
            velocity.y = -2f;

            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
            isJumping = false;
        }

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGrounded", true);
            isJumping = false;
        }

        else if (isJumping)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsGrounded", false);
            
        }

        if (Input.GetButtonDown("Jump") && isGrounded && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping")))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            isJumping = true;
            animator.SetBool("IsJumping", true);
            isGrounded = false;
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGrounded", false);
        }
        else
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
  
        //attack

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            if (Input.GetButtonDown("Attack"))
            {
                animator.SetBool("Attack", true);
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
 

        //move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        float magnitude = direction.magnitude;
        direction.Normalize();

        if (direction.magnitude >= 0.1f)
        {
            animator.SetFloat("moveSpeed", magnitude);
            animator.SetBool("Moving", true);
            
           //direction = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * direction;
            controller.Move(direction * speed* magnitude * Time.deltaTime);
            
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up); 
            controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, toRotation, turnSmoothTime * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
}