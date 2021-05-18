using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    int maxLevel = 5;

    private static LevelUnlocker _instance;
    public static LevelUnlocker instance
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
    //fonction quand un niveau fini pour actualiser progression
    /*
     * void EndLevel()
     * {
     *      if(score > GameManagerBehaviour.scoreList[SceneManagement.Scene.GetBuildIndex()])
     *      {
     *          GameManagerBehaviour.scoreList[SceneManagement.Scene.GetBuildIndex()] = score;
     *      }
     *      if((SceneManagement.Scene.GetBuildIndex()) == level)
     *      {
     *             level = SceneManagement.Scene.GetBuildIndex() + 1;
     *      }
     *}
    */
    public void UnlockLevel()
    {
        for(int i = 0; i < maxLevel; i++)
        {
            if(i <= GameManagerBehaviour.instance.level)
            {
                Debug.Log("Unlock Level " + i);
                //fctpour l'UI(i)
            }
            if (i < GameManagerBehaviour.instance.level)
            {
                Debug.Log("Unlock recette " + i);
                //fctpour l'UI(i)
            }
        }
    }
}
