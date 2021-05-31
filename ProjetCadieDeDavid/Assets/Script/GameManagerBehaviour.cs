using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    public int playerSkin;
    public int coin;
    public int coinPerLevel;
    public int maxLevel;
    public int level;
    public int levelSelect;
    public int ticket;
    public int ticketPerLevel;
    public List<int> HighScoreList;
    public List<int> firstStar;
    public List<int> secondStar;
    public List<int> thirdStar;
    public List<int> ticketSpawn;
    public enum GameStates
    {
        MainMenu,
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
        for (int i = 0; i < maxLevel; i++)
        {
            HighScoreList.Add(0);
        }
    }
    private void Start()
    {
        AddCoin(0);
        AddTicket(0);
        /*
        SaveData data = SaveSysteme.LoadData();
        level = data.level;
        coin = data.coin;
        ticket = data.ticket;
        for (int i = 0; i < data.highScoreList.Length; i++)
        {
            HighScoreList.Add(data.highScoreList[i]);
        }
        */
    }
    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SaveSysteme.Save(this);
        }
        if (Input.GetKeyDown("l"))
        {
            SaveData data = SaveSysteme.LoadData();
            level = data.level;
            coin = data.coin;
            ticket = data.ticket;
            for (int i = 0; i < data.highScoreList.Length; i++)
            {
                HighScoreList.Add(data.highScoreList[i]);
                ticketSpawn[i] = (data.ticketSpawn[i]);
            }
            this.UnlockLevel();
        }
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("Level : " + level);
            Debug.Log("Coin : " + coin);
            Debug.Log("Ticket : " + ticket);
            for (int i = 0; i < maxLevel; i++)
            {
                Debug.Log("HighScore Level : "+ (i+1) +" = "+HighScoreList[i]);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGameState(GameStates.GameOver);
        }
    }
    public void AddCoin(int numberOfCoin)
    {
        coin += numberOfCoin;
        coinPerLevel -= numberOfCoin;
        UIManagerBehaviour.instance.DisplayCoin();
    }
    public void AddTicket(int numberOfTicket)
    {
        ticket += numberOfTicket;
        ticketPerLevel -= numberOfTicket;
    }

    public void ResetCoinAndTicket()
    {
        AddCoin(coinPerLevel);
        AddTicket(ticketPerLevel);
        coinPerLevel = 0;
        ticketPerLevel = 0;
    }

    public void ChangeGameState(GameStates currentState)
    {
        _GameState = currentState;
        switch (_GameState)
        {
            case GameStates.MainMenu:
                UIManagerBehaviour.instance.SetMainMenuActive();
                UIManagerBehaviour.instance.StopAllCoroutines();
                if(!SoundManagerBehaviour.instance.mainMenuTheme.isPlaying)
                {
                    SoundManagerBehaviour.instance.PlayMainMenuTheme();
                }
                Time.timeScale = 1;
                break;
            case GameStates.InGame:
                UIManagerBehaviour.instance.SetHUDActive();
                UIManagerBehaviour.instance.StopAllCoroutines();
                Time.timeScale = 1;
                break;
            case GameStates.Pause:
                UIManagerBehaviour.instance.SetPauseActive();
                if (LevelManagerBehaviour.Instance.inGameTheme.isPlaying)
                {
                    LevelManagerBehaviour.Instance.inGameTheme.Pause();
                }
                Time.timeScale = 0;
                break;
            case GameStates.GameOver:
                UIManagerBehaviour.instance.SetGameOverActive();
                LevelManagerBehaviour.Instance.inGameTheme.Stop();
                SoundManagerBehaviour.instance.PlayGameOverTheme();
                break;
            case GameStates.Recipe:
                UIManagerBehaviour.instance.SetRecipeActive();
                ScenesManagerBehaviour.instance.LoadRecipeScene();
                SoundManagerBehaviour.instance.PlayBookMenuTheme();
                break;
        }
    }

    //Button UI
    public void ChangeGameStateByUI(int currentState)
    {
        switch (currentState)
        {
            case 0:
                if (GameState == GameStates.Pause)
                {
                    ScenesManagerBehaviour.instance.LoadMainMenuOnlyInGame();
                }
                else
                {
                    ScenesManagerBehaviour.instance.LoadMainMenu();
                }
                ChangeGameState(GameStates.MainMenu);
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
    public void UnlockLevel()
    {
        for (int i = 1; i < maxLevel; i++)
        {
            if (i <= GameManagerBehaviour.instance.level)
            {
                Debug.Log("Unlock Level " + (i));
            }
            if (i < GameManagerBehaviour.instance.level)
            {
                Debug.Log("Unlock recette " + (i));
            }
        }
    }
}
