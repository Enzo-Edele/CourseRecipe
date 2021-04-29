using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArticle : MonoBehaviour
{
    public GameObject knaki, fraise, fromage, myrtille;
    int tailleList;
    List<GameObject> Rayon = new List<GameObject>();
    GameObject articlesSelect, memory;
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
                Rayon.Add(knaki);
                Rayon.Add(fromage);
                tailleList = Rayon.Count;
                break;
            case Rayons.Rayon2:
                Rayon.Clear();
                Rayon.Add(fraise);
                Rayon.Add(myrtille);
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
        positionRnd = Random.Range(0.1f, 0.2f);
        positionSpawn.x = Screen.width * positionRnd;
        positionSpawn.y = Screen.height * 1.1f;
        positionSpawn = Camera.main.ScreenToWorldPoint(positionSpawn);
        positionSpawn.z = 0;
        if (knaki != null)
        {
            Instantiate(Rayon[Random.Range(0, tailleList)], positionSpawn, Quaternion.identity);
        }
        timer = Random.Range(timeMin, timeMax);
    }
}
