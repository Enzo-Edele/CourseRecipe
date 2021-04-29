using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    public float speed;
    public bool left;
    public float TimerSpawn = 10;
    float time = 0;
    [SerializeField]
    GameObject prefabEnemy;
    void Start()
    {
        
    }

    void Update()
    {
        time -= Time.deltaTime;
        if(time <0)
        {
            Instantiate(prefabEnemy, this.transform.position, Quaternion.identity,this.transform);
        }
    }
}
