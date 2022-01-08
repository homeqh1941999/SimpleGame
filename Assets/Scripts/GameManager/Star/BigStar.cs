using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStar : MonoBehaviour
{
    private RectTransform rt;

    private Vector3 orgScale;
    private Vector3 adjScale = new Vector3(1.2f, 1.2f, 1.2f);

    float limitTime = 0.4f;
    float counter;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        orgScale = rt.localScale;
        counter = limitTime;
    }
    // Update is called once per frame
    void Update()
    {
        Twink();
    }
    void Twink()
    {
        counter -= Time.deltaTime;
        if(counter < limitTime/2)
        {
            rt.localScale = adjScale;
            if(counter < 0)
            {
                counter = limitTime;
            }
        }
        else
        {
            rt.localScale = orgScale;
        }
    }
}
