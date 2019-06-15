using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Projectile"))
        {
            Debug.Log("wall2" + other.gameObject.CompareTag("ProjectileStandard") + " " + other.gameObject.CompareTag("ProjectileSpecial"));

            if (other.gameObject.CompareTag("ProjectileStandard"))
            {
                Debug.Log("Destroying");
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("ProjectileSpecial"))
            {
                if (other.gameObject.GetComponent<Projectile>().bounce < 3)
                {
                    Debug.Log("Bounce");
                    other.gameObject.GetComponent<Projectile>().bounce++;
                    other.GetComponent<Projectile>().onCollisionWith(collider);
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}