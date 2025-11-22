using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float hitForce = 12f;
    public float spikeBounceForce;

    private bool swung;
    private float rechargeTime;
    private Vector2 swingPlace;
    private Vector2 downSwingPlace;
    private PlayerMovement player;
    private int playerDir;
    private bool downSwung;

    // Start is called before the first frame update
    void Start()
    {
        rechargeTime = 0;
        swung = false;
        downSwung = false;
        player = FindObjectOfType<PlayerMovement>();
        swingPlace = new Vector2(1, 0);
        downSwingPlace = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.sr.flipX)
        {
            playerDir = -1;
        }
        else
        {
            playerDir = 1;
        }

        if (Input.GetKeyDown(KeyCode.Z) && !swung)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                downSwung = true;
                transform.localEulerAngles = new Vector3(0, 0, 90);
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, downSwingPlace, 1);
            }
            else
            {
                transform.localEulerAngles = Vector3.zero;
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, swingPlace, playerDir);
            }
            rechargeTime = 0;
            swung = true;
        }

        if (swung && rechargeTime >= 0.35f)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 1);
            swung = false;
            downSwung = false;
        }
        else if (downSwung && swung && rechargeTime >= 0.35f)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 1);
            transform.localEulerAngles = Vector3.zero;
            swung = false;
            downSwung = false;
        }
        else
        {
            rechargeTime += Time.deltaTime;
            if (rechargeTime >= 0.35f)
            {
                rechargeTime = 0.35f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;

            int dir = 1;

            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                dir = -1;
            }

            Vector2 forceVector = new Vector2(Vector2.right.x * dir, Vector2.up.y/2);

            if (swung)
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(forceVector * hitForce, ForceMode2D.Impulse);
            }
        }

        if ((collision.transform.CompareTag("Spike") || collision.transform.CompareTag("Enemy")) && downSwung)
        {
            player.Bounce(spikeBounceForce);
        }
    }
}
