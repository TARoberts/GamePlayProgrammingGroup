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

    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isWandering = false;
    public float rotationSpeed;
    public float movementSpeed;

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
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().PlayerHP == 0)
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

                if (isRotatingRight == true)
                {
                    transform.Rotate(transform.up * Time.deltaTime * rotationSpeed * 50.0f);
                }

                if (isRotatingLeft == true)
                {
                    transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed * 50.0f);
                }

                if (isWalking == true)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * 25.0f);
                }
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().PlayerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<EricCharacterMovement>().PlayerHP - 1;
        }
        yield return new WaitForSeconds(2.0f);
        canAttack = true;
    }

    IEnumerator patrol()
    {
        int RotationTime = Random.Range(1, 4);
        int RotateWait = Random.Range(1, 4);
        int RotateDirection = Random.Range(1, 2);
        int WalkWait = Random.Range(1, 5);
        int WalkTime = Random.Range(1, 4);

        isWandering = true;

        yield return new WaitForSeconds(WalkWait);

        isWalking = true;

        yield return new WaitForSeconds(WalkTime);

        isWalking = false;

        yield return new WaitForSeconds(RotateWait);

        if (RotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(RotationTime);
            isRotatingLeft = false;
        }

        if (RotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(RotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }
}
