using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    int limitChildCount;

    private void Start()
    {
        limitChildCount = transform.childCount;
    }
    void Update()
    {
        ElementCheck();
    }

    // Check if there ís a child element
    private void ElementCheck()
    {
        if (transform.childCount < 0)
        {
            LevelControl.isPartDestroy = true;
            Destroy(gameObject);
        }            
    }

    private void OnDestroy()
    {
        LevelControl.isPartDestroy = true;
    }
}
