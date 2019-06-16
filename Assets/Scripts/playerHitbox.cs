using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitbox : MonoBehaviour
{
    public int team;
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
        if (other.gameObject.tag != "shield" && other.gameObject.tag != "Player")
        {
            if (other.gameObject.layer == 10 && gameObject.layer == 9)
            {
                if (other.gameObject.GetComponent<Projectile>().charged == true)
                {
                    gameObject.GetComponentInParent<player>().reduceLife(2);
                }

                Destroy(other.gameObject);
            }
            if (other.gameObject.layer == 11 && gameObject.layer == 8)
            {
                if(other.gameObject.GetComponent<Projectile>().charged == true)
                {
                        gameObject.GetComponentInParent<player>().reduceLife(1);
                }
                
                Destroy(other.gameObject);
            }
            if(other.gameObject.layer == 10 && gameObject.layer == 8)
            {
                Destroy(other.gameObject);
            }
            if(other.gameObject.layer == 11 && gameObject.layer == 9)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.tag == "slow")
        {
            gameObject.GetComponentInParent<player>().slowEnemies();
            other.gameObject.transform.position = new Vector3(-3, -15, 0);
        }
    }
}