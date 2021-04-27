using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerBehaviour : MonoBehaviour
{
    public float backgroundSpeed;
    public enum levelStates
    {
        Collect,
        Run,
    }
    private static levelStates _LevelStates;
    public static levelStates LevelStates;
    private static LevelManagerBehaviour _instance;
    public LevelManagerBehaviour instance
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

    public void ChangeLevelStates(levelStates currentState)
    {
        _LevelStates = currentState;
        switch(_LevelStates)
        {
            case levelStates.Collect:
                break;
            case levelStates.Run:
                break;
        }
    }
}
