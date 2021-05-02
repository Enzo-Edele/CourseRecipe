using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerBehaviour : MonoBehaviour
{
    public float backgroundSpeed;
    public int capaciteCaddie;

    public int minSpeed;
    public int maxSpeed;
    public int minTime;
    public int maxTime;

    public int playerLife;
    public enum LevelStates
    {
        Collect,
        Run,
    }
    private static LevelStates _LevelState;
    public static LevelStates LevelState;
    private static LevelManagerBehaviour _instance;
    public static LevelManagerBehaviour instance
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

    public void ChangeLevelStates(LevelStates currentState)
    {
        _LevelState = currentState;
        switch(_LevelState)
        {
            case LevelStates.Collect:
                break;
            case LevelStates.Run:
                break;
        }
    }
}
