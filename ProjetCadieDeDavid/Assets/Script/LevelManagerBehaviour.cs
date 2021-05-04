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

    public string[] recette = new string[0];
    public enum LevelStates
    {
        LevelBriefing,
        Collect,
        Run,
    }
    private static LevelStates _LevelState;
    public static LevelStates LevelState
    {
        get
        {
            return _LevelState;
        }
    }
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
        ChangeLevelStates(LevelStates.LevelBriefing);
        //listetexte (recette[1] "saut de ligne" recette[2])
    }

    public void ChangeLevelStates(LevelStates currentState)
    {
        _LevelState = currentState;
        switch(_LevelState)
        {
            case LevelStates.LevelBriefing:
                UIManagerBehaviour.instance.SetLevelBriefingActive();
                Time.timeScale = 0;
                break;
            case LevelStates.Collect:
                UIManagerBehaviour.instance.SetLevelBriefingNotActive();
                Time.timeScale = 1;
                break;
            case LevelStates.Run:
                UIManagerBehaviour.instance.SetLevelBriefingNotActive();
                Time.timeScale = 1;
                break;
        }
    }
}
