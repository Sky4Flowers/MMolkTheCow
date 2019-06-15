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
            Debug.Log("wall");

            if (other.gameObject.tag == "ProjectileStandard")
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "ProjectileSpecial")
            {
                if(other.gameObject.GetComponent<Projectile>().bounce < 3)
                {
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
