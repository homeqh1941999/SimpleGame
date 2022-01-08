using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Create(Vector3 position)
    {
        Transform timerTransform = Instantiate(GameAssets.i.timerTMP, position, Quaternion.identity);

        Timer timer = timerTransform.GetComponent<Timer>();

        return timer;
    }

    private TextMeshPro textMesh;
    
    public float _timer;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {       
        SetTimer();
    }
    public void SetTimer()
    {
        _timer -= Time.deltaTime;
        if(_timer < 0)
        {
            Debug.Log("GameOver");
        }
        else
        {
            textMesh.SetText(_timer.ToString("0.00"));
        }       
    }
}
