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

    private static moveStates _MoveStates;
    public static moveStates MoveStates;

    BoxCollider2D bc2d;
    float boxVar = 0.1f;
    float offsetVar = 0.03f;

    int capacité;
    int capacitéMax = 20;

    float speed = 20;
    float horizontal;
    float vertical;
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
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
            ChangeMoveState(moveStates.Jump);
        }
        else
        {
            ChangeMoveState(moveStates.Run);
        }
 
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
                break;
            case moveStates.Jump:
                break;
            case moveStates.Death:
                break;
        }
    }
}
