using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;

    private VariableJoystick joystick;

    public float radius = 10f;
    public float speed = 3f;

    bool isDead;
    bool isAppear;

    private SpriteRenderer sr_RocketBody;
    private SpriteRenderer sr_Flame;
    private Color colorTrans;

    void Start()
    {
        joystick = FindObjectOfType<VariableJoystick>();
        sr_RocketBody = GameObject.Find("RocketBody").GetComponent<SpriteRenderer>();
        sr_Flame = GameObject.Find("Flame").GetComponent<SpriteRenderer>();

        // Take min and max device screen
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        colorTrans = sr_RocketBody.color;
        colorTrans = sr_Flame.color;

        StartCoroutine(Appear(sr_RocketBody));
        StartCoroutine(Appear(sr_Flame));
    }

    void Update()
    {
        // Player's movement
        Movement();
        Dead();
    }

    private void Movement()
    {
        float hor = joystick.Horizontal;
        float ver = joystick.Vertical;
        Vector3 direction = new Vector3(hor, ver, 0).normalized;
        transform.Translate(direction * 0.08f, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 3.5f, maxScreenBounds.x - 3f),
                Mathf.Clamp(transform.position.y, minScreenBounds.y + 0.5f, maxScreenBounds.y - 2), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAppear)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (isDead)
                {
                    return;
                }
                isDead = true;
            }
        }
    }

    private void Dead()
    {
        if (isDead)
        {
            isDead = false;
            GameAssets.lives--;
            Destroy(gameObject);
        }

    }

    IEnumerator Appear(SpriteRenderer sr)
    {
        float time = 0;
        while(time < 2)
        {
            time += Time.deltaTime + 0.2f;
            yield return new WaitForSeconds(0.15f);
            
            colorTrans.a = 0;
            sr.color = colorTrans;

            yield return new WaitForSeconds(0.15f);
        
            colorTrans.a = 1;
            sr.color = colorTrans;
        }
        isAppear = true;
    }
}
