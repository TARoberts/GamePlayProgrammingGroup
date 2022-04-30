using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMovement : MonoBehaviour
{
    private Vector3[] splinePoints;
    private int noSplines;
    private int currentSpline = 0; 

    public bool debugSpline = true;
    public bool activeSpline;

    private Transform startPos;

    [SerializeField] private Transform platform;
    [SerializeField] CharacterController player;
    [SerializeField] public ThirdPersonMovement moveScript;
    [SerializeField] GameObject splineCam, mainCam;

    private void Start()
    {
        startPos = transform;
        activeSpline = false;
        noSplines = transform.childCount;
        splinePoints = new Vector3[noSplines];


        for (int i = 0; i < noSplines; i++)
        {
            splinePoints[i] = transform.GetChild(i).position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < noSplines; i++)
        {
            splinePoints[i] = transform.GetChild(i).position;
        }

        if (noSplines > 1 && debugSpline)
        {
            for (int i = 0; i < noSplines; i++)
            {
                if (i+1 < noSplines)
                {
                    Debug.DrawLine(splinePoints[i], splinePoints[i + 1], Color.red);
                }
                
            }
        }

        if (activeSpline)
        {
            float step = 5 * Time.deltaTime;
            splineCam.SetActive(true);
            mainCam.SetActive(false);

            if (Input.GetAxisRaw("Horizontal") > 0.1f || Input.GetAxisRaw("Horizontal") < -0.1f)
            {
                Vector3 position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z + (Input.GetAxisRaw("Vertical")* 5));
                transform.position = position;
                
            }

            else
            {
                
                transform.position = startPos.position;
            }

                
            platform.position = Vector3.MoveTowards(platform.position, splinePoints[currentSpline], step);

            if (Vector3.Distance(platform.position, splinePoints[currentSpline]) < 0.1f)
            {
                if (currentSpline < noSplines)
                {
                    currentSpline++;
                }

            }
            if (currentSpline == noSplines)
            {
                player.transform.parent = null;
                moveScript.active = true;
                activeSpline = false;
                player.enabled = true;
                mainCam.SetActive(true);
                splineCam.SetActive(false);
            }
        }



    }
}
