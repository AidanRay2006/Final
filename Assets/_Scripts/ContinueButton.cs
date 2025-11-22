using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public GameObject pauseScreen;

    public void Continue()
    {
        pauseScreen.SetActive(false);
    }
}
