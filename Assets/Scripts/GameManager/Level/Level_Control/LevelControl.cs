using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    private Vector3 dir;
    private Vector3 orgTransform;
    private float endPoint;


    public static bool isPartDestroy;
    void Start()
    {
        dir = Vector3.down;
        endPoint = transform.position.y * 3/5;
        orgTransform = transform.position;
        isPartDestroy = false;
    }

    void Update()
    {
        GoingDown();
        isWinning();
    }

    private void GoingDown()
    {
        transform.Translate(dir * Time.deltaTime);
        if (isPartDestroy)
        {        
            endPoint = endPoint - orgTransform.y * 2/5;
            dir = Vector3.down;
            isPartDestroy = false;
        }
        else if (transform.position.y < endPoint)
        {
            dir = Vector3.zero;
        }
    }

    private void isWinning()
    {
        if (transform.childCount <= 0)
        {         
            Destroy(gameObject);
        }            
    }

    void OnDestroy()
    {
        GameAssets.isWin = true;
    }
}
