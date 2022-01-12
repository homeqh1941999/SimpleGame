using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        SetLives(GameAssets.lives);
    }
    public void SetLives(int _lives)
    {
        if (_lives < 0)
        {
            GameAssets.isGameOver = true;
        }
        else
        {
            textMesh.SetText("Lives: " + _lives.ToString());
        }
    }
}
