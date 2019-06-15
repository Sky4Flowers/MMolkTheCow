using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnBehaviour
{
    private static int spawnRange = 10;
    //private static Vector3 spawnMiddle; der Einfachheit halber 0, 0, 0

    public static void spawnPlayers(int amount)
    {
        while (amount > 0)
        {
            GameObject.Instantiate(GameManager.getPlayerById(--amount));
            //TODO set position etc
        }
    }

    public static void spawnObstacles()
    {
        int amount = Random.Range(1, 10);
        while (amount > 0)
        {
            //TODO 
        }
    }

    public static void spawnWave()
    {
        //TODO
    }
}