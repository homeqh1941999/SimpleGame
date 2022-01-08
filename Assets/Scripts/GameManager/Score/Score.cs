using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Create(Vector3 position)
    {
        Transform scoreTransform = Instantiate(GameAssets.i.scoreTMP, position, Quaternion.identity);

        Score score = scoreTransform.GetComponent<Score>();

        return score;
    }

    private TextMeshPro textMesh;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        SetScore(GameAssets.scores);
    }
    public void SetScore(int _scores)
    {
        textMesh.SetText("Score: " + _scores.ToString("D7"));
    }
}
