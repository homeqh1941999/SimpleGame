using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public void goToHome()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 1;
    }
    public void goToSelect()
    {
        SceneManager.LoadSceneAsync("LevelSelect");
        Time.timeScale = 1;
    }
}
