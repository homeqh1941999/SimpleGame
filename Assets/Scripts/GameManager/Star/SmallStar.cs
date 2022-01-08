using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStar : MonoBehaviour
{
    private RectTransform rt;

    private Vector3 yRotation;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    
    void Update()
    {
        yRotation += new Vector3(0, 1, 1) * 90 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(yRotation);
    }
}
