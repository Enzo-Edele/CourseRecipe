using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    public bool right;
    float time = 0;
    public GameObject prefabEnemy;
    public float[] posY;

    void Update()
    {
        if(GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.InGame && LevelManagerBehaviour.LevelState == LevelManagerBehaviour.LevelStates.Collect)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Instantiate(prefabEnemy, RandomPosY(), Quaternion.identity, this.transform);
                time = RandomTime();
            }
        }
    }

    public int RandomSpeed()
    {
        int rngSpeed = Random.Range(LevelManagerBehaviour.Instance.minSpeed, LevelManagerBehaviour.Instance.maxSpeed);
        return rngSpeed;
    }

    float RandomTime()
    {
        float rngTime = Random.Range(LevelManagerBehaviour.Instance.minTime, LevelManagerBehaviour.Instance.maxTime);
        return rngTime;
    }

    Vector2 RandomPosY()
    {
        Vector2 position = this.transform.position;
        int pos = Random.Range(0, posY.Length);
        position.y = posY[pos];
        return position;
    }
}
