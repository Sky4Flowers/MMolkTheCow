using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int health;
    private int startHealth;
    public int team;
    // Start is called before the first frame update
    void Start()
    {
        switch (team)
        {
            case 1: startHealth = GameManager.getTeamHeaths()[0]; break;
            case 2: startHealth = GameManager.getTeamHeaths()[1]; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealthbar()
    {
        switch (team)
        {
            case 1: health = GameManager.getTeamHeaths()[0]; break;
            case 2: health = GameManager.getTeamHeaths()[1]; break;
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
