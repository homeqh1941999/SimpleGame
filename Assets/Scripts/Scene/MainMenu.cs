using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// This script is about the game's menu interface such as play game, select level, settings, quit game...
/// </summary>
public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Continue;
    public GameObject LoadingProgessInterface;
    public Image loadingProgessBar;

    [SerializeField] string filename;

    List<AsyncOperation> sceneLoad = new List<AsyncOperation>();

    List<InputData> entriesData = new List<InputData>();
    
    private void Awake()
    {
        // Load data from Json file before start the game
        LoadJSonData();
    }
    private void Start()
    {
        // check to see if the game is continue to play or restart a new play
        if (!GameAssets.isContinue)
        {
            Continue.SetActive(false);
        }
    }
    // Play game or Restart Game
    public void PlayGame()
    {
        //List<int> entriesScoreData = new List<int>();

        // create var variable for the first time to start the game or restart the game. 
        var entriesScoreData = 0;
        var levelCount = 0;
        var levelMaxCount = 0;
        var lives = 2;
        var currentMaxScores = 0;

        var isContinue = true;
        var isReset = true;

        // Save all variable into Json file...
        SaveJSonData(entriesScoreData, currentMaxScores, levelCount, levelMaxCount, lives, isContinue, isReset);
        //... and then load it again to retrieve new data.
        LoadJSonData();
        // Hide the menu panel...
        HideMenu();
        //... and then show the loading screen
        ShowLoadingScreen();
        // Move to LevelComplete Scene
        sceneLoad.Add(SceneManager.LoadSceneAsync("LevelComplete"));
        // Delay for loading scene
        StartCoroutine(LoadingScreen(sceneLoad));
    }
    // Continue to play game from previous level
    public void ContinueGame()
    {
        HideMenu();
        ShowLoadingScreen();

        sceneLoad.Add(SceneManager.LoadSceneAsync("LevelComplete"));
        StartCoroutine(LoadingScreen(sceneLoad));
    }
    private void HideMenu()
    {
        Menu.SetActive(false);
    }
    private void ShowLoadingScreen()
    {
        LoadingProgessInterface.SetActive(true);
    }
    public void GotoSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    // List and show loading progess
    IEnumerator LoadingScreen(List<AsyncOperation> sceneLoad)
    {
        float totalProgress = 0;
        for (int i = 0; i < sceneLoad.Count; ++i)
        {
            while (!sceneLoad[i].isDone)
            {
                totalProgress += sceneLoad[i].progress;
                loadingProgessBar.fillAmount = totalProgress / sceneLoad.Count;
                yield return null;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    /** Saving and Loading**/
    private void SaveJSonData(int scores, int maxScores, int levelCount, int levelMaxCount, int lives, bool isContinue, bool isReset)
    {
        entriesData.Add(new InputData(scores, maxScores, lives, levelCount, levelMaxCount, isContinue, isReset));
        FileHandler.SaveToJSon<InputData>(entriesData, filename);
        Debug.Log("Save successfully");
    }

    private void LoadJSonData()
    {
        entriesData = FileHandler.LoadListFromJSon<InputData>(filename);
        foreach (InputData entries in entriesData)
        {
            GameAssets.levelCount = entries.levelCount;
            GameAssets.levelMaxCount = entries.levelMaxCount;
            GameAssets.lives = entries.lives;
            GameAssets.scores = entries.scores;
            GameAssets.currentMaxScores = entries.maxScores;
            GameAssets.isContinue = entries.isContinue;
            GameAssets.isReset = entries.isReset;
        }
        Debug.Log("Load successfully");
    }
}
