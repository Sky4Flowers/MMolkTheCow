using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
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
