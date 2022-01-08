using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private Level levelTMP;

    void Start()
    {
        Level.Create(transform.position, GameAssets.levelCount + 1);
        Invoke(nameof(LoadGamePlay), 5f);               
    }

    private void LoadGamePlay()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }
}
