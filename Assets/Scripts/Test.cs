using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool update = false;
    bool lateupdate = false;
    bool onGUI = false;

    private void Update()
    {
        if (!update)
        {
            TestUpdate();
        }
    }

    private void OnGUI()
    {
        if (!onGUI)
        {
            TestOnGUI();
        }
    }

    private void LateUpdate()
    {
        if (!lateupdate)
        {
            TestLateUpdate();
        }
    }

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void Start()
    {
        Debug.Log("Start");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
    }

    void TestUpdate()
    {
        update = true;
        Debug.Log("Update");
    }
    void TestOnGUI()
    {
        onGUI = true;
        Debug.Log("OnGUI");
    }
    void TestLateUpdate()
    {
        lateupdate = true;
        Debug.Log("LateUpdate");
    }
}
