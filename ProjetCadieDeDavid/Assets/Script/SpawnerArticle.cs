using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArticle : MonoBehaviour
{
    public GameObject[] rayonLaitier = new GameObject[0];
    public GameObject[] rayonFruit = new GameObject[0];
    List<GameObject> Rayon = new List<GameObject>();
    int tailleList;
    int rndArticle, memory;
    public enum Rayons
    {
        Rayon1,
        Rayon2,
        Rayon3
    }
    private Rayons _rayonActuel;
    public Rayons _RayonsActuel
    {
        get
        {
            return _rayonActuel;
        }
    }

    Vector3 positionSpawn;
    float positionRnd;
    float timer = 3, timeMin = 3, timeMax = 8;

    void Start()
    {
        positionSpawn = Camera.main.WorldToScreenPoint(transform.position);
        this.RayonChange(Rayons.Rayon1);
    }
    public void RayonChange(Rayons newState)
    {
        _rayonActuel = newState;
        switch (_rayonActuel)
        {
            case Rayons.Rayon1:
                Rayon.Clear();
                for(int i = 0; i < rayonLaitier.Length; i++)
                {
                    Rayon.Add(rayonLaitier[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.Rayon2:
                Rayon.Clear();
                for (int i = 0; i < rayonFruit.Length; i++)
                {
                    Rayon.Add(rayonFruit[i]);
                }
                tailleList = Rayon.Count;
                break;
            case Rayons.Rayon3:
                Rayon.Clear();
                break;
        }
    }
    void Update()
    {
        if(timer < 0)
        {
            this.Generate();
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if(Input.GetKeyDown("1"))
        {
            this.RayonChange(Rayons.Rayon1);
        }
        else if (Input.GetKeyDown("2"))
        {
            this.RayonChange(Rayons.Rayon2);
        }
    }
    void Generate()
    {
        positionSpawn = Camera.main.WorldToScreenPoint(transform.position);
        positionRnd = Random.Range(0.1f, 0.9f);
        positionSpawn.x = Screen.width * positionRnd;
        positionSpawn.y = Screen.height * 1.1f;
        positionSpawn = Camera.main.ScreenToWorldPoint(positionSpawn);
        positionSpawn.z = 0;
        rndArticle = Random.Range(0, tailleList);
        while(rndArticle == memory)
        {
            rndArticle = Random.Range(0, tailleList);
        }
        memory = rndArticle;
        Instantiate(Rayon[rndArticle], positionSpawn, Quaternion.identity);
        timer = Random.Range(timeMin, timeMax);
    }
}
