using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public enum state { idle, look, chase, attack};
    public state AIState = state.idle;
    public float distance;
    private GameObject player;
    [SerializeField] player_combat combat;
    [SerializeField] Rigidbody body;
    

    public float speed = 3;
    private bool canAttack = true;
    private bool patrolling = true;
    public float range = 3.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = 2000;

        Vector3 toOther = player.transform.position - transform.position;
        body.AddForce(toOther.normalized * -2.0f, ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if (combat.HP == 0)
        {
            distance = 2000;
        }

        if (distance > 18.0f)
        {
            AIState = state.idle;
        }

        else if (distance > 15f && distance <= 18.0f)
        {
            AIState = state.look;
        }

        else if (distance > range && distance <= 15f)
        {
            AIState = state.chase;
        }

        else if (distance <= range)
        {
            AIState = state.attack;
        }

        if (AIState == state.idle)
        {
            if (patrolling)
            {
                patrolling = false;
                StartCoroutine(patrol());
            } 
        }

        else if (AIState == state.look)
        {
            Vector3 relativePos = player.transform.position - transform.position;

            relativePos.y = 0;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }

        else if (AIState == state.chase)
        {
            float step = speed * Time.deltaTime;
            Vector3 relativePos = player.transform.position - transform.position;

            relativePos.y = 0;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            Vector3 newPos = Vector3.MoveTowards(transform.position, player.transform.position, step);

            newPos.y = transform.position.y;

            transform.position = newPos;
        }

        else if (AIState == state.attack)
        {
            if (canAttack && combat.HP > 0)
            {
                canAttack = false;
                StartCoroutine(attack());
            }
        }
    }

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = 0;
        var z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }
    IEnumerator attack()
    {
        Vector3 toOther = player.transform.position - transform.position;
        if (combat.iFrame == false)
        {
            body.AddForce(toOther.normalized * -2.0f, ForceMode.Impulse);
            combat.HP = combat.HP - 1;
        }
        yield return new WaitForSeconds(2.0f);
        canAttack = true;
    }

    IEnumerator patrol()
    {
        body.velocity = RandomVector(-5f, 5f);
        yield return new WaitForSeconds(2.5f);
        patrolling = true;
        yield return null;
    }
}
