using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIBehaviour : MonoBehaviour
{
    public Image image;

    private void Start()
    {
        Instantiate(image, this.transform.position, Quaternion.identity);
    }
}
