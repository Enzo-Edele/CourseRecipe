using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caddie : MonoBehaviour
{
    public enum MoveStates
    {
        Run,
        Jump,
        Death,
    }

    private static MoveStates _moveState;
    public static MoveStates _MoveState
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

    float spriteHeight;
    [SerializeField]
    float timerY = 200;
    float timeY = 0;
    [SerializeField]
    int moveY = 2;
    [SerializeField]
    float speed = 500;
    [SerializeField]
    float jumpPower = 20;
    int life;
    float horizontal;
    float vertical;

    public GameObject etaleGO;
    public GameObject etaleGO1;
    bool colliderFix = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        spriteHeight = GetComponent<SpriteRenderer>().sprite.rect.height;
        capacité = capacitéMax;
        life = LevelManagerBehaviour.instance.playerLife;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(vertical != 0)
        {
            timeY -= Time.deltaTime;
            Debug.Log(timeY);
        }
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && LevelManagerBehaviour.LevelState == LevelManagerBehaviour.LevelStates.Run)
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector2 box = bc2d.size;
        Vector3 position = this.transform.position;
        if (colliderFix)
        {
            vertical = -15;
            colliderFix = false;
        }
        if(position.x + speed * horizontal * Time.deltaTime >= -8 && position.x + speed * horizontal * Time.deltaTime <= 8)
        {
            position.x += speed * horizontal * Time.deltaTime;
        }
        if (timeY <0)
        {
            if(position.y + moveY * vertical* Time.deltaTime <= -1 && position.y + moveY * vertical * Time.deltaTime >= -4)
            {
                position.y += moveY * vertical * Time.deltaTime;
                timeY = timerY;
            }
        }
        transform.position = position;
    }
    void Jump()
    {
        ChangeMoveState(MoveStates.Jump);
        Vector2 jump = new Vector2(0, jumpPower);
        rb2D.velocity = jump;
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
                Articles recup = collision.GetComponent<Articles>();
                collision.gameObject.transform.SetParent(this.transform);
                Destroy(collision.attachedRigidbody);
                recup.speed = 0;
                Vector2 box = bc2d.size;
                box.y += boxVar;
                bc2d.size = box;
                Vector2 offset = bc2d.offset;
                offset.y += offsetVar;
                bc2d.offset = offset;
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
            Destroy(this.gameObject);
        }
    }
    public void ChangeMoveState(MoveStates currentState)
    {
        _moveState = currentState;
        switch (_moveState)
        {
            case MoveStates.Run:
                rb2D.gravityScale = 0;
                EtaleBehaviour etale = etaleGO.GetComponent<EtaleBehaviour>();
                etale.ChangeEffector2D(180f);
                etale = etaleGO1.GetComponent<EtaleBehaviour>();
                etale.ChangeEffector2D(180f);
                colliderFix = true;
                break;
            case MoveStates.Jump:
                rb2D.gravityScale = 1;
                EtaleBehaviour etaleFix = etaleGO.GetComponent<EtaleBehaviour>();
                etaleFix.ChangeEffector2D(0f);
                etaleFix = etaleGO1.GetComponent<EtaleBehaviour>();
                etaleFix.ChangeEffector2D(0f);
                break;
            case MoveStates.Death:
                break;
        }
    }
}
