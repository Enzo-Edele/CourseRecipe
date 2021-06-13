using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

//Frederic
//Enzo

public class LevelManagerBehaviour : MonoBehaviour
{
    public bool levelDone;
    public int score = 0;

    public int minSpeed;
    public int maxSpeed;
    public int minTime;
    public int maxTime;
    
    public int capaciteCaddie;
    public int playerLife;

    public GameObject menuBriefing;
    public string[] articleAskedArray; //article demander
    public int[] articleNumberArray; //nombre souhaiter
    public List <string> articleCurrentList = new List<string>();
    public List<int> articleCurrentNumberList = new List<int>();
    public List<int> articleSpawnedList = new List<int>(); //nombre de l'aricle spawner
    List<int> indexSpawnedList = new List<int>(); //recupére l'index de l'article a vérifié dans le rayon

    public string[] rayonArray;
    public int rayonInUse = 0;
    public float timeCollect = 10;
    float timerCollect;
    public int runnerLentgh = 2;


    public TMP_Text[] listHUDText;
    public TMP_Text[] listNumberHUDText;
    public GameObject[] miniatureArtcileArray;
    public AudioSource inGameTheme;

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
        for (int i = 0; i < articleAskedArray.Length; i++)
        {
            articleCurrentList.Add(articleAskedArray[i]);
            articleCurrentNumberList.Add(0);
        }
        for (int i = 0; i < miniatureArtcileArray.Length; i++)
        {
            miniatureArtcileArray[i].GetComponent<Image>().preserveAspect = true;
            miniatureArtcileArray[i].SetActive(false);
            UIManagerBehaviour.instance.listHUDText[i].fontStyle = FontStyles.Normal;
            UIManagerBehaviour.instance.listHUDText[i].SetText("");
            UIManagerBehaviour.instance.listHUDText[i].color = new Color32(0, 0, 0, 255);
            UIManagerBehaviour.instance.listNumberHUDText[i].SetText("");
            UIManagerBehaviour.instance.listNumberHUDText[i].color = new Color32(0, 0, 0, 255);
            UIManagerBehaviour.instance.miniatureArtcileArray[i].SetActive(false);
        }
        UIManagerBehaviour.instance.DisplayLevel();
        UIManagerBehaviour.instance.DisplayListHUD();
        //SpawnerManagerBehavior.Instance.ChangeRayonState(rayonArray[rayonInUse]);
    }
    private void Start()
    {
        ChangeLevelStates(LevelStates.LevelBriefing);
        for(int i = 0; i < articleNumberArray.Length; i++)
        {
            articleSpawnedList.Add(0);
        }
        SoundManagerBehaviour.instance.StopAllSound();
        inGameTheme.Play();
    }
    private void Update()
    {
        if(timerCollect < 0 && LevelState == LevelStates.Collect)
        {
            bool nextLevel = true;
            for (int i = 0; i < indexSpawnedList.Count; i++)
            {
                if (articleSpawnedList[indexSpawnedList[i]] < articleNumberArray[i] + articleNumberArray[i] * 0.1f)
                {
                    nextLevel = false;
                    Debug.Log(articleAskedArray[indexSpawnedList[i]]+" Spawned = " +articleSpawnedList[indexSpawnedList[i]]+" / "+ (articleNumberArray[i] + articleNumberArray[i] * 0.1f));
                }
            }
            if (nextLevel)
            {
                SpawnerManagerBehavior.Instance.panelTransition = runnerLentgh;
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
        }
        else
        {
            timerCollect -= Time.deltaTime;
        }
        if(menuBriefing.activeSelf && GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.InGame && LevelState != LevelStates.LevelBriefing)
        {
            menuBriefing.SetActive(false);
        }
    }
    public void testSpwanArticle(string art)
    {
        art.Replace("(Clone)", "");
        for(int i = 0; i < articleAskedArray.Length; i++)
        {
            if(art.Equals(articleAskedArray[i]))
            {
                articleSpawnedList[i]++;
                Debug.Log(articleAskedArray[i] + " : " + articleSpawnedList[i]);
            }
        }
    }
    public void DisplayList()
    {
        for (int i = 0; i < articleCurrentList.Count; i++)
        {
            miniatureArtcileArray[i].SetActive(true);
            miniatureArtcileArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(articleCurrentList[i]);
            listHUDText[i].text = articleCurrentList[i];
            if (i >= articleAskedArray.Length)
            {
                listHUDText[i].color = UIManagerBehaviour.instance.red;
                listNumberHUDText[i].color = UIManagerBehaviour.instance.red;
            }
            else
            {
                if (articleCurrentNumberList[i] == articleNumberArray[i])
                {
                    listHUDText[i].fontStyle = FontStyles.Strikethrough;
                    listNumberHUDText[i].color = UIManagerBehaviour.instance.green;
                }
            }
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
            if (articleCurrentList[i].Equals(nameArticle.Replace("2", "").Replace("3", "").Replace("(Clone)", "")))
            {
                if(i < articleNumberArray.Length)
                {
                    if (articleCurrentNumberList[i] < articleNumberArray[i])
                    {
                        found = true;
                        articleCurrentNumberList[i]++;
                        score += 500;
                        if (articleCurrentNumberList[i] == articleNumberArray[i])
                        {
                            Caddie caddie;
                            caddie = FindObjectOfType(typeof(Caddie)) as Caddie;
                            caddie.ParticlesEffect(2);
                        }
                        else if (articleCurrentNumberList[i] < articleNumberArray[i])
                        {
                            Caddie caddie;
                            caddie = FindObjectOfType(typeof(Caddie)) as Caddie;
                            caddie.ParticlesEffect(1);
                        }
                    }
                }
                else
                {
                    found = true;
                    articleCurrentNumberList[i]++;
                    score -= 50;
                    Caddie caddie;
                    caddie = FindObjectOfType(typeof(Caddie)) as Caddie;
                    caddie.ParticlesEffect(0);
                }
            }
        }
        if(!found)
        {
            if (articleCurrentList.Count >= miniatureArtcileArray.Length)
            {
                GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.GameOver);
            }
            else
            {
                articleCurrentList.Add(nameArticle.Replace("(Clone)", ""));
                articleCurrentNumberList.Add(1);
                score -= 50;
                Caddie caddie;
                caddie = FindObjectOfType(typeof(Caddie)) as Caddie;
                caddie.ParticlesEffect(0);
            }
        }
        UIManagerBehaviour.instance.DisplayListHUD();
        VerifArticles();
    }

    public void ChangeLevelStates(LevelStates currentState)
    {
        _levelState = currentState;
        switch (_levelState)
        {
            case LevelStates.LevelBriefing:
                UIManagerBehaviour.instance.HUD.SetActive(false);
                menuBriefing.SetActive(true);
                DisplayList();
                Time.timeScale = 0;
                break;
            case LevelStates.Collect:
                Time.timeScale = 1;
                timerCollect = timeCollect;
                indexSpawnedList.Clear();
                //test var
                int list = 0;
                for(int i = 0; i < articleAskedArray.Length; i++) //vérifier actualisation du nombre requis
                {
                    for (int y = 0; y < SpawnerManagerBehavior.Instance.rayon.Count; y++)
                    { 
                        if(articleAskedArray[i] == SpawnerManagerBehavior.Instance.rayon[y].name)
                        {
                            indexSpawnedList.Add(i);
                            Debug.Log("Change état : " + articleAskedArray[i] + " = " + SpawnerManagerBehavior.Instance.rayon[y].name);
                            Debug.Log("Nombre "+ articleAskedArray[i] + " requis : "+ indexSpawnedList[list]);
                            list++;
                        }
                    }
                }
                break;
            case LevelStates.Run:
                Time.timeScale = 1;
                break;
        }
    }

    void VerifArticles()
    {
        bool complete = true;
        for(int i =0; i < articleNumberArray.Length;i++)
        {
            if(articleCurrentNumberList[i] != articleNumberArray[i])
            {
                complete = false;
                break;
            }
        }
        levelDone = complete;
    }

    //Button
    public void ChangeLevelStatebyUI(int levelState)
    {
        SoundManagerBehaviour.instance.PlayButtonSound();
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
    public void ReturnBook()
    {
        GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.Recipe);
    }

    public void SetHUDOn()
    {
        UIManagerBehaviour.instance.HUD.SetActive(true);
    }
}
