using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScore
{
    private int totalScore;
    private int numOfSuccessfullPasses;
    private int hits;
    private Text teamScore;
    private string text;

    public TeamScore(int totalScore, int hits, int numOfSuccessfullPasses)
    {
        this.totalScore = totalScore;
        this.hits = hits;
        this.numOfSuccessfullPasses = numOfSuccessfullPasses;
    }

    void SetScore()
    {
        
    }
}
