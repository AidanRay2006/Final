using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //public variables
    public float gravity = 1f;
    public float jumpForce = 12f;
    public float speed = 6f;
    public float deltaSpeed = 6f;

    //private variables
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D groundCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;

        groundCollider = GetComponent<BoxCollider2D>();

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = rb.velocity;

        //handles moving left and right (taken from assignment 4)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            //makes turning feel better
            if (vel.x > 0)
            {
                vel.x = 0;
            }

            vel.x -= deltaSpeed * Time.deltaTime;
            if (vel.x <= -speed && Grounded())
            {
                vel.x = -speed;
            }
            //move slower in the air
            else
            {
                vel.x = -speed * 0.5f;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            sr.flipX = false;
            //makes turning feel better
            if (vel.x < 0)
            {
                vel.x = 0;
            }

                vel.x += deltaSpeed * Time.deltaTime;
            if (vel.x >= speed && Grounded())
            {
                vel.x = speed;
            }
            //move slower in the air
            else
            {
                vel.x = speed * 0.5f;
            }
        }
        else
        {
            vel.x = 0;
        }

        rb.velocity = vel;

        //handles jumping (taken from assignment 4)
        if (Input.GetKeyDown(KeyCode.UpArrow) && Grounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        //handles the dash
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }

    //taken from assignment 4
    private bool Grounded()
    {
        return groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
