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
    public float dashForce = 12f;
    public SpriteRenderer sr;
    public bool dashed;
    public bool flipped;
    public bool hit;

    //private variables
    private Rigidbody2D rb;
    private BoxCollider2D groundCollider;
    private bool touchedGround;
    private float dashRecharge;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;

        groundCollider = GetComponent<BoxCollider2D>();

        sr = GetComponent<SpriteRenderer>();

        dashed = false;
        dashRecharge = 0;

        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = rb.velocity;

        if (hit)
        {
            return;
        }

        //handles moving left and right (taken from assignment 4)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            flipped = true;
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
            flipped = false;
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
            Bounce(jumpForce);
        }

        //handles the dash
        if (Input.GetKeyDown(KeyCode.X) && !dashed)
        {
            if (sr.flipX)
            {
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            }
            dashed = true;
            touchedGround = false;
            dashRecharge = 0;
        }

        //make sure enough time has passed between when the player last dashed
        //so that they can dash again, and they have touched the ground since then
        if (dashed && dashRecharge == 0.5f && touchedGround)
        {
            dashed = false;
        }
        else
        {
            dashRecharge += Time.deltaTime;
            //dashRecharge cannot exceed 0.5
            if (dashRecharge >= 0.5f)
            {
                dashRecharge = 0.5f;
            }
        }
        //fixes a bug where if you had touched the ground, but tried to dash
        //in the air before the time ran out you couldn't
        if (Grounded())
        {
            touchedGround = true;
        }

        sr.flipX = flipped;
    }

    //some debugging help thanks to ChatGPT
    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        return hit.collider.CompareTag("Ground");
    }

    public void Bounce(float bounceForce)
    {
        rb.AddForce(Vector3.up * bounceForce, ForceMode2D.Impulse);
    }
}
