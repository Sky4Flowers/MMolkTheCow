using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int health;
    private int startHealth;
    [SerializeField]
    public int team;
    public float maxSize;

    void OnEnable()
    {
        startHealth = GameManager.getTeamHealths()[team];
    }

    public void updateHealthbar()
    {
        health = GameManager.getTeamHealths()[team];
        Debug.Log(health);
        Resize();
    }

    void Resize()
    {
        if(startHealth == 0)
        {
            startHealth = GameManager.getTeamHealths()[team];
            health = startHealth;
            maxSize = transform.localScale.y;
        }
        Debug.Log(maxSize + " " + transform.localScale.y);
        float percentage = (float) health / startHealth;
        Vector2 scale = new Vector2(transform.localScale.x, maxSize * percentage);
        transform.localScale = scale;
    }
}