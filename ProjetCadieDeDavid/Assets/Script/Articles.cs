using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Articles : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public float speed, speedMin = 3, speedMax = 6;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = Random.Range(speedMin, speedMax);
    }

    void FixedUpdate()
    {
        if(GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.InGame)
        {
            if (rb2d != null)
            {
                Vector2 position = rb2d.position;
                position.y = position.y - speed * Time.deltaTime;
                rb2d.MovePosition(position);
            }
            if (transform.position.magnitude > 12.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
