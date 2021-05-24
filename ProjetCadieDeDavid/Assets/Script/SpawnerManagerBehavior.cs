using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerBehavior : MonoBehaviour
{
    Vector3 positionSpawnItem;
    float positionRnd, positionRndMax = 0.05f, positionRndMin = 0.96f;
    float timer = 3;
    [HideInInspector]
    public float timeMin = 1, timeMax = 2;

    public string premierRayon;
    public GameObject[] rayonEpicerieUn = new GameObject[0];
    public GameObject[] rayonEpicerieDeux = new GameObject[0];
    public GameObject[] rayonLaitier = new GameObject[0];
    public GameObject[] rayonBouPoi = new GameObject[0];
    public GameObject[] rayonLegumeUn = new GameObject[0];
    public GameObject[] rayonLegumeDeux = new GameObject[0];
    public GameObject[] rayonFruit = new GameObject[0];
    public List<GameObject> rayon = new List<GameObject>();
    int tailleList;
    int rndArticle, memory, rndPanel;


    public GameObject[] panel;
    public GameObject panelEpUn, panelEpDeux, panelLaitier, panelBouPoi, panelLegUn, panelLegDeux, panelFruit;
    GameObject panelRayon;
    public GameObject coin;
    Vector3 positionStart;
    Vector3 positionSpawnPannel;
    [HideInInspector]
    public int panelTransition;
    bool stopRunner = false;
    public enum Rayons
    {
        RayonEpicerieUn,
        RayonEpicerieDeux,
        RayonLaitier,
        RayonBouPoi,
        RayonLegumeUn,
        RayonLegumeDeux,
        RayonFruit
    }
    private Rayons _rayonActuel;
    public Rayons _RayonsActuel
    {
        get
        {
            return _rayonActuel;
        }
    }

    private static SpawnerManagerBehavior _instance;
    public static SpawnerManagerBehavior Instance
    {
        get
        {
            return _instance;
        }
    }
    void Start()
    {
        _instance = this;
        ChangeRayonState(premierRayon);
        positionSpawnItem = Camera.main.WorldToScreenPoint(transform.position);
        positionStart = Camera.main.WorldToScreenPoint(transform.position);
        Instantiate(panelRayon, Camera.main.ScreenToWorldPoint(positionStart), Quaternion.identity);
        positionSpawnPannel = Camera.main.WorldToScreenPoint(transform.position);
        positionSpawnPannel.x = Screen.width * 1.5f;
        rndPanel = Random.Range(0, panel.Length);
        Instantiate(panel[rndPanel], Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
    }
    void Update()
    {
        if (timer < 0 && LevelManagerBehaviour.LevelState == LevelManagerBehaviour.LevelStates.Collect && GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.InGame)
        {
            this.Generate();
        }
        else if (timer <0 && LevelManagerBehaviour.LevelState == LevelManagerBehaviour.LevelStates.Run && GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.InGame)
        {
            GenerateCoin();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    void Generate()
    {
        positionSpawnItem = Camera.main.WorldToScreenPoint(transform.position);
        positionRnd = Random.Range(positionRndMin, positionRndMax);
        positionSpawnItem.x = Screen.width * positionRnd;
        positionSpawnItem.y = Screen.height * 1.1f;
        positionSpawnItem = Camera.main.ScreenToWorldPoint(positionSpawnItem);
        positionSpawnItem.z = 0;
        rndArticle = Random.Range(0, tailleList);
        while (rndArticle == memory)
        {
            rndArticle = Random.Range(0, tailleList);
        }
        memory = rndArticle;
        Instantiate(rayon[rndArticle], positionSpawnItem, Quaternion.identity);
        timer = Random.Range(timeMin, timeMax);
    }

    void GenerateCoin()
    {
        positionSpawnItem = Camera.main.WorldToScreenPoint(transform.position);
        positionRnd = Random.Range(positionRndMin, positionRndMax);
        positionSpawnItem.x = Screen.width * positionRnd;
        positionSpawnItem.y = Screen.height * 1.1f;
        positionSpawnItem = Camera.main.ScreenToWorldPoint(positionSpawnItem);
        positionSpawnItem.z = 0;
        Instantiate(coin, positionSpawnItem, Quaternion.identity);
        timer = Random.Range(timeMin, timeMax);
    }
    public void SpawnDecor()
    {
        if (stopRunner)
        {
            LevelManagerBehaviour.Instance.ChangeLevelStates(LevelManagerBehaviour.LevelStates.Collect);
            stopRunner = false;
        }
        if (panelTransition == 1)
        {
            Instantiate(panelRayon, Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
            stopRunner = true;
            panelTransition--;
        }
        else
        {
            rndPanel = Random.Range(0, panel.Length);
            Instantiate(panel[rndPanel], Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
            panelTransition--;
        }
    }

    void RayonChange(Rayons newState)
    {
        _rayonActuel = newState;
        switch (_rayonActuel)
        {
            case Rayons.RayonEpicerieUn:
                rayon.Clear();
                for (int i = 0; i < rayonEpicerieUn.Length; i++)
                {
                    rayon.Add(rayonEpicerieUn[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelEpUn;
                break;
            case Rayons.RayonEpicerieDeux:
                rayon.Clear();
                for (int i = 0; i < rayonEpicerieDeux.Length; i++)
                {
                    rayon.Add(rayonEpicerieDeux[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelEpDeux;
                break;
            case Rayons.RayonLaitier:
                rayon.Clear();
                for (int i = 0; i < rayonLaitier.Length; i++)
                {
                    rayon.Add(rayonLaitier[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelLaitier;
                break;
            case Rayons.RayonBouPoi:
                rayon.Clear();
                for (int i = 0; i < rayonBouPoi.Length; i++)
                {
                    rayon.Add(rayonBouPoi[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelBouPoi;
                break;
            case Rayons.RayonLegumeUn:
                rayon.Clear();
                for (int i = 0; i < rayonLegumeUn.Length; i++)
                {
                    rayon.Add(rayonLegumeUn[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelLegUn;
                break;
            case Rayons.RayonLegumeDeux:
                rayon.Clear();
                for (int i = 0; i < rayonLegumeDeux.Length; i++)
                {
                    rayon.Add(rayonLegumeDeux[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelLegDeux;
                break;
            case Rayons.RayonFruit:
                rayon.Clear();
                for (int i = 0; i < rayonFruit.Length; i++)
                {
                    rayon.Add(rayonFruit[i]);
                }
                tailleList = rayon.Count;
                panelRayon = panelFruit;
                break;
        }
    }
    public void ChangeRayonState(string rayon)
    {
        switch (rayon)
        {
            case "EpicerieUn":
                this.RayonChange(Rayons.RayonEpicerieUn);
                break;
            case "EpicerieDeux":
                this.RayonChange(Rayons.RayonEpicerieDeux);
                break;
            case "Laitier":
                this.RayonChange(Rayons.RayonLaitier);
                break;
            case "BouPoi":
                this.RayonChange(Rayons.RayonBouPoi);
                break;
            case "LegumeUn":
                this.RayonChange(Rayons.RayonLegumeUn);
                break;
            case "LegumeDeux":
                this.RayonChange(Rayons.RayonLegumeDeux);
                break;
            case "Fruit":
                this.RayonChange(Rayons.RayonFruit);
                break;
        }
    }
}
