using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI textMesh;  
    public float _timer;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
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
