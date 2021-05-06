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
    public GameObject[] rayonBoucheriePoissonnerie = new GameObject[0];
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
        RayonBoucheriePoissonnerie,
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

    public GameObject panelInUse, panelUn;
    public GameObject panelEpUn, panelEpDeux, panelLaitier, panelBouPoi, panelLegUn, panelLegDeux, panelFruit;
    Vector3 positionStart;
    Vector3 positionSpawnPannel;
    bool stopRunner = false;
    void Start()
    {
        positionSpawnItem = Camera.main.WorldToScreenPoint(transform.position);
        this.RayonChange(Rayons.RayonEpicerieUn);
        positionStart = Camera.main.WorldToScreenPoint(transform.position);
        Instantiate(panelInUse, Camera.main.ScreenToWorldPoint(positionStart), Quaternion.identity);
        positionSpawnPannel = Camera.main.WorldToScreenPoint(transform.position);
        positionSpawnPannel.x = Screen.width * 1.5f;
        Instantiate(panelInUse, Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
    }
    public void RayonChange(Rayons newState)
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
                break;
            case Rayons.RayonEpicerieDeux:
                Rayon.Clear();
                for (int i = 0; i < rayonEpicerieDeux.Length; i++)
                {
                    Rayon.Add(rayonEpicerieDeux[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.RayonLaitier:
                Rayon.Clear();
                for (int i = 0; i < rayonLaitier.Length; i++)
                {
                    Rayon.Add(rayonLaitier[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.RayonBoucheriePoissonnerie:
                Rayon.Clear();
                for (int i = 0; i < rayonBoucheriePoissonnerie.Length; i++)
                {
                    Rayon.Add(rayonBoucheriePoissonnerie[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.RayonLegumeUn:
                Rayon.Clear();
                for (int i = 0; i < rayonLegumeUn.Length; i++)
                {
                    Rayon.Add(rayonLegumeUn[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.RayonLegumeDeux:
                Rayon.Clear();
                for (int i = 0; i < rayonLegumeDeux.Length; i++)
                {
                    Rayon.Add(rayonLegumeDeux[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.RayonFruit:
                Rayon.Clear();
                for (int i = 0; i < rayonFruit.Length; i++)
                {
                    Rayon.Add(rayonFruit[i]);
                }
                tailleList = Rayon.Count;
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

        if (Input.GetKeyDown("[1]"))
        {
            this.RayonChange(Rayons.RayonEpicerieUn);
        }
        else if (Input.GetKeyDown("[2]"))
        {
            this.RayonChange(Rayons.RayonEpicerieDeux);
        }
        else if (Input.GetKeyDown("[3]"))
        {
            this.RayonChange(Rayons.RayonLaitier);
        }
        else if (Input.GetKeyDown("[4]"))
        {
            this.RayonChange(Rayons.RayonBoucheriePoissonnerie);
        }
        else if (Input.GetKeyDown("[5]"))
        {
            this.RayonChange(Rayons.RayonLegumeUn);
        }
        else if (Input.GetKeyDown("[6]"))
        {
            this.RayonChange(Rayons.RayonLegumeDeux);
        }
        else if (Input.GetKeyDown("[7]"))
        {
            this.RayonChange(Rayons.RayonFruit);
        }
        if (Input.GetKeyDown("1"))
        {
            LevelManagerBehaviour.instance.ChangeLevelStates(LevelManagerBehaviour.LevelStates.Run);
        }
        else if (Input.GetKeyDown("2"))
        {
            stopRunner = true;
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
        Instantiate(panelInUse, Camera.main.ScreenToWorldPoint(positionSpawnPannel), Quaternion.identity);
        if (stopRunner)
        {
            LevelManagerBehaviour.instance.ChangeLevelStates(LevelManagerBehaviour.LevelStates.Collect);
            stopRunner = false;
        }
    }
}
