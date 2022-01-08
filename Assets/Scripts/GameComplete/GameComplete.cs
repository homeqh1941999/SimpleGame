using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{

    private Text Score;
    private Text HighScore;
    private Text HighScoreText;

    void Start()
    {
        Score = GameObject.Find("Score").GetComponent<Text>();
        HighScore = GameObject.Find("HighScore").GetComponent<Text>();
        HighScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();

        Score.text = "Your Score: " + GameAssets.scores.ToString();
        HighScore.text = "High Score: " + GameAssets.currentMaxScores.ToString();
    }


}
