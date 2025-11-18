using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.sr.flipX)
        {
            playerDir = -1;
        }
        else
        {
            playerDir = 1;
        }
        swingPlace = new Vector2(1, 0);


        if (Input.GetKeyDown(KeyCode.Z) && !swung)
        {
            swung = true;
            transform.position = Vector2.MoveTowards(transform.position, swingPlace, playerDir);
            rechargeTime = 0;
        }

        if (swung && rechargeTime >= 0.35f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 1);
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
}
