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
    Animator animator;

    [SerializeField]
    float speed = 5;
    int life;
    float horizontal;
    float spriteWidth;

    public GameObject[] starEffect;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        capacitéMax = LevelManagerBehaviour.Instance.capaciteCaddie;
        capacité = capacitéMax;
        life = LevelManagerBehaviour.Instance.playerLife;
        spriteWidth = GetComponent<SpriteRenderer>().sprite.rect.width;
        if(GameManagerBehaviour.instance.playerSkin == 1)
        {
            spriteWidth *= 1.6f;
        }
    }

    private void Update()
    {
        if (GameManagerBehaviour.GameState == GameManagerBehaviour.GameStates.InGame)
        {
            Move();
        }
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        Vector3 position = Camera.main.WorldToScreenPoint(this.transform.position);
        position.x += speed * horizontal * Time.deltaTime; 
        position.x = Mathf.Clamp(position.x, spriteWidth * 0.5f, Screen.width - spriteWidth * 0.5f);
        transform.position = Camera.main.ScreenToWorldPoint(position);
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
        if( i != 1)
        {
            Instantiate(starEffect[i], this.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(starEffect[i], this.transform.position, Quaternion.identity, this.transform);
        }
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
