using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is about Game Play such as loading level, check if the game is end,...
/// </summary>
/// 
public class GameManager : MonoBehaviour
{
    public GameObject go_GameOver;
    public GameObject go_GameUI;
    public GameObject go_GameComplete;
    public GameObject go_GamePause;

    public GameObject go_LevelChangeBackground;
    public Button btnPause;

    public GameObject[] pf_LevelLoading;
    private Player pf_Player;

    private float delayTime = 2f;

    bool isLevelLoaded;
    bool isPaused = false;

    string filename;
    GameObject _currentLevel;

    List<InputData> entriesData = new List<InputData>();

    private void Awake()
    {
        // Set up variable for GameAssets
        GameAssets.isGameOver = false;
        GameAssets.isWin = false;
        filename = GameAssets.filename;
    }

    void Start()
    {
        // Check if level is loaded...
        if (isLevelLoaded)
        {
            isLevelLoaded = false;
        }

        // Show Game Play panel
        ShowGamePlay();

        // Hide Level Change Background
        HideLevelChangeBackground();
    }
    // Check for respawning player if player still has lives
    private void FixedUpdate()
    {
        RespawnPlayer();
    }
    // Check if level is loaded...
    private void Update()
    {
        // level is not loaded then respawn new level...
        if (!isLevelLoaded)
        {
            StartCoroutine(RespawnLevel());
        }
        // ... or check winning for this level
        else
        {
            StartCoroutine(isWinning());
        }
    }

    // Check if the game is over or respawn new player
    private void RespawnPlayer()
    {
        if (GameAssets.isGameOver)
        {
            go_GameOver.SetActive(true);
        }
        else if (pf_Player == null)
        {
            Instantiate(GameAssets.i.Player);
            pf_Player = FindObjectOfType<Player>();
        }
    }

    // Check if level is limited and respawn next level
    IEnumerator RespawnLevel()
    {
        isLevelLoaded = true;
        if (GameAssets.levelCount < pf_LevelLoading.Length)
        {
            if (_currentLevel == null)
            {
                yield return new WaitForSeconds(delayTime);
                _currentLevel = Instantiate(pf_LevelLoading[GameAssets.levelCount]);
                _currentLevel.name = pf_LevelLoading[GameAssets.levelCount].name;
            }
        }
        else
        {
            Debug.Log("New level is coming soon...");
            yield return new WaitForSeconds(delayTime);
        }
    }

    // Check if player is winning
    IEnumerator isWinning()
    {
        // if current scores is greater than current max scores, the current max scores will be the current scores.
        if (GameAssets.scores > GameAssets.currentMaxScores)
        {
            GameAssets.currentMaxScores = GameAssets.scores;
        }

        // if current level is greater than the max level reached, the max level reached will be the current level.
        if (GameAssets.levelCount > GameAssets.levelMaxCount)
        {
            GameAssets.levelMaxCount = GameAssets.levelCount;
        }

        // Delay belong to delayTime
        yield return new WaitForSeconds(delayTime);

        // Check if player is winning, save data to Json file and show Game Complete panel.
        if (GameAssets.isWin)
        {
            Destroy(_currentLevel);
            GameAssets.levelCount++;
            GameAssets.isWin = false;
            SaveJSonData(GameAssets.scores, GameAssets.currentMaxScores, GameAssets.levelCount, GameAssets.levelMaxCount, 
                GameAssets.lives, GameAssets.isContinue, GameAssets.isReset);
            go_GameComplete.SetActive(true);          
        }    
    }

    private void ShowGamePlay()
    {
        go_GameUI.SetActive(true);
    }

    private void HideLevelChangeBackground()
    {
        go_LevelChangeBackground.SetActive(false);
    }

    // not done yet
    public void RetryGame()
    {
        GameAssets.lives = 2;
        GameAssets.scores = 0;

        SceneManager.LoadSceneAsync("LevelComplete");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync("LevelComplete");
    }

    public void SelectLevel()
    {
        PauseGame();
        SceneManager.LoadSceneAsync("LevelSelect");
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            go_GamePause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            go_GamePause.SetActive(false);
        }
        btnPause.gameObject.SetActive(!isPaused);
    }

    public void MainMenu()
    {
        PauseGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }


    //** Saving and Loading **//
    private void SaveJSonData(int scores, int maxScores, int levelCount, int levelMaxCount, int lives, bool isContinue, bool isReset)
    {
        entriesData.Add(new InputData(scores, maxScores, lives, levelCount, levelMaxCount, isContinue, isReset));
        FileHandler.SaveToJSon<InputData>(entriesData, filename);
        Debug.Log("Save successfully");
    }
    private void OnApplicationQuit()
    {
        SaveJSonData(GameAssets.scores, GameAssets.currentMaxScores, GameAssets.levelCount, GameAssets.levelMaxCount,
            GameAssets.lives, GameAssets.isContinue, GameAssets.isReset);
    }
}
