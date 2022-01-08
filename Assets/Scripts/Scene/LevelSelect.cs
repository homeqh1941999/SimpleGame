using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is about choosing level to play
/// </summary>
/// 

public class LevelSelect : MonoBehaviour
{
    public int noOfLevels;
    public GameObject levelButton;
    public RectTransform parentPanel;

    int scoreSelect = 0;
    int livesSelect = 2;

    int levelReached;
    string filename;

    // List InputData for saving data into Json file
    List<InputData> entriesData = new List<InputData>();

    private void Awake()
    {
        LevelButton();
        // Get Json file name from GameAssets
        filename = GameAssets.filename;    
    }
    void LevelButton()
    {
        // Save level variable through PlayerPrefs 
        PlayerPrefs.SetInt("level", GameAssets.levelMaxCount + 1);
        levelReached = PlayerPrefs.GetInt("level");

        // Create Button from 1 to number of levels
        for (int i = 0; i < noOfLevels; i++)
        {
            int x = new int();
            x = i;

            GameObject lvButton = Instantiate(levelButton);
            lvButton.transform.SetParent(parentPanel, false);

            Text buttonText = lvButton.GetComponentInChildren<Text>();
            buttonText.text = (i + 1).ToString();

            // Add event listen for Button 
            lvButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                LevelSelected(x);
            });

            // if not reaching higher level yet, the button can't be accessed
            if (i + 1 > levelReached)
            {
                lvButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    // When the button is clicked
    void LevelSelected(int index)
    {
        PlayerPrefs.SetInt("levelSelected", index);
        // Save index of level, scores, lives into GameAssets
        GameAssets.levelCount = index;
        GameAssets.scores = scoreSelect;
        GameAssets.lives = livesSelect;

        Debug.Log("LevelCount: " + GameAssets.levelCount);
        Debug.Log("LevelMaxCount: " + GameAssets.levelMaxCount);

        // Delay 1s for loading game
        Invoke(nameof(LoadGamePlay), 1f);
    }

    void LoadGamePlay()
    {
        SceneManager.LoadSceneAsync("LevelComplete");
    }

    /** Saving and Loading **/
    private void SaveJSonData(int scores, int maxScores, int levelCount, int levelMaxCount, int lives, bool isContinue, bool isReset)
    {
        entriesData.Add(new InputData(scores, maxScores, lives, levelCount, levelMaxCount, isContinue, isReset));
        FileHandler.SaveToJSon<InputData>(entriesData, filename);
        Debug.Log("Save successfully");
    }

    private void OnDestroy()
    {
        SaveJSonData(GameAssets.scores, GameAssets.currentMaxScores, GameAssets.levelCount, GameAssets.levelMaxCount, GameAssets.lives, GameAssets.isContinue, GameAssets.isReset);
    }
}
