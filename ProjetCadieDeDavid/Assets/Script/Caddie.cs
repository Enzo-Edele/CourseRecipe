using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caddie : MonoBehaviour
{
    public enum moveStates
    {
        Run,
        Jump,
        Death,
    }

    private moveStates _MoveStates;
    public moveStates MoveStates;

    BoxCollider2D bc2d;
    float boxVar = 0.1f;
    float offsetVar = 0.03f;

    int capacité;
    int capacitéMax = 20;

    float speed = 20;
    float horizontal;
    float vertical;

    Rigidbody2D rb2D;
    float timer =5f;
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        capacité = capacitéMax;
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        timer -=Time.deltaTime;
        if(timer <0)
        {
            ChangeMoveState(moveStates.Run);
            timer = 5f;
        }
        //Debug.Log(timer);
    }
    void Jump()
    {
        Vector2 jump = this.transform.position;
        jump.y += 10;
        rb2D.velocity = jump;
        ChangeMoveState(moveStates.Jump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Articles recup = collision.GetComponent<Articles>();
        capacité -= 1;
        Vector2 box = bc2d.size;
        box.y += boxVar;
        bc2d.size = box;
        Vector2 offset = bc2d.offset;
        offset.y += offsetVar;
        bc2d.offset = offset;
    }

    public void ChangeMoveState(moveStates currentState)
    {
        _MoveStates = currentState;
        switch(_MoveStates)
        {
            case moveStates.Run:
                //rb2D.gravityScale = 0;
                break;
            case moveStates.Jump:
                //rb2D.gravityScale = 1;
                break;
            case moveStates.Death:
                break;
        }
    }
}
