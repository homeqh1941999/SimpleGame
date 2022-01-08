using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{ 
    public static Point Create(Vector3 position, float points)
    {
        Transform pointPopupTransform = Instantiate(GameAssets.i.pointPopup, position, Quaternion.identity);

        Point point = pointPopupTransform.GetComponent<Point>();
        point.SetPoint(points);

        return point;
    }
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void SetPoint(float points)
    {
        textMesh.SetText(points.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 0.5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }
}
