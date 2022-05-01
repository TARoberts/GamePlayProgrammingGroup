using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLookAt : MonoBehaviour
{

    Animator animator;
    public bool isActive = false;
    public Transform objTarget;
 
    public float lookweight;

    //GameObject objLock = GameObject.FindGameObjectWithTag("Lock");

    GameObject objPivot;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();



        objPivot = new GameObject("DummyP");
        objPivot.transform.parent = transform.parent;
        objPivot.transform.localPosition = new Vector3(0, 1.46f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 objects = objLock.transform.position - transform.position;
        objPivot.transform.LookAt(objTarget);
        float pivotRotY = objPivot.transform.localRotation.y;
        //Debug.Log(pivotRotY);

        float dist = Vector3.Distance(objPivot.transform.position, objTarget.position);
        //Debug.Log(dist);

        if(pivotRotY < 0.65f && pivotRotY > -0.65f && dist < 8.0f)
        {
            lookweight = Mathf.Lerp(lookweight, 1, Time.deltaTime * 2.5f);
        }
        else
        {
            lookweight = Mathf.Lerp(lookweight, 0, Time.deltaTime * 2.5f);
        }
    }

    private void OnAnimatorIK()
    {
        if(animator)
        {
            if(isActive)
            {
                if(objTarget != null)
                {
                    animator.SetLookAtWeight(lookweight);
                    animator.SetLookAtPosition(objTarget.position);
                }
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
