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
    private Color color = Color.black;

    public PlayerScore(int playerId, int teamId, float health, float longestShot)
    {
        this.teamId = teamId;
        this.playerId = playerId;
        this.longestShot = longestShot;
        this.health = health;
        string playerScoreId = "Player" + playerId + "Score";
        string playerScoreValueId = "Player" + playerId + "ScoreValue";

        playerScore = GameObject.Find(playerScoreId).GetComponent<Text>();
        playerScore.color = color;
        playerScoreValue = GameObject.Find(playerScoreValueId).GetComponent<Text>();
        playerScoreValue.color = color;

        if (playerId == 1 || playerId == 3)
        {
            color = new Color(0x59 / 255.0f, 0xff / 255.0f, 0x67 / 255.0f);
        }
        else
        {
            color = new Color(0x04 / 255.0f, 0xf8 / 255.0f, 0xff / 255.0f);
        }
        playerScore.color = color;
        playerScoreValue.color = color;
        Debug.Log(color.ToString());
        SetScore();
    }
    void SetScore()
    {
        playerScore.text = "PLAYER #" + playerId + "\nHealth:" + "\nFarthest Shot:";
        playerScoreValue.text = "\n" + health + "\n" + longestShot;
    }
}
