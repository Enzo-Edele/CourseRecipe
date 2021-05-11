using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caddie : MonoBehaviour
{
    public enum PlayerStates
    {
        Run,
        Death,
    }

    private static PlayerStates _moveState;
    public static PlayerStates _MoveState
    {
        get
        {
            return _moveState;
        }
    }

    float boxVar = 0.075f;
    float offsetVar = 0.04f;

    int capacité;
    int capacitéMax = 20;

    BoxCollider2D bc2d;
    Rigidbody2D rb2D;

    public float[] posY;
    int currentPos = 0;
    [SerializeField]
    float timer = 1;
    float time = 0;
    [SerializeField]
    float speed = 5;
    int life;
    float horizontal;
    float vertical;

    public GameObject[] starEffect;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        capacitéMax = LevelManagerBehaviour.Instance.capaciteCaddie;
        capacité = capacitéMax;
        life = LevelManagerBehaviour.Instance.playerLife;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        Move();
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 position = this.transform.position;
        if(position.x + speed * horizontal * Time.deltaTime >= -8 && position.x + speed * horizontal * Time.deltaTime <= 8)
        {
            position.x += speed * horizontal * Time.deltaTime;
        }
        if(time < 0)
        {
            if (vertical > 0)
            {
                if (currentPos < 2)
                {
                    currentPos++;
                }
                position.y = posY[currentPos];
            }
            else if (vertical < 0)
            {
                if (currentPos > 0)
                {
                    currentPos--;
                }
                position.y = posY[currentPos];
            }
            time = timer;
        }
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.gravityScale = 1;
        rb2D.gravityScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Articles article = collision.GetComponent<Articles>();
        if(article != null)
        {
            if (capacité > 0)
            {
                capacité -= 1;
                collision.gameObject.transform.SetParent(this.transform);
                Destroy(collision.attachedRigidbody);
                article.speed = 0;
                Vector2 box = bc2d.size;
                box.y += boxVar;
                bc2d.size = box;
                Vector2 offset = bc2d.offset;
                offset.y += offsetVar;
                bc2d.offset = offset;
                LevelManagerBehaviour.Instance.AddInArticleList(collision.GetComponent<SpriteRenderer>().sprite.name);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void ChangeLife(int value)
    {
        life += value;
        if(life <= 0)
        {
            ChangeMoveState(PlayerStates.Death);
        }
    }

    public void ParticlesEffect(int i)
    {
        Instantiate(starEffect[i], this.transform.position, Quaternion.identity, this.transform);
    }

    public void ChangeMoveState(PlayerStates currentState)
    {
        _moveState = currentState;
        switch (_moveState)
        {
            case PlayerStates.Run:
                break;
            case PlayerStates.Death:
                GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.GameOver);
                break;
        }
    }
}
