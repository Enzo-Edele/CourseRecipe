using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuteItem : MonoBehaviour
{
    public GameObject[] rayon = new GameObject[0];
    int tailleList;
    int rndArticle, memory;
    Vector3 positionSpawnItem;
    float positionRnd, positionRndMax = 0.5f, positionRndMin = 0.95f;
    float timeMin = 1, timeMax = 3, timer = 2;

    private void Start()
    {
        tailleList = rayon.Length;
    }
    void Update()
    {
        if (timer < 0)
        {
            this.Generate();
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
}
