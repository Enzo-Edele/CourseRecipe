using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerBehaviour : MonoBehaviour
{
    public int level;
    public bool levelDone;

    public float backgroundSpeed;

    public int minSpeed;
    public int maxSpeed;
    public int minTime;
    public int maxTime;

    public int capaciteCaddie;
    public int playerLife;

    public GameObject menuBriefing;
    public Image imageArticle;
    public Text articleName;
    int articlePos = 0;
    public string[] articleAskedArray;
    public int[] articleNumberArray;
    public List <string> articleCurrentList = new List<string>();
    public List<int> articleCurrentNumberList = new List<int>();

    public string[] rayonArray;
    public List<string> rayonList = new List<string>();
    int nombreRayon = 0;   

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
        for (int i = 0; i < rayonArray.Length; i++)
        {
            rayonList.Add(rayonArray[i]);
        }
    }
    private void Start()
    {
        for(int i = 0; i < articleAskedArray.Length; i++)
        {
            articleCurrentList.Add(articleAskedArray[i]);
            articleCurrentNumberList.Add(0);
        }
        UIManagerBehaviour.instance.DisplayLevel();
        UIManagerBehaviour.instance.DisplayListHUD();
        imageArticle.preserveAspect = true;
        ChangeSprite(0);
    }

    public void ChangeLevelStates(LevelStates currentState)
    {
        _LevelState = currentState;
        switch(_LevelState)
        {
            case LevelStates.LevelBriefing:
                UIManagerBehaviour.instance.HUD.SetActive(false);
                menuBriefing.SetActive(true);
                Time.timeScale = 0;
                break;
            case LevelStates.Collect:
                UIManagerBehaviour.instance.HUD.SetActive(true);
                menuBriefing.SetActive(false);
                Time.timeScale = 1;
                break;
            case LevelStates.Run:
                UIManagerBehaviour.instance.HUD.SetActive(true);
                menuBriefing.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }

    //Button
    public void ChangeLevelStatebyUI(int levelState)
    {
        switch (levelState)
        {
            case 1:
                ChangeLevelStates(LevelManagerBehaviour.LevelStates.Collect);
                break;
            case 2:
                ChangeLevelStates(LevelManagerBehaviour.LevelStates.Run);
                break;
        }
    }

    public void ChangeSprite(int amount)
    {
        if(articlePos + amount >= 0 && articlePos + amount < articleAskedArray.Length)
        {
            articlePos += amount;
            imageArticle.sprite = Resources.Load<Sprite>(articleAskedArray[articlePos]);
            articleName.text = articleAskedArray[articlePos];
        }
    }

    public void AddInArticleList(string nameArticle)
    {
        bool found = false;
        for (int i = 0; i < articleCurrentList.Count; i++)
        {
            if (articleCurrentList[i].Equals(nameArticle.Replace("(Clone)", "")))
            {
                articleCurrentNumberList[i]++;
                found = true;
                if( i < articleNumberArray.Length)
                {
                    if (articleCurrentNumberList[i] == articleNumberArray[i])
                    {
                        Caddie caddie;
                        caddie = FindObjectOfType(typeof(Caddie)) as Caddie;
                        caddie.ParticlesEffect();
                    }
                }
            }
        }
        if(!found)
        {
            articleCurrentList.Add(nameArticle.Replace("(Clone)", ""));
            articleCurrentNumberList.Add(1);
        }
        UIManagerBehaviour.instance.DisplayListHUD();
    }
}
