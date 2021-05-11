using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerBehavior : MonoBehaviour
{
    Vector3 positionSpawnItem;
    float positionRnd, positionRndMax = 0.05f, positionRndMin = 0.96f;
    float timer = 3, timeMin = 1, timeMax = 2;

    public GameObject[] rayonEpicerieUn = new GameObject[0];
    public GameObject[] rayonEpicerieDeux = new GameObject[0];
    public GameObject[] rayonLaitier = new GameObject[0];
    public GameObject[] rayonBouPoi = new GameObject[0];
    public GameObject[] rayonLegumeUn = new GameObject[0];
    public GameObject[] rayonLegumeDeux = new GameObject[0];
    public GameObject[] rayonFruit = new GameObject[0];
    List<GameObject> Rayon = new List<GameObject>();
    int tailleList;
    int rndArticle, memory;
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
    public string premierRayon;

    public GameObject panelInUse, panelUn;
    public GameObject panelEpUn, panelEpDeux, panelLaitier, panelBouPoi, panelLegUn, panelLegDeux, panelFruit;
    GameObject panelRayon;
    Vector3 positionStart;
    Vector3 positionSpawnPannel;
    [HideInInspector]
    public int panelTransition = 5;
    bool stopRunner = false;

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
        Instantiate(panelInUse, Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
    }
    void RayonChange(Rayons newState)
    {
        _rayonActuel = newState;
        switch (_rayonActuel)
        {
            case Rayons.RayonEpicerieUn:
                Rayon.Clear();
                for (int i = 0; i < rayonEpicerieUn.Length; i++)
                {
                    Rayon.Add(rayonEpicerieUn[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelEpUn;
                break;
            case Rayons.RayonEpicerieDeux:
                Rayon.Clear();
                for (int i = 0; i < rayonEpicerieDeux.Length; i++)
                {
                    Rayon.Add(rayonEpicerieDeux[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelEpDeux;
                break;
            case Rayons.RayonLaitier:
                Rayon.Clear();
                for (int i = 0; i < rayonLaitier.Length; i++)
                {
                    Rayon.Add(rayonLaitier[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelLaitier;
                break;
            case Rayons.RayonBouPoi:
                Rayon.Clear();
                for (int i = 0; i < rayonBouPoi.Length; i++)
                {
                    Rayon.Add(rayonBouPoi[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelBouPoi;
                break;
            case Rayons.RayonLegumeUn:
                Rayon.Clear();
                for (int i = 0; i < rayonLegumeUn.Length; i++)
                {
                    Rayon.Add(rayonLegumeUn[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelLegUn;
                break;
            case Rayons.RayonLegumeDeux:
                Rayon.Clear();
                for (int i = 0; i < rayonLegumeDeux.Length; i++)
                {
                    Rayon.Add(rayonLegumeDeux[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelLegDeux;
                break;
            case Rayons.RayonFruit:
                Rayon.Clear();
                for (int i = 0; i < rayonFruit.Length; i++)
                {
                    Rayon.Add(rayonFruit[i]);
                }
                tailleList = Rayon.Count;
                panelRayon = panelFruit;
                break;
        }
    }
    public void ChangeRayonState(string rayon)
    {
        switch(rayon)
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
    void Update()
    {
        if (timer < 0 && LevelManagerBehaviour.LevelState == LevelManagerBehaviour.LevelStates.Collect)
        {
            this.Generate();
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
        Instantiate(Rayon[rndArticle], positionSpawnItem, Quaternion.identity);
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
            Instantiate(panelInUse, Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
            panelTransition--;
        }
    }
}
