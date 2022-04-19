using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Combat : MonoBehaviour
{
    
    public int startingHP;
    public int hp;
    private float range = 3;
    private bool hitStun = false;
    [SerializeField] Animator playerAnimator, myAnimator;
    private GameObject player, me;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Rigidbody body;
    [SerializeField] AIScript brain;


    private void Start()
    {
        
        hp = startingHP;
        me = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        if (hp == 2)
        {
            me.transform.localScale = new Vector3 (2f, 2f, 2f);
            particle.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            range = 2.4f;
        }
        else if (hp == 1)
        {
            me.transform.localScale = new Vector3(1f, 1f, 1f);
            particle.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            range = 1.8f;
        }
        brain.range = range;
    }
    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("ded");

            if (startingHP > 1)
            {
                startingHP--;
                hp = startingHP;
                Instantiate(me, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity);

                Instantiate(me, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
            }
            StartCoroutine(die());
            
        }
        if (!hitStun)
        {
            attacked();
        }
    }

    IEnumerator attack()
    {
        hp--;

        yield return new WaitForSeconds(1.0f);
        hitStun = false;
    }
    IEnumerator die()
    {
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(me);
    }
    void attacked()
    {
        Vector3 forward = player.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = player.transform.position - transform.position;
        if (Vector3.Dot(forward, toOther) < -0.5f)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < range)
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                {
                    particle.Play();
                    toOther.y = 0;
                    body.AddForce(toOther.normalized * -5.0f, ForceMode.Impulse);
                    hitStun = true;
                    StartCoroutine(attack());
                }
                
            }
        }
    }
}
