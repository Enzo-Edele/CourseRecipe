using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    float speed = 500;
    SpawnerManagerBehavior panelSpawner;
    void Update()
    {
        if (LevelManagerBehaviour.LevelState == LevelManagerBehaviour.LevelStates.Run)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x -= speed * Time.deltaTime;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
            if (pos.x <= -Screen.width * 0.5f)
            {
                panelSpawner = FindObjectOfType(typeof(SpawnerManagerBehavior)) as SpawnerManagerBehavior;
                panelSpawner.SpawnDecor();
                Destroy(gameObject);
            }
        }
    }
}
