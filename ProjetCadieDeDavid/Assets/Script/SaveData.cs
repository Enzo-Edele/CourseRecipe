using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level;
    public int coin;
    public int ticket;
    public int achatMamieVelo;
    public int achatMamieScooter;
    public int[] highScoreList = new int[GameManagerBehaviour.instance.HighScoreList.Count];
    public int[] ticketSpawn = new int[GameManagerBehaviour.instance.ticketSpawn.Count];

    public SaveData(GameManagerBehaviour gameData)
    {
        level = gameData.level;
        coin = gameData.coin;
        ticket = gameData.ticket;
        achatMamieVelo = gameData.achatMamieVelo;
        achatMamieScooter = gameData.achatMamieScooter;
        for(int i = 0; i < gameData.HighScoreList.Count; i++)
        {
            highScoreList[i] = gameData.HighScoreList[i];
            ticketSpawn[i] = gameData.ticketSpawn[i];
        }
    }
}
