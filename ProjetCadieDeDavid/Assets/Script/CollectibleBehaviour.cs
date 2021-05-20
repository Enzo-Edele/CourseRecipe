using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public bool coin;
    public int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour player = collision.GetComponent<PlayerBehaviour>();
        if(player != null)
        {
            if(coin)
            {
                GameManagerBehaviour.instance.AddCoin(amount);
                Destroy(this.gameObject);
            }
            else
            {
                GameManagerBehaviour.instance.AddTicket(amount);
                Destroy(this.gameObject);
            }
        }
    }
}
