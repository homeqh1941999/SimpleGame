using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    [SerializeField] private float enemySpeed;
    [SerializeField] private int scores;

    //[SerializeField] private float volume = 0.3f;

    [SerializeField] private AudioClip adcExplosionSound;
    [SerializeField] private AudioClip adcHitSound;

    public Rigidbody2D rb;
    public Renderer rend;
    public GameObject explosion;

    bool isDead;

    /// <summary>
    /// This script is about 
    /// </summary>
    /// 
    private void Awake()
    {
    }

    // Enemy destroyed by taking damage
    public void TakeDamage(float damage)
    {

        hp -= damage;
        //ads.PlayOneShot(adcHitSound, 0.6f);
        // if the current health is less than or equal to zero...
        if (hp <= 0)
        {
            // the enemy is dead
            isDead = true;
            // there is a little sound belong to if enemy is dead
            EffectSound(isDead);
            // ... the enemy is dead       
            Dead();
        }
        else
        {
            // there is a little sound belong to if enemy is dead
            EffectSound(isDead);
        }
    }

    // Enemy destroyed method

    private void  Dead()
    {
        // create explosion effect at the enemy's position
        Instantiate(explosion, transform.position, transform.rotation);
        Point.Create(GetPosition(), GetPoint());
        GameAssets.scores += scores;

        // delete the enemy when it's dead
        Destroy(gameObject);
    }

    void EffectSound(bool isDead)
    {
        // play a clip at enemy's position
        if (isDead)
        {
            AudioSource.PlayClipAtPoint(adcExplosionSound, transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(adcHitSound, transform.position);
        }
    }
    public float GetPoint()
    {
        return scores;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
