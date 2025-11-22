using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetector : MonoBehaviour
{
    public float hitForce;
    
    private PlayerMovement player;
    private bool hit;
    private float recharge;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        
        recharge = 0;
        hit = false;
    }

    private void Update()
    {
        player.hit = hit;

        //gives the player some invulnerability after being hit
        if (hit && recharge == 0.4f)
        {
            hit = false;
        }
        else
        {
            recharge += Time.deltaTime;
            if(recharge >= 0.4f)
            {
                recharge = 0.4f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if ((collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Spike")) && !hit)
        {
            int dir = 1;

            if (player.flipped)
            {
                dir = -1;
            }

            hit = true;

            Vector2 forceVector = new Vector2(Vector2.right.x * dir, Vector2.up.y/2f);

            player.GetComponent<Rigidbody2D>().AddForce(forceVector * hitForce, ForceMode2D.Impulse);

            recharge = 0;

            GameManager.loseHealth();
        }
    }
}
