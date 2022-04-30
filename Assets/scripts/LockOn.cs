using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public Transform playerTransform;

    [SerializeField] Cinemachine.CinemachineFreeLook c_VirtualCamera;
    
    Transform targetTransform;
    [SerializeField] GameObject lockedoneffect;
    [SerializeField] Cinemachine.CinemachineTargetGroup targets;
    GameObject lockedTarget;

    bool lockedon = false;


    void Update()
    {
        RaycastHit target;
        if (Input.GetButtonDown("LockOn"))
        {
            if (!lockedon)
            {
                if (Physics.Raycast(transform.position, transform.forward, out target, Mathf.Infinity))
                {
                    if (target.transform.tag == "Enemy")
                    {
                        targetTransform = target.transform;
                        GameObject temp = Instantiate(lockedoneffect, targetTransform);
                        lockedTarget = temp;
                        lockedTarget.transform.position = targetTransform.position;
                        lockedon = true;
                        Debug.Log("hit");

                        targets.AddMember(targetTransform, 1f, 5);
                        //c_VirtualCamera.m_LookAt = targetTransform;
                    }
                    

                }
            }
            else if (lockedon)
            {
                //c_VirtualCamera.LookAt = playerTransform;
                targets.RemoveMember(targetTransform);
                Destroy(lockedTarget);
                lockedon = false;
            }

        }

    }
}
