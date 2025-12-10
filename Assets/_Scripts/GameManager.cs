using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int hitPoints;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject[] hearts;
    public static bool endGame;
    public GameObject winScreen;
    public TextMeshProUGUI winTMPText;
    public static string winMessage;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 5;
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame)
        {
            winTMPText.text = winMessage;
            winScreen.SetActive(true);
            return;
        }

        //set the amount of hearts on screen equal to how much health the player has left
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hitPoints)
            {
                hearts[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                hearts[i].GetComponent<Image>().color = Color.black;
            }
        }

        if (hitPoints == 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            pauseScreen.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }

        if (pauseScreen.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public static void loseHealth()
    {
        hitPoints--;
    }

    private void heartShake(GameObject heart, float intensity)
    {
        Vector2 ogLocation = heart.transform.position;
        Vector2 travelPos = new Vector2(transform.position.x + Random.Range(0, 2), transform.position.y + Random.Range(0, 2));

        heart.transform.position = Vector2.MoveTowards(ogLocation, travelPos, intensity);
        heart.transform.position = Vector2.MoveTowards(travelPos, ogLocation, intensity);
    }
}
