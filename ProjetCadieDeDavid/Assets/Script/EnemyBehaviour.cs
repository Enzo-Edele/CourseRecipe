using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    float speed;
    EnemySpawnerBehaviour parent;
    SpriteRenderer sprite;
    void Start()
    {
        GameObject daddyGO = this.transform.parent.gameObject;
        parent = daddyGO.GetComponent<EnemySpawnerBehaviour>();
        speed = parent.speed;
        if(parent.left)
        {
            speed *=-1;
            sprite = GetComponent<SpriteRenderer>();
            sprite.flipX = true;
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 position = this.transform.position;
        position.x += speed * Time.deltaTime;
        this.transform.position = position;
    }
}
