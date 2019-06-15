using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int health;
    private int startHealth;
    public int team;

    void OnEnable()
    {
        switch (team)
        {
            case 1: startHealth = GameManager.getTeamHealths()[0]; break;
            case 2: startHealth = GameManager.getTeamHealths()[1]; break;
        }
    }

    public void DecreaseHealthbar()
    {
        switch (team)
        {
            case 1: health = GameManager.getTeamHealths()[0]; break;
            case 2: health = GameManager.getTeamHealths()[1]; break;
        }
        Resize();
    }

    void Resize()
    {
        float percentage = health / startHealth;
        Vector2 scale = new Vector2(transform.localScale.x, transform.localScale.y - percentage);
        transform.localScale = scale;
    }

}
