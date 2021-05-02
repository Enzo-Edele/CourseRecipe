using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    public bool right;
    float time = 0;
    public GameObject prefabEnemy;

    void Update()
    {
        time -= Time.deltaTime;
        if(time <0)
        {
            Instantiate(prefabEnemy, this.transform.position, Quaternion.identity,this.transform);
            time = RandomTime();
        }
    }

    public int RandomSpeed()
    {
        int rngSpeed = Random.Range(LevelManagerBehaviour.instance.minSpeed, LevelManagerBehaviour.instance.maxSpeed);
        return rngSpeed;
    }

    float RandomTime()
    {
        float rngTime = Random.Range(LevelManagerBehaviour.instance.minTime, LevelManagerBehaviour.instance.maxTime);
        return rngTime;
    }
}
