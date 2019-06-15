using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
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
        if (other.gameObject.tag.Contains("Projectile"))
        {
            if (other.gameObject.layer == gameObject.layer)
            {
                //reflektieren fehlt, hier nur testweise:
                other.gameObject.GetComponent<Projectile>().direction *= -1;
                other.gameObject.GetComponent<Projectile>().charged = true;
            }
            else if (other.gameObject.layer == 8 && gameObject.layer == 9)
            {
                Debug.Log("reflect1");
                //reflektieren fehlt, hier nur testweise:
                other.gameObject.GetComponent<Projectile>().direction *= -1;
                other.gameObject.layer = 9;
                other.gameObject.GetComponent<Projectile>().charged = true;
            }
            else if (other.gameObject.layer == 9 && gameObject.layer == 8)
            {
                Debug.Log("reflect2");
                //reflektieren fehlt, hier nur testweise:
                other.gameObject.GetComponent<Projectile>().direction *= -1;
                other.gameObject.layer = 8;
                other.gameObject.GetComponent<Projectile>().charged = true;
            }
        }
    }
}