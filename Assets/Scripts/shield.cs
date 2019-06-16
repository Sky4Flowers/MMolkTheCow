using System.Collections;
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
        Debug.Log("prepre");
        if (other.gameObject.tag.Contains("Projectile"))
        {
            Debug.Log("pre");
            if ((other.gameObject.layer == 10 && gameObject.layer == 12) || (other.gameObject.layer == 11 && gameObject.layer == 13))
            {
                if(other.gameObject.layer == 10)
                {
                    GameManager.getInstance().onPlayerPass(0);
                }
                else
                {
                    GameManager.getInstance().onPlayerPass(1);
                }
                other.GetComponent<Projectile>().onCollisionWith(collider);
                other.gameObject.GetComponent<Projectile>().charged = true;
                Debug.Log("10, 12");
            }
            else if (other.gameObject.layer == 10 && gameObject.layer == 13)
            {
                GameManager.getInstance().onPlayerPass(1);
                other.GetComponent<Projectile>().onCollisionWith(collider);
                other.gameObject.layer = 11;
                other.gameObject.GetComponent<Projectile>().charged = true;
                Debug.Log("10, 13");
            }
            else if (other.gameObject.layer == 11 && gameObject.layer == 12)
            {
                GameManager.getInstance().onPlayerPass(0);
                other.GetComponent<Projectile>().onCollisionWith(collider);
                other.gameObject.layer = 10;
                other.gameObject.GetComponent<Projectile>().charged = true;
                Debug.Log("11, 12");
            }
        }
    }
}