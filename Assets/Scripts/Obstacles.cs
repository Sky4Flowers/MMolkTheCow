using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public int timeInS;
    GameObject group1;
    bool toggle = true;
    bool pause = true;
    // Start is called before the first frame update
    void Start()
    {
        group1 = GameObject.Find("Group1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggle)
        {
            StartCoroutine("ToggleGroup");
            StartCoroutine("Pause");
        }
    }

    IEnumerator ToggleGroup()
    {
        toggle = false;
        StartCoroutine("Pause");
        yield return new WaitForSeconds(timeInS);
        group1.SetActive(false);
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(timeInS);
        group1.SetActive(true);
        toggle = true;
    }

}
