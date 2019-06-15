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
        winningText = GameObject.Find("WinningTeam").GetComponent<Text>();
        winningText.text = "TEAM #" + winningTeam + " WINS!";
        winningText.color = Color.yellow;
        teamScores.Add(new TeamScore(1, 12345, 67890, 7));
        teamScores.Add(new TeamScore(2, 1345, 6780, 0));
        playerScores.Add(new PlayerScore(1, 1, 100, 246));
        playerScores.Add(new PlayerScore(2, 1, 400, 2456));
        playerScores.Add(new PlayerScore(3, 2, 700, 256));
        playerScores.Add(new PlayerScore(4, 2, 50, 246));
    }

    private void FixedUpdate()
    {
        StartCoroutine("Restart");
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(10);
        GameManager.goBackToMain();
    }
}
