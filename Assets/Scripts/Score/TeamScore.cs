using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScore
{
    private int totalScore = 12345;
    private int numOfSuccessfullPasses = 12345;
    private int hits = 12345;
    private int teamId = 42;
    private Text teamScore;
    private Text teamValues;

    public TeamScore(int teamId, int totalScore, int hits, int numOfSuccessfullPasses)
    {
        this.teamId = teamId;
        this.totalScore = totalScore;
        this.hits = hits;
        this.numOfSuccessfullPasses = numOfSuccessfullPasses;

        string teamName = "";
        string teamValue = "";
        Color color = Color.black;

        if (teamId == 1)
        {
            teamName = "Team1Name";
            teamValue = "Team1Score";
            color = new Color(0xfe/ 255.0f, 0xb7/ 255.0f, 0x1f/ 255.0f);
        }
        else
        {
            teamName = "Team2Name";
            teamValue = "Team2Score";
            color = new Color(0xfd/255.0f, 0x00 / 255.0f, 0x6a / 255.0f);
        }

        teamScore = GameObject.Find(teamName).GetComponent<Text>();
        teamValues = GameObject.Find(teamValue).GetComponent<Text>();

        teamScore.color = color;
        teamValues.color = color;

        SetScore();
    }

    void SetScore()
    {
        teamScore.text = "TEAM #" + teamId + "\nHits:\nPasses";
        teamValues.text = totalScore + "\n" + hits + "\n" + numOfSuccessfullPasses;
    }
}