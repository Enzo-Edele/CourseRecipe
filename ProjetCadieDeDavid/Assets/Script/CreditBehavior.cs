using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enzo

public class CreditBehavior : MonoBehaviour
{
    Vector2 positionInit;
    Vector2 positionInitTHX;

    public float speed = 0.3f;
    public GameObject thank;
    public AudioSource créditTheme;
    public float timeCrédit = 10f;
    void Start()
    {
        SoundManagerBehaviour.instance.StopAllSound();
        créditTheme.Play();
        positionInit = transform.position;
        positionInitTHX = thank.transform.position;
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
            StartCoroutine(EndCrédit());
        }
    }
    public void StartCredit()
    {
        transform.position = positionInit;
        thank.transform.position = positionInitTHX;
    }

    IEnumerator EndCrédit()
    {
        yield return new WaitForSeconds(timeCrédit);
        GameManagerBehaviour.instance.ChangeGameStateByUI(0);
    }
}
