using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public class WeaponList
    {
        public string weaponName;
        public int weaponID;

        public WeaponList(string weaponName, int weaponID)
        {
            this.weaponName = weaponName;
            this.weaponID = weaponID;
        }
    }

    public Transform fireEndPoint;
    public Transform fireStartPoint;
    public GameObject bulletPrefabs;
    public GameObject _laser;
    public GameObject startLaserVFX;
    public GameObject endLaserVFX;

    [SerializeField] private AudioClip adc;
    [SerializeField] private float volume = 0.3f;
    [SerializeField] private float distanceRay = 100;

    private LineRenderer lrend;
    private AudioSource ads;
    private Button btn;

    public static float damage;
    public static int checkWeapon;

    private Vector3 fireStartPointModify;
    private Vector3 fireEndPointModify;

    private List<WeaponList> weaponList;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    float delayTimerForSpammingShootingButton = .2f;
    bool isLaserOn = false;
    
    Transform m_transform;
    private void Awake()
    {
        ads = GetComponent<AudioSource>();      
        btn = GameObject.FindGameObjectWithTag("Shoot").GetComponent<Button>();
        m_transform = GetComponent<Transform>();
        btn.onClick.AddListener(CheckWeapon);
        lrend = _laser.GetComponent<LineRenderer>();
        lrend.enabled = !lrend.enabled;          
    }
    private void Start()
    {
        SetUpWeapon();
        VFXLaserList();
    }
    // Normal Bullets Weapons = 0;
    // Three Bullets Weapons = 1;
    // ...
    private void SetUpWeapon()
    {
        weaponList = new List<WeaponList>();
        weaponList.Add(new WeaponList("TwoBulletsWeapon", 1));
        weaponList.Add(new WeaponList("ThreeBulletsWeapon", 2));
        weaponList.Add(new WeaponList("LazerWeapon", 3));
        weaponList.Add(new WeaponList("OneBulletsEnhancedWeapon", 4));
        checkWeapon = 3;
    }
    public void BulletsCreate(float bullets)
    {     
        // Get the player's position
        fireStartPointModify = new Vector3(fireStartPoint.position.x, fireStartPoint.position.y, fireStartPoint.position.z);

        // the amount of bullets for each weapons
        fireStartPointModify.x += bullets;

        // create bullets at player's position
        Instantiate(bulletPrefabs, fireStartPointModify, fireStartPoint.rotation);      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            // Check what kind of weapons we're using
            foreach(WeaponList item in weaponList)
            {
                if (collision.gameObject.CompareTag(item.weaponName))
                {
                    checkWeapon = item.weaponID;
                    Destroy(collision.gameObject);
                    break;
                }
            }
            ads.PlayOneShot(adc, volume); 
        }       
    }

    // check weapon before shooting
    void CheckWeapon()
    {
        switch (checkWeapon)
        {
            case 0:
                NormalWeapon(0);
                break;
            case 1:
                TwoBulletsWeapon();
                break;
            case 2:
                ThreeBulletsWeapon();
                break;
            case 3:
                LaserWeapon();  
                break;
            case 4:
                NormalWeapon(40);
                break;
            default:
                break;
        }      
    }

    // the detail of normal weapon or one bullets enhanced with extra damage
    private void NormalWeapon(float extraDamage)
    {
        BulletsCreate(0);
        damage = 40 + extraDamage;
    }

    // the detail of two bullets weapon
    private void TwoBulletsWeapon()
    {
        for (int i = 0; i < 3; i = i + 2)
        {
            BulletsCreate(i - 1);
            damage = 100;
        }
    }

    // the detail of three bullets weapon
    private void ThreeBulletsWeapon()
    {
        for (int i = 0; i < 3; i++)
        {
            BulletsCreate(i - 1);
            damage = 50;
        }
    }

    // the detail of laser weapon
    private void LaserWeapon()
    {
        StartCoroutine(ShootLaser(0.2f));
        damage = 1;
    }
    IEnumerator ShootLaser(float delayTime)
    {
        Player.isShooting = !Player.isShooting;
        lrend.enabled = !lrend.enabled;
        fireStartPointModify = new Vector3(fireStartPoint.position.x, fireStartPoint.position.y, fireStartPoint.position.z);
        fireEndPointModify = new Vector3(fireEndPoint.position.x, fireEndPoint.position.y, fireEndPoint.position.z);
        RaycastHit2D hit = Physics2D.Raycast(fireStartPointModify, Vector3.up, distanceRay, LayerMask.GetMask("Enemy"));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // the enemy take damage and loose its health points.
                    enemy.TakeDamage(damage);
                }
            }
            DrawLaser(fireStartPointModify, hit.point);
        }
        else
        {
            DrawLaser(fireStartPointModify, fireEndPointModify);
        }
        btn.enabled = false;
        yield return new WaitForSeconds(delayTime);
        lrend.enabled = !lrend.enabled;
        isLaserOn = false;
        LaserState(isLaserOn);
        yield return new WaitForSeconds(delayTime + delayTimerForSpammingShootingButton);
        Player.isShooting = !Player.isShooting;
        btn.enabled = true;
    }

    private void VFXLaserList()
    {
        for (int i=0; i < startLaserVFX.transform.childCount; i++)
        {
            var ps = startLaserVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
        for (int i = 0; i < endLaserVFX.transform.childCount; i++)
        {
            var ps = endLaserVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
        LaserState(isLaserOn);
    }
    private void Draw2DRay(Vector3 startPos, Vector3 endPos)
    {       
        lrend.SetPosition(0, startPos);
        lrend.SetPosition(1, endPos);
    }
    private void DrawLaser(Vector3 fireStartPoint, Vector3 fireEndPoint)
    {
        isLaserOn = true;
        LaserState(isLaserOn);
        startLaserVFX.transform.position = fireStartPoint;
        endLaserVFX.transform.position = fireEndPoint;
        Draw2DRay(fireStartPoint, fireEndPoint);        
    }
    private void LaserState(bool isLaserOn)
    {
        if (isLaserOn)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Stop();
            }
        }
    }
}
