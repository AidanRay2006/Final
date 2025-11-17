using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public bool moving = false;
    public float minDistance = 0.1f;
    public float speed = 2f;

    private bool movingToPointA;
    private bool seePlayer;
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        //stolen from assignment 4

        if (!moving)
        {
            return;
        }

        //if the enemy sees the player, it will walk towards it
        if (seePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 1.5f * Time.deltaTime);
            return;
        }
        //otherwise, it will continue on its path
        else
        {
            CheckDirection();
        }

        if (movingToPointA)
        {
            transform.eulerAngles = Vector3.zero;
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            seePlayer = true;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            seePlayer = false;
        }
    }

    private void CheckDirection()
    {
        //make sure that it is moving to point a or not
        if (movingToPointA && !seePlayer)
        {
            if (Vector2.Distance(transform.position, pointA.position) <= minDistance)
            {
                movingToPointA = false;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, pointB.position) <= minDistance)
            {
                movingToPointA = true;
            }
        }
    }
}
