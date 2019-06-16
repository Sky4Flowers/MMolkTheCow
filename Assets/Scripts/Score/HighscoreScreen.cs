using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScreen : MonoBehaviour
{ 
    public int winningTeam;
    public List<PlayerScore> playerScores = new List<PlayerScore>();
    public List<TeamScore> teamScores = new List<TeamScore>();
    public Text winningText;

    void Start()
    {
        winningTeam = GameManager.getWinningTeam();
        winningText = GameObject.Find("WinningTeam").GetComponent<Text>();
        winningText.text = "TEAM #" + winningTeam + " WINS!";
        winningText.color = Color.yellow;
        teamScores.Add(GameManager.getTeamScoreOf(1));
        teamScores.Add(GameManager.getTeamScoreOf(2));
        playerScores.Add(GameManager.getPlayerScore(1));
        playerScores.Add(GameManager.getPlayerScore(2));
        playerScores.Add(GameManager.getPlayerScore(3));
        playerScores.Add(GameManager.getPlayerScore(4));

        StartCoroutine("Restart");
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(10);
        GameManager.goBackToMain();
    }
}