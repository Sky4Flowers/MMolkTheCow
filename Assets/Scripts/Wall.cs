using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("wall");
        if (other.gameObject.tag != "shield" && other.gameObject.tag != "Player")
        {
            Debug.Log("wall2");

            if (other.gameObject.tag == "ProjectileStandard")
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "ProjectileSpecial")
            {
                if(other.gameObject.GetComponent<Projectile>().bounce < 3)
                {
                    other.gameObject.GetComponent<Projectile>().bounce++;
                    //reflektieren fehlt, hier nur testweise:
                    other.gameObject.GetComponent<Projectile>().direction *= -1;
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
