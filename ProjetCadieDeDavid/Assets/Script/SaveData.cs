using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level;
    public int coin;
    public int ticket;

    public SaveData(GameManagerBehaviour gameData)
    {
        level = gameData.level;
        coin = gameData.coin;
        ticket = gameData.ticket;
    }
}
