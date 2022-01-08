using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will hold all the basic input about the game like score, level, life,...
[Serializable]
public class InputData
{
    public int maxScores;
    public int lives;
    public int levelCount;
    public int levelMaxCount;
    public int scores;

    public bool isContinue;
    public bool isReset;

    public InputData(int scores, int maxScores, int lives, int levelCount, int levelMaxCount, bool isContinue, bool isReset)
    {
        this.scores = scores;
        this.maxScores = maxScores;
        this.lives = lives;
        this.levelMaxCount = levelMaxCount;
        this.levelCount = levelCount;
        this.isContinue = isContinue;
        this.isReset = isReset;
    }
}
[Serializable]
public class InputScore
{
    public int lives;
}

