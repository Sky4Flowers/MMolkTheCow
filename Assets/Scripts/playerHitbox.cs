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
            Debug.Log("hit");
                        if (other.gameObject.layer == 8 && gameObject.layer == 9)
                                {
                                    gameObject.GetComponentInParent<player>().reduceLife(2);
                                    Destroy(other.gameObject);
                                }
                        if (other.gameObject.layer == 9 && gameObject.layer == 8)
                                {
                                    gameObject.GetComponentInParent<player>().reduceLife(1);
                                    Destroy(other.gameObject);
                                }
                        if(other.gameObject.tag == "slow")
                         {
                gameObject.GetComponentInParent<player>().slowEnemies();
                         }
        }
    }
}