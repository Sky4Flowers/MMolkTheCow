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

        if (playerId == 1 || playerId == 2)
        {
            color = new Color(0xfd / 255.0f, 0x00 / 255.0f, 0x6a / 255.0f);
        }
        else
        {
            color = new Color(0xfe / 255.0f, 0xb7 / 255.0f, 0x1f / 255.0f);
        }
        playerScore.color = color;
        playerScoreValue.color = color;
        Debug.Log(color.ToString());
        SetScore();
    }
    void SetScore()
    {
        string name = "";
        switch (playerId)
        {
            case 1: name = "DAIN"; break;
            case 2: name = "COUP"; break;
            case 3: name = "BRAX"; break;
            case 4: name = "ARIS"; break;
        }
        playerScore.text = "PLAYER #" + playerId + ":" + "\nHealth:" + "\nFarthest Shot:";
        playerScoreValue.text = name + "\n" + health + "\n" + longestShot;
    }
}
