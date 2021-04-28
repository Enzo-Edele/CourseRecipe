using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtaleBehaviour : MonoBehaviour
{
    PlatformEffector2D effector2D;
    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    public void ChangeEffector2D(float rotation)
    {
        effector2D.rotationalOffset = rotation;
    }
}
