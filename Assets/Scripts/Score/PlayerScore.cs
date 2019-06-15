using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore
{
    private float longestShot;
    private float health;
    private int teamId;
    private int playerId;
    private Text playerScore;
    private Text playerScoreValue;
    private string playerTextScore;
    private PlayerScore(int playerId, int teamId, float health, float longestShot)
    {
        this.teamId = teamId;
        this.playerId = playerId;
        this.longestShot = longestShot;
        this.health = health;
        SetScore();
    }
    void SetScore()
    {
        playerScore = GameObject.Find("Player1Score").GetComponent<Text>();
        playerTextScore = "PLAYER #" + playerId + "\nHealth:" + "\nFarthest Shot:";

        playerScore.text = playerTextScore;
    }
}
