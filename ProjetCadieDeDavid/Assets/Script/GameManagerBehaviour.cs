using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    public enum GameStates
    {
        MainMenu,
        InGame,
        Pause,
        GameOver,
        Recipe,
    }
    private static GameStates _GameState;
    public static GameStates GameState;
    private static GameManagerBehaviour _instance;
    public GameManagerBehaviour instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void ChangeLevelStates(GameStates currentState)
    {
        _GameState = currentState;
        switch (_GameState)
        {
            case GameStates.MainMenu:
                break;
            case GameStates.InGame:
                break;
            case GameStates.Pause:
                break;
            case GameStates.GameOver:
                break;
            case GameStates.Recipe:
                break;
        }
    }
}
