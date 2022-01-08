using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeManage : MonoBehaviour
{
    public GameObject[] WeaponUpgradeList;
    private Vector3 dir;


    void Start()
    {
        dir = Vector3.down;
        InvokeRepeating(nameof(WeaponUpgrade), 5f, 10f);
    }

    // Update is called once per frame
    [System.Obsolete]

    void WeaponUpgrade()
    {
        Instantiate(WeaponUpgradeList[Random.Range(0, 4)]);
    }
}
