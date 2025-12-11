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
    public PlayerDetector playerDetector;
    public int hitPoints;
    public AudioSource hitSound;

    private bool movingToPointA;
    private GameObject player;
    private bool seePlayer;
    private Animator animator;
    private SpriteRenderer sr;
    public float colorTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        colorTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        colorTime += Time.deltaTime;
        //makes the enemy flash when hit
        if (colorTime >= 0.15f)
        {
            sr.color = Color.white;
        }

        //some code stolen from assignment 4

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }

        player = playerDetector.player;
        seePlayer = playerDetector.seePlayer;

        if (!moving)
        {
            return;
        }

        //if the enemy sees the player, it will walk towards it
        if (seePlayer)
        {
            animator.speed = 2;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * 2.5f * Time.deltaTime);
            return;
        }
        //if the enemy has fallen far away from its movement points
        else if (transform.position.y < pointB.transform.position.y - 2f || transform.position.y > pointB.transform.position.y + 2f)
        {
            animator.speed = 1;
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            return;
        }
        animator.speed = 1;
        //otherwise, it will continue on its path
        CheckDirection();

        //assumes point a is always to the right
        if (movingToPointA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (transform.position.x > pointA.position.x)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (transform.position.x < pointB.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
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

    public void loseHealth()
    {
        hitPoints--;
        colorTime = 0;
        sr.color = Color.red;
    }
}
