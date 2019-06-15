using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int startTimeInSeconds;
    private bool isActive;
    public bool run;
    private int currentTime;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        currentTime = startTimeInSeconds;
        text = gameObject.GetComponent<Text>();
        text.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (run)
        {
            StartCoroutine("TimerTick");
        }
    }

    IEnumerator TimerTick()
    {
        run = false;
        text.text = ""+ currentTime;
        yield return new WaitForSeconds(1);
        currentTime--;
        run = true;
        if(currentTime < 0)
        {
            run = false;
            isActive = false;
            gameObject.SetActive(isActive);
        }
    }
}
