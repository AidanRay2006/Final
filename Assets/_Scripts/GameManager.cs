using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int hitPoints;
    public TextMeshProUGUI hpText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 5;
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"HP:{hitPoints}";

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
}
