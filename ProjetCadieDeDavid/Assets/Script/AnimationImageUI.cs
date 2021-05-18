using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationImageUI : MonoBehaviour
{
    Image image;
    public Sprite[] sprite;
    public float timer = 0.1f;
    int posSprite = 0;
    float time;
    void Start()
    {
        image = GetComponent<Image>();
        time = timer;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            ChangeSpriteRun();
            time = timer;
        }
    }

    void ChangeSpriteRun()
    {
        if (posSprite >= sprite.Length)
        {
            posSprite = 0;
        }
        image.sprite = sprite[posSprite];
        posSprite++;
    }
}
