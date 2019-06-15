using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool[] playerConnected = new bool[4];
    bool startGame = false;
    Text team1;
    Text team2;
    Text player1;
    Text player2;
    Text player3;
    Text player4;
    Color defaultColor1;
    Color defaultColor2;
    InputManager manager;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        startGame = false;
        player1 = GameObject.Find("Player1").GetComponent<Text>();
        player2 = GameObject.Find("Player2").GetComponent<Text>();
        player3 = GameObject.Find("Player3").GetComponent<Text>();
        player4 = GameObject.Find("Player4").GetComponent<Text>();

        team1 = GameObject.Find("Team1Title").GetComponent<Text>();
        team2 = GameObject.Find("Team2Title").GetComponent<Text>();
        team1.color = new Color(0xfd / 255.0f, 0x00 / 255.0f, 0x6a / 255.0f);
        team2.color = new Color(0xfe / 255.0f, 0xb7 / 255.0f, 0x1f / 255.0f);
        manager = InputManager.Instance;
        defaultColor1 = new Color(0x67 / 255.0f, 0x11 / 255.0f, 0x2a / 255.0f);
        defaultColor2 = new Color(0x75 / 255.0f, 0x32 / 255.0f, 0x00 / 255.0f);
        player1.color = player2.color = defaultColor1;
        player3.color = player4.color = defaultColor2;
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        for(int i = 0; i < manager.playerNum; i++)
        {
            if (!playerConnected[i]) break;
            else StartCoroutine("StartGame");
        }
    }

    void OnInput()
    {
        for (int i = 0; i < manager.playerNum; i++)
        {
            if (manager.getButtonDown(i, InputManager.ButtonType.A))
            {
                if (!playerConnected[i])
                {
                    playerConnected[i] = true;
                    SwitchColor(i, playerConnected[i]);
                }
                else
                {
                    playerConnected[i] = false;
                    SwitchColor(i, playerConnected[i]);
                }
            }
        }
    }

    void SwitchColor(int playerId, bool active)
    {
        if (playerId == 0)
        {
            if (active)
            {
                player1.color = new Color(0xfd / 255.0f, 0x00 / 255.0f, 0x6a / 255.0f); return;
            }
            else
            {
                player1.color = defaultColor1; return;
            }
        }
        else if (playerId == 2)
        {
            if (active)
            {
                player3.color = new Color(0xfe / 255.0f, 0xb7 / 255.0f, 0x1f / 255.0f); return;
            }
            else
            {
                player3.color = defaultColor2; return;
            }
        }
        else if (playerId == 1)
        {
            if (active)
            {
                player2.color = new Color(0xfd / 255.0f, 0x00 / 255.0f, 0x6a / 255.0f); return;
            }
            else
            {
                player2.color = defaultColor1; return;
            }
        }
        else if (playerId == 3)
        {
            if (active)
            {
                player4.color = new Color(0xfe / 255.0f, 0xb7 / 255.0f, 0x1f / 255.0f); return;
            }
            else
            {
                player4.color = defaultColor2; return;
            }
        }
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        GameManager.startGame();
    }
}
