using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float degreesPerSecondX = 15.0f;
    public float degreesPerSecondY = 15.0f;
    public float degreesPerSecondZ = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float speed = 5f;
    public Transform start, end, ground;

    public bool rotateX, rotateY, rotateZ;

    public bool bounce;

    public bool moveBetweenPoints;
    private bool atEnd = false;

 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        

        //unlocks the start and end point from the parent
        end.parent = transform.parent;
        start.parent = transform.parent;
        ground.parent = transform.parent;
    }
     
    // Update is called once per frame
    void Update ()
    {

        if (rotateX)
        {
            // Spin object around X-Axis
            transform.Rotate(new Vector3(Time.deltaTime * degreesPerSecondX, 0f, 0f), Space.World);
        }

        if (rotateY)
        {
            // Spin object around Y-Axis
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecondY, 0f), Space.World);
        }

        if (rotateZ)
        {
            // Spin object around Z-Axis
            transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * degreesPerSecondZ), Space.World);
        }

        if (bounce)
        {
            posOffset = transform.position;
            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }

        if (moveBetweenPoints)
        {
            if (!atEnd)
            {
                float step = speed * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, end.position, step);
            }

            else if (atEnd)
            {
                float step = speed * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, start.position, step);
            }

            if (Vector3.Distance(this.transform.position, end.position) < 0.1f)
            {
                atEnd = true;
            }

            else if (Vector3.Distance(this.transform.position, start.position) < 0.1f)
            {
                atEnd = false;
            }


        }

    }
}
