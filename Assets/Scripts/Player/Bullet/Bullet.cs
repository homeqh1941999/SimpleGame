using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private AudioClip adcShootSound;

    [SerializeField] private float volume = 0.3f;
    [SerializeField] private float extraDame;

    private AudioSource ads;
    private float damage;
  
    private Vector2 diagonalRight = new Vector2(1, 1);
    private Vector2 diagonalLeft = new Vector2(-1, 1);

    [HideInInspector]
    public float bulletSpeed;

    private void Awake()
    {
        ads = GetComponent<AudioSource>();
    }
    void Start()
    {
        // Make bullets fly toward
        Shoot();
    }
    void Update()
    {
        // if the bullets fly out of the screen, delete them.
        if (!sr.isVisible)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Bullets disappear when touch the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                // the enemy take damage and loose its health points.
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }      
        } 
    }
    private void Shoot()
    {   
        Weapon weapon = GameObject.Find("Weapon").GetComponent<Weapon>();

        switch (Weapon.checkWeapon)
        {
            // if player has normal weapon, one bullet for each button down
            case 0:
                damage = Weapon.damage;
                rb.velocity = transform.up * bulletSpeed;
                break;
            // if player has two bullets weapon, two bullets for each button down
            case 1:
                damage = Weapon.damage;
                rb.velocity = transform.up * bulletSpeed;
                break;
            // if player has three bullets weapon, three bullets for each button down and two of them will fly with diagonal direction.
            case 2:
                if (transform.position.x < weapon.transform.position.x - 0.9f)
                    rb.velocity = diagonalLeft * bulletSpeed;
                else if (transform.position.x > weapon.transform.position.x + 0.9f)
                    rb.velocity = diagonalRight * bulletSpeed;
                else
                    rb.velocity = transform.up * bulletSpeed;
                break;
            // if player has laser weapon, bullets will become a ray 
            case 3:
                damage = Weapon.damage;               
                break;
            // if player has one bullet enhanced weapon, one bullet for each button down but it will has more damage
            case 4:
                damage = Weapon.damage;
                transform.localScale = new Vector3(.2f, .2f, .2f);
                rb.velocity = transform.up * bulletSpeed;
                break;
        }
        ads.PlayOneShot(adcShootSound, volume);
    }

    /*private void ShootBullets(Vector3 shootPoint, float bulletSpeed, float offset, float damage)
    {
        this.damage = damage;
        rb.velocity = shootPoint * bulletSpeed;
    }*/
}
