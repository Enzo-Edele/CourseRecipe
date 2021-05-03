using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Articles : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public string nom;

    public float speed;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = Random.Range(3, 6);
    }
    private void Update()
    {
        Vector2 position = transform.position;
        position.y -= speed * Time.deltaTime;
        transform.position = position;
    }
    void FixedUpdate()
    {
        /*
        if (rb2d != null)
        {
            Vector2 position = rb2d.position;
            position.y = position.y - speed * Time.deltaTime;
            rb2d.MovePosition(position);
        }
        */
        if(transform.position.magnitude > 10.0f)
        {
            Destroy(gameObject);
        }
    }
}
