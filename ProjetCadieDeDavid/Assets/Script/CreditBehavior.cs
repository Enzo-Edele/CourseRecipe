using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBehavior : MonoBehaviour
{
    public float speed = 0.3f;
    public GameObject thank;
    void Start()
    {
        
    }
    void Update()
    {
        Vector2 position = transform.position;
        position.y = position.y + speed;
        transform.position = position;
        Vector2 positionThank = thank.transform.position;
        if(positionThank.y < Screen.height * 0.5f)
        {
            positionThank.y = positionThank.y + speed;
            thank.transform.position = positionThank;
        }
    }
}
