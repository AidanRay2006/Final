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
    public PlayerDetector playerDetecor;

    private bool movingToPointA;
    private GameObject player;
    private bool seePlayer;

    // Update is called once per frame
    void Update()
    {
        //some code stolen from assignment 4

        player = playerDetecor.player;
        seePlayer = playerDetecor.seePlayer;

        if (!moving)
        {
            return;
        }

        //if the enemy sees the player, it will walk towards it
        if (seePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * 2.5f * Time.deltaTime);
            return;
        }
        //otherwise, it will continue on its path
        else
        {
            CheckDirection();
        }

        //assumes point a is always to the right
        if (movingToPointA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (transform.position.x > pointA.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (transform.position.x < pointB.position.x)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
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
