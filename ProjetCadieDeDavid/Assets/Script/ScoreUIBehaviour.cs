using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIBehaviour : MonoBehaviour
{
    public GameObject[] image;
    [SerializeField]
    float timer = 2;
    [SerializeField]
    float speed = 5;
    Vector2 position;
    Vector2 positionUI;
    TMP_Text scoreText;
    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        if(UIManagerBehaviour.instance.currentScoreUI < 0)
        {
            Instantiate(image[0], this.transform.position, Quaternion.identity, this.transform);
            scoreText.SetText(UIManagerBehaviour.instance.currentScoreUI.ToString());
        }
        else
        {
            Instantiate(image[1], this.transform.position, Quaternion.identity, this.transform);
            scoreText.text = "+ " + UIManagerBehaviour.instance.currentScoreUI;
        }
        positionUI = UIManagerBehaviour.instance.scoreText.transform.position;
    }

    private void Update()
    {
        Move();
        timer -= Time.deltaTime;
        if(timer < 0)
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
