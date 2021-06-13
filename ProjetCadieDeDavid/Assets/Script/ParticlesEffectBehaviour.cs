using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Frederic

public class ParticlesEffectBehaviour : MonoBehaviour
{
    public float speed = 2f;
    public float time = 5;
    Vector2 positionUI;
    Vector2 position;

    private void Start()
    {
        positionUI = Camera.main.ScreenToWorldPoint(UIManagerBehaviour.instance.miniatureArtcileArray[0].transform.position);
    }
    void Update()
    {
        Move();
        time -= Time.deltaTime;
        if (time < 0 || GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.GameOver || GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.Pause)
        {
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        position = transform.position;
        float newSpeed = (positionUI.x - position.x) * speed;
        MoveAxisX(newSpeed);
        newSpeed = (positionUI.y - position.y) * speed;
        MoveAxisY(newSpeed);
        transform.position = position;
    }
    void MoveAxisX(float newSpeed)
    {
        if (position.x < positionUI.x)
        {
            position.x += Mathf.Abs(newSpeed) * Time.deltaTime;
        }
        else
        {
            position.x -= Mathf.Abs(newSpeed) * Time.deltaTime;
        }
    }

    void MoveAxisY(float newSpeed)
    {
        if (position.y < positionUI.y)
        {
            position.y += Mathf.Abs(newSpeed) * Time.deltaTime;
        }
        else
        {
            position.y -= Mathf.Abs(newSpeed) * Time.deltaTime;
        }
    }
}
