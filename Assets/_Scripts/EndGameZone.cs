using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameZone : MonoBehaviour
{
    public string message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.winMessage = message;
            GameManager.endGame = true;
        }
    }
}
