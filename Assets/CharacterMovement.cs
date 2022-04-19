using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 4f;
    public float turnSmooth = .1f;
    private float turnSmoothness;

    public Transform gameCam;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0 , vertical).normalized;
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameCam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothness, turnSmooth);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 relMove = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 

            controller.Move(relMove.normalized * moveSpeed * Time.deltaTime);
        }

    }
}
