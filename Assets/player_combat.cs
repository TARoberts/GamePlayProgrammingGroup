using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat : MonoBehaviour
{
    public int HP;
    [SerializeField] Animator playerAnimator;
    /*[SerializeField] AI_Combat aiCombat;*/
    [SerializeField] CharacterController player;
    public bool iFrame = false;
    [SerializeField] GameObject spawn;

    private Transform enemy = null;

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            iFrame = true;
        }
        else
        {
            iFrame = false;
        }
        if (HP <= 0)
        {
            playerAnimator.SetBool("Dead", true);
            player.enabled = false;
            
            StartCoroutine(respawn());
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3.0f);
        playerAnimator.SetBool("Dead", false);
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        HP = 5;
        player.enabled = true;
        yield return null;
    }
}
