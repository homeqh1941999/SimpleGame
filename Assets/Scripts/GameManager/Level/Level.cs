using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Create(Vector3 position, int level)
    {
        Transform levelTransform = Instantiate(GameAssets.i.levelTMP, position, Quaternion.identity);

        Level levels = levelTransform.GetComponent<Level>();
        levels.SetLevel(level);

        return levels;
    }

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void SetLevel(int level)
    {
        textMesh.SetText("Level " + level.ToString());
    }

    public void LevelTMP_Delete()
    {
        Destroy(gameObject);
    }
}
