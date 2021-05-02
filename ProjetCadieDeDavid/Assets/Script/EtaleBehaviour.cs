using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtaleBehaviour : MonoBehaviour
{
    PlatformEffector2D effector2D;
    [SerializeField]
    bool floor = false;
    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }
    /*private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        if(vertical <0)
        {

            time -= Time.deltaTime;
            if (time < 0)
            {
                ChangeEffector2D(180f);
                time = timer;
            }
        }
    }*/
    public void ChangeEffector2D(float rotation)
    {
        effector2D.rotationalOffset = rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Caddie caddie = collision.collider.GetComponent<Caddie>();
        if (caddie != null)
        {
            if (floor & collision.relativeVelocity.y < 0)
            {
                caddie.ChangeMoveState(Caddie.MoveStates.Run);
            }
        }
    }
}
