using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWall()
    {
        int time = Random.Range(20, 40);
        yield return new WaitForSeconds(time);
        int pos = Random.Range(0, 3);
        switch (pos)
        {
            case 0:
                gameObject.transform.position = new Vector3(-3, 1, 0);
                break;
            case 1:
                gameObject.transform.position = new Vector3(3, 1, 0);
                break;
            case 2:
                gameObject.transform.position = new Vector3(0, 4, 0);
                break;
            case 3:
                gameObject.transform.position = new Vector3(0, -2, 0);
                break;
        }
        StartCoroutine(SpawnWall());
    }
}
