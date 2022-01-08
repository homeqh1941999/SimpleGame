using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public static Lives Create(Vector3 position)
    {
        Transform livesTransform = Instantiate(GameAssets.i.livesTMP, position, Quaternion.identity);

        Lives lives = livesTransform.GetComponent<Lives>();

        return lives;
    }

    private TextMeshPro textMesh;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
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
