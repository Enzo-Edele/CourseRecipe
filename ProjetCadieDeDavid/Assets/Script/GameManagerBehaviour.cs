using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    public enum gameStates
    {
        MainMenu,
        InGame,
        Pause,
        GameOver,
        Recipe,
    }
    private static gameStates _GameStates;
    public static gameStates GameStates;
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

    public void ChangeLevelStates(gameStates currentState)
    {
        _GameStates = currentState;
        switch (_GameStates)
        {
            case gameStates.MainMenu:
                break;
            case gameStates.InGame:
                break;
            case gameStates.Pause:
                break;
            case gameStates.GameOver:
                break;
            case gameStates.Recipe:
                break;
        }
    }
}
