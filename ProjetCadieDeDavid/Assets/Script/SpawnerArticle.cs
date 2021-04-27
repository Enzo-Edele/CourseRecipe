using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArticle : MonoBehaviour
{
    [SerializeField]
    GameObject Knaki;
    Vector3 positionSpawn;
    float positionRnd;
    float timeRnd;
    float timer = 3;

    GameObject memory;
    void Start()
    {
        positionSpawn = Camera.main.WorldToScreenPoint(transform.position);
    }
    void Update()
    {
        if(timer > 0)
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
        positionSpawn = Camera.main.WorldToScreenPoint(transform.position);
        positionRnd = Random.Range(0.1f, 0.91f);
        positionSpawn.x = Screen.width * positionRnd;
        Instantiate(Knaki, positionSpawn, Quaternion.identity);
        timer = Random.Range(2, 8);
    }
}
