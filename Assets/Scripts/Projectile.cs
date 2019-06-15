using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool charged;
    public int bounce;

    public int sourceId;

    private Vector3 direction;
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

    public void setDirection(Vector3 input)
    {
        direction = input;
    }

    public Vector3 getDirection()
    {
        return direction;
    }

    public void onCollisionWith(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1) && hit.collider.Equals(other))
        {
            direction = Vector3.Reflect(direction, hit.normal);
        }
    }
}