using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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
    public Text articleName;
    public string[] articleAskedArray;
    public int[] articleNumberArray;
    public List <string> articleCurrentList = new List<string>();
    public List<int> articleCurrentNumberList = new List<int>();

    public string[] rayonArray;
    int rayonInUse = 0;
    public float timeCollect = 10;
    float timerCollect;
    public int runnerLenght = 2;


    public TMP_Text[] listHUDText;
    public TMP_Text[] listNumberHUDText;
    public GameObject[] miniatureArtcileArray;

    public enum LevelStates
    {
        LevelBriefing,
        Collect,
        Run,
    }
    private static LevelStates _levelState;
    public static LevelStates LevelState
    {
        get
        {
            return _levelState;
        }
    }
    private static LevelManagerBehaviour _instance;
    public static LevelManagerBehaviour Instance
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
    private void Start()
    {
        for(int i = 0; i < articleAskedArray.Length; i++)
        {
            articleCurrentList.Add(articleAskedArray[i]);
            articleCurrentNumberList.Add(0);
        }
        UIManagerBehaviour.instance.DisplayLevel();
        UIManagerBehaviour.instance.DisplayListHUD();
        ChangeLevelStates(LevelStates.LevelBriefing);
    }
    private void Update()
    {
        if(timerCollect < 0 && LevelState == LevelStates.Collect)
        {
            SpawnerManagerBehavior.Instance.panelTransition = runnerLenght;
            rayonInUse++;
            if (rayonInUse < rayonArray.Length)
            {
                SpawnerManagerBehavior.Instance.ChangeRayonState(rayonArray[rayonInUse]);
            }
            else
            {
                GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.GameOver);
            }
            ChangeLevelStates(LevelStates.Run);
        }
        else
        {
            timerCollect -= Time.deltaTime;
        }
    }
    public void ChangeLevelStates(LevelStates currentState)
    {
        _levelState = currentState;
        switch(_levelState)
        {
            case LevelStates.LevelBriefing:
                UIManagerBehaviour.instance.HUD.SetActive(false);
                menuBriefing.SetActive(true);
                DisplayList();
                Time.timeScale = 0;
                break;
            case LevelStates.Collect:
                UIManagerBehaviour.instance.HUD.SetActive(true);
                menuBriefing.SetActive(false);
                Time.timeScale = 1;
                timerCollect = timeCollect;
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

    void DisplayList()
    {
        for (int i = 0; i < articleCurrentList.Count; i++)
        {
            miniatureArtcileArray[i].SetActive(true);
            miniatureArtcileArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(Instance.articleCurrentList[i]);
            listHUDText[i].text = Instance.articleCurrentList[i];
            listNumberHUDText[i].SetText(articleCurrentNumberList[i].ToString());
            if (i < articleNumberArray.Length)
            {
                listNumberHUDText[i].text += "/" + articleNumberArray[i].ToString();
            }
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
                        caddie.ParticlesEffect(2);
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
