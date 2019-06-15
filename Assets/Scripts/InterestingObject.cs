using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestingObject : MonoBehaviour, ICamInterest
{
    private bool isShown = true;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public bool isOutOfView()
    {
        return !isShown;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    public void OnBecameInvisible()
    {
        isShown = false;
    }

    public void OnBecameVisible()
    {
        isShown = true;
    }
}