using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Articles : MonoBehaviour
{
    public string nom;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
