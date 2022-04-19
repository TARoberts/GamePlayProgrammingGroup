using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPush : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public bool cutscene;


    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Button"))
        {
            if (cutscene)
            {
                animator.SetBool("pushButton", true);
            }
            else
            {
                animator.SetBool("pushButton", false);
            }
        }
    }
}
