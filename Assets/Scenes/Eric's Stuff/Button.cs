using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Button : MonoBehaviour
{
    public bool active = false;
    public bool canPress = false;
    public bool canAction = true;

    public GameObject button;
    public GameObject door;
    public GameObject player;

    public Material red;
    public Material green;

    public float moveSpeed = 3.0f;
    public float buttonMoveSpeed = 500.0f;

    public Vector3 position1;
    public Vector3 position2;
    public Vector3 buttonPos1;
    public Vector3 buttonPos2;

    public Camera buttonCamera;
    public Camera playerCamera;

    public Vector3 cameraStart;
    public Vector3 cameraFinish;

    private void Update()
    {
        cameraStart = playerCamera.transform.position;
        if (canPress)
        {
            if (Input.GetButtonDown("Main Attack"))
            {
                if (canAction)
                {
                    StartCoroutine(CameraMovementAsync());
                    StartCoroutine(MoveButton(buttonPos2, buttonPos1));
                    active = !active;
                    canAction = false;
                }
            }
        }

        if (active)
        {
            button.GetComponent<MeshRenderer>().material = green;
            StartCoroutine(MoveDoorAsync(position1));
        }
        else if (!active)
        {
            button.GetComponent<MeshRenderer>().material = red;
            StartCoroutine(MoveDoorAsync(position2));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canPress = !canPress;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPress = false;
    }

    IEnumerator MoveDoorAsync(Vector3 goalPos)
    {
        yield return new WaitForSeconds(0.75f);
        float dist = Vector3.Distance(door.transform.position, goalPos);

        if (dist > 0.0001f)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, goalPos, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator MoveButton(Vector3 goalPos, Vector3 startPos)
    {
        yield return new WaitForSeconds(0.25f);
        float dist = Vector3.Distance(button.transform.position, goalPos);

        if (dist > 0.0001f)
        {
            button.transform.position = Vector3.Lerp(button.transform.position, goalPos, buttonMoveSpeed * Time.deltaTime);
        }

        yield return new WaitForSeconds(0.25f);

        float newDist = Vector3.Distance(button.transform.position, startPos);

        if (newDist > 0.0001f)
        {
            button.transform.position = Vector3.Lerp(button.transform.position, startPos, buttonMoveSpeed * Time.deltaTime);
        }
    }

    IEnumerator CameraMovementAsync()
    {
        player.GetComponent<EricCharacterMovement>().playerSpeed = 0;
        player.GetComponent<EricCharacterMovement>().enabled = false;
        playerCamera.enabled = false;
        buttonCamera.enabled = true;
        Debug.Log("Start");
        yield return new WaitForSeconds(3);

        Debug.Log("I have delayed for 4 seconds");
        player.GetComponent<EricCharacterMovement>().enabled = true;
        player.GetComponent<EricCharacterMovement>().playerSpeed = 3;
        playerCamera.enabled = true;
        buttonCamera.enabled = false;
        canAction = true;
    }
}
