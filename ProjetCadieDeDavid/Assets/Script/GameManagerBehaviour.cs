using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    public int playerSkin = 0;
    public int coin;
    public int coinPerLevel;
    public int level;
    public int ticket;
    public enum GameStates
    {
        MainMenu,
        LevelSelection,
        InGame,
        Pause,
        GameOver,
        Recipe,
    }
    private static GameStates _GameState;
    public static GameStates GameState
    {
        get
        {
            return _GameState;
        }
    }
    private static GameManagerBehaviour _instance;
    public static GameManagerBehaviour instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        AddCoin(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown("[5]"))
        {
            SaveSysteme.Save(this);
        }
        if (Input.GetKeyDown("[7]"))
        {
            SaveData data = SaveSysteme.LoadData();
            level = data.level;
            coin = data.coin;
            ticket = data.ticket;
            LevelUnlocker.instance.UnlockLevel();
        }
        if(Input.GetKeyDown("[8]"))
        {
            level++;
        }
        if (Input.GetKeyDown("[9]"))
        {
            Debug.Log("Level : " + level);
        }
    }
    public void AddCoin(int numberOfCoin)
    {
        coin += numberOfCoin;
        coinPerLevel -= numberOfCoin;
        UIManagerBehaviour.instance.DisplayCoin();
    }

    public void ChangeGameState(GameStates currentState)
    {
        _GameState = currentState;
        switch (_GameState)
        {
            case GameStates.MainMenu:
                UIManagerBehaviour.instance.SetMainMenuActive();
                Time.timeScale = 0;
                break;
            case GameStates.LevelSelection:
                UIManagerBehaviour.instance.SetLevelSelectionActive();
                Time.timeScale = 0;
                break;
            case GameStates.InGame:
                UIManagerBehaviour.instance.SetHUDActive();
                Time.timeScale = 1;
                break;
            case GameStates.Pause:
                UIManagerBehaviour.instance.SetPauseActive();
                Time.timeScale = 0;
                break;
            case GameStates.GameOver:
                UIManagerBehaviour.instance.SetGameOverActive();
                break;
            case GameStates.Recipe:
                UIManagerBehaviour.instance.SetRecipeActive();
                ScenesManagerBehaviour.instance.LoadRecipeScene();
                break;
        }
    }

    //Button UI
    public void ChangeGameStateByUI(int currentState)
    {
        switch (currentState)
        {
            case 0:
                ChangeGameState(GameStates.MainMenu);
                ScenesManagerBehaviour.instance.LoadMainMenu();
                break;
            case 1:
                ChangeGameState(GameStates.LevelSelection);
                break;
            case 2:
                ChangeGameState(GameStates.InGame);
                break;
            case 3:
                ChangeGameState(GameStates.Pause);
                break;
            case 4:
                ChangeGameState(GameStates.GameOver);
                break;
            case 5:
                ChangeGameState(GameStates.Recipe);
                ScenesManagerBehaviour.instance.LoadRecipeScene();
                break;
        }
    }
    public void ChangePlayerSkin(int skin)
    {
        playerSkin = skin;
    }
}
