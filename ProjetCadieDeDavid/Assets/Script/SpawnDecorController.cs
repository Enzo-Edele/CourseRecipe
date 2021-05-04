using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDecorController : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    Vector3 positionStart;
    Vector3 positionSpwan;
    void Start()
    {
        positionStart = Camera.main.WorldToScreenPoint(transform.position);
        Instantiate(panel, Camera.main.ScreenToWorldPoint(positionStart), Quaternion.identity);
        positionSpwan = Camera.main.WorldToScreenPoint(transform.position);
        positionSpwan.x = Screen.width * 1.5f;
        Instantiate(panel, Camera.main.ScreenToWorldPoint(positionSpwan), Quaternion.identity);
    }
    public void SpawnDecor()
    {
        Instantiate(panel, Camera.main.ScreenToWorldPoint(positionSpwan), Quaternion.identity);
    }
}
