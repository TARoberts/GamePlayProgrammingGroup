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
    public GameObject CSCam1, CSCam2;

    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public float dashSpeed;
    public float dashTime;
    public bool doubleJump;
    public bool active = true;

    Vector3 velocity;
    bool isGrounded;
    bool isJumping;
    Vector3 direction;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;

    public Animator animator;
    public Rigidbody rb;
    public GameObject weapons;
    public GameObject place;

    public bool inCS = false;
    public bool inPos = false;


    private void Start()
    {
        active = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (inCS)
            {
                if (!inPos)
                {
                    SetPosition();
                }

                animator.SetBool("Moving", false);
                weapons.SetActive(false);
                rb.freezeRotation = true;

            }

            else
            {

                rb.freezeRotation = false;
                if (!weapons.active)
                {
                    StartCoroutine(Delay());
                    weapons.SetActive(true);
                }

                CSCam1.SetActive(false);

                //jump
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (!isGrounded && (velocity.y < 0))
                {
                    velocity.y = -2f;

                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", true);

                }

                if (isGrounded)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    animator.SetBool("IsGrounded", true);

                }

                else
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsGrounded", false);

                }

                if (Input.GetButtonDown("Jump") && (isGrounded))
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

                    animator.SetBool("IsJumping", true);
                    isGrounded = false;
                    animator.SetBool("IsFalling", false);
                    animator.SetBool("IsGrounded", false);
                }

                else if (Input.GetButtonDown("Jump") && doubleJump)
                {
                    doubleJump = false;
                    velocity.y = Mathf.Sqrt(jumpHeight * 4 * -2 * gravity);

                    animator.SetBool("IsJumping", true);
                    isGrounded = false;
                    animator.SetBool("IsFalling", false);
                }

                else
                {

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
                direction = new Vector3(horizontal, 0f, vertical);
                float magnitude = direction.magnitude;
                direction.Normalize();

                if (direction.magnitude >= 0.1f)
                {
                    animator.SetFloat("Animation Speed", 1);
                    animator.SetFloat("moveSpeed", magnitude);
                    animator.SetBool("Moving", true);

                    direction = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * direction;

                    controller.Move(direction * speed * magnitude * Time.deltaTime);

                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, toRotation, turnSmoothTime * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("Moving", false);
                    animator.SetFloat("Animation Speed", 1);
                }

                //dash

                if (Input.GetButtonDown("Dash") && isGrounded)
                {
                    StartCoroutine(Dash());

                }
            }
        }
       
    }

    void SetPosition()
    {
        controller.enabled = false;
        controller.transform.position = place.transform.position;
        controller.transform.rotation = place.transform.rotation;
        controller.enabled = true;
        inPos = true;

    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            animator.SetFloat("Animation Speed", 2);
            controller.Move(direction * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }
}