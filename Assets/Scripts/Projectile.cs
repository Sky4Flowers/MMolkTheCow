using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool charged;
    public int bounce;

    public int team;

    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        charged = false;
        bounce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * 2);
    }

    public void SetDirection(Vector3 input)
    {
        direction = input;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
}