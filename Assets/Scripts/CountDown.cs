using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int startTimeInSeconds;
    public bool isActive;
    private int currentTime;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        currentTime = startTimeInSeconds;
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine("TimerTick");
    }

    IEnumerator TimerTick()
    {
        text.text = ""+currentTime;
        yield return new WaitForSeconds(1);
        currentTime--;
        if(currentTime < 0)
        {
            isActive = false;
            gameObject.SetActive(isActive);
        }
    }
}
