using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameAssets is a gameObject which will save all variable in the whole game.
public class GameAssets : MonoBehaviour
{
    public static int levelCount;
    public static int levelMaxCount;
    public static int lives;
    public static int scores;
    public static int currentMaxScores;

    public static bool isWin;
    public static bool isGameOver;
    public static bool isContinue;
    public static bool isReset;

    public static string filename;

    private static GameAssets _i;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        filename = "SaveData.json";
    }
    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));            
            return _i;
        }
    }

    public Transform pointPopup;
    public Transform levelTMP;
    public Transform scoreTMP;
    public Transform livesTMP;
    public Transform timerTMP;
    public GameObject Player;
}
