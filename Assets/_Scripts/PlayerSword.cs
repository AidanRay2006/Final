using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float hitForce = 12f;

    private bool swung;
    private float rechargeTime;
    private Vector2 swingPlace;
    private PlayerMovement player;
    private int playerDir;

    // Start is called before the first frame update
    void Start()
    {
        rechargeTime = 0;
        swung = false;
        player = FindObjectOfType<PlayerMovement>();
        swingPlace = new Vector2(1, 0);
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
            swung = true;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, swingPlace, playerDir);
            rechargeTime = 0;
        }

        if (swung && rechargeTime >= 0.35f)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 1);
            swung = false;
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
    }
}
