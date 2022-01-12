using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{ 
    private TextMeshProUGUI textMesh;
    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
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
