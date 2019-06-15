using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SlowSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SlowSpawn()
    {
        int time = Random.Range(3, 5);
        yield return new WaitForSeconds(time);
        int pos = Random.Range(0, 3);
        switch (pos)
        {
            case 0:  gameObject.transform.position = new Vector3(-4, 5, 0);
                break;
            case 1:  gameObject.transform.position = new Vector3(4, 5, 0);
                break;
            case 2:  gameObject.transform.position = new Vector3(-4, -2.5f, 0);
                break;
            case 3:  gameObject.transform.position = new Vector3(4, -2.5f, 0);
                break;
        }
        StartCoroutine(SlowSpawn());
    }
}
