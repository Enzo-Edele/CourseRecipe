using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Frederic

public class CollectibleBehaviour : MonoBehaviour
{
    public bool coin;
    public int amount;
    public GameObject particle;

    void Update()
    {
        if (transform.position.magnitude > 12.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour player = collision.GetComponent<PlayerBehaviour>();
        if(player != null)
        {
            if(coin)
            {
                GameManagerBehaviour.instance.AddCoin(amount);
                Instantiate(particle, this.transform.position, Quaternion.identity);
            }
            else
            {
                GameManagerBehaviour.instance.AddTicket(amount);
                Instantiate(particle, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
