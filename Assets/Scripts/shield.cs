﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    private new Collider collider;
    public int team;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Something
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Projectile"))
        {
            if (other.gameObject.layer == gameObject.layer)
            {
                other.GetComponent<Projectile>().onCollisionWith(collider);
                other.gameObject.GetComponent<Projectile>().charged = true;
            }
            else if (other.gameObject.layer == 8 && gameObject.layer == 9)
            {
                other.GetComponent<Projectile>().onCollisionWith(collider);
                other.gameObject.layer = 9;
                other.gameObject.GetComponent<Projectile>().charged = true;
            }
            else if (other.gameObject.layer == 9 && gameObject.layer == 8)
            {
                other.GetComponent<Projectile>().onCollisionWith(collider);
                other.gameObject.layer = 8;
                other.gameObject.GetComponent<Projectile>().charged = true;
            }
        }
    }
}