using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public SpriteRenderer sr;
    private Vector3 dir;
    private float speed = 2f;
    private float disappearTimer;

    void Start()
    {
        transform.position = new Vector3(Random.Range(5, 15), transform.position.y, 0);
        disappearTimer = 2f;
        dir = Vector3.down;             
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            if (!sr.isVisible)
            {
                Destroy(gameObject);
            }
        }      
    }
}
