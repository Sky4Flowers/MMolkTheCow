using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputManager : MonoBehaviour
{

    public enum ButtonType { X, Y, A, B, Start, Back, LeftShoulder, RightShoulder };
    public static InputManager Instance;
    bool[] playerIndexSet = { false, false, false, false };
    PlayerIndex playerIndex;
    GamePadState[] state = new GamePadState[4];
    GamePadState[] prevState = new GamePadState[4];
    public int playerNum;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Initialization
        // Find all controllers

        for (int i = 0; i < playerNum; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                playerIndexSet[i] = true;
            }
        }

    }

    void Update()
    {
        //Updating States
        for (int i = 0; i < playerNum; i++)
        {
            prevState[i] = state[i];
            state[i] = GamePad.GetState((PlayerIndex)i);
        }
    }

    public Vector2 getLeftStick(int player)//Getting values from left stick
    {
        return new Vector2(state[player].ThumbSticks.Left.X, state[player].ThumbSticks.Left.Y);
    }

    public Vector2 getRightStick(int player)//Getting values from right stick
    {
        return new Vector2(state[player].ThumbSticks.Right.X, state[player].ThumbSticks.Right.Y);
    }

    public bool getButtonDown(int player, ButtonType button)
    {
        switch (button)
        {
            case ButtonType.A:
                return (prevState[player].Buttons.A == ButtonState.Released && state[player].Buttons.A == ButtonState.Pressed);
            case ButtonType.B:
                return (prevState[player].Buttons.B == ButtonState.Released && state[player].Buttons.B == ButtonState.Pressed);
            case ButtonType.X:
                return (prevState[player].Buttons.X == ButtonState.Released && state[player].Buttons.X == ButtonState.Pressed);
            case ButtonType.Y:
                return (prevState[player].Buttons.Y == ButtonState.Released && state[player].Buttons.Y == ButtonState.Pressed);
            case ButtonType.Start:
                return (prevState[player].Buttons.Start == ButtonState.Released && state[player].Buttons.Start == ButtonState.Pressed);
            case ButtonType.Back:
                return (prevState[player].Buttons.Back == ButtonState.Released && state[player].Buttons.Back == ButtonState.Pressed);
            case ButtonType.RightShoulder:
                return (prevState[player].Buttons.RightShoulder == ButtonState.Released && state[player].Buttons.RightShoulder == ButtonState.Pressed);
            case ButtonType.LeftShoulder:
                return (prevState[player].Buttons.LeftShoulder == ButtonState.Released && state[player].Buttons.LeftShoulder == ButtonState.Pressed);

            default:
                Debug.LogError("Button " + button + " not mapped");
                return false;
        }
    }

    public bool getButton(int player, ButtonType button)
    {
        switch (button)
        {
            case ButtonType.A:
                return state[player].Buttons.A == ButtonState.Pressed;
            case ButtonType.B:
                return state[player].Buttons.B == ButtonState.Pressed;
            case ButtonType.X:
                return state[player].Buttons.X == ButtonState.Pressed;
            case ButtonType.Y:
                return state[player].Buttons.Y == ButtonState.Pressed;
            case ButtonType.Start:
                return state[player].Buttons.Start == ButtonState.Pressed;
            case ButtonType.Back:
                return state[player].Buttons.Back == ButtonState.Pressed;
            case ButtonType.RightShoulder:
                return state[player].Buttons.RightShoulder == ButtonState.Pressed;
            case ButtonType.LeftShoulder:
                return state[player].Buttons.LeftShoulder == ButtonState.Pressed;

            default:
                Debug.LogError("Button " + button + " not mapped");
                return false;
        }
    }

}
