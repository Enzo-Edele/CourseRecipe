using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    float speed = 500;
    SpawnDecorController panelSpawner;
    void Update()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.x -= speed * Time.deltaTime;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
        if(pos.x <= -Screen.width * 0.5f)
        {
            panelSpawner = FindObjectOfType(typeof(SpawnDecorController)) as SpawnDecorController;
            panelSpawner.SpawnDecor();
            Destroy(gameObject);
        }
    }
}
