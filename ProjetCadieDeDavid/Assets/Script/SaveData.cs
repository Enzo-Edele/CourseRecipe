using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level;
    public int coin;
    public int ticket;
    public int[] highScoreList = new int[GameManagerBehaviour.instance.scoreList.Count];

    public SaveData(GameManagerBehaviour gameData)
    {
        level = gameData.level;
        coin = gameData.coin;
        ticket = gameData.ticket;
        for(int i = 0; i < gameData.scoreList.Count; i++)
        {
            highScoreList[i] = gameData.scoreList[i];
        }
    }
}
