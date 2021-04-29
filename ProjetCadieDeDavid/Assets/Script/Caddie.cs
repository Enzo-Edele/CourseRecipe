using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caddie : MonoBehaviour
{
    public enum moveStates
    {
        Run,
        Jump,
        Death,
    }

    private static moveStates _MoveStates;
    public static moveStates MoveStates;

    float boxVar = 0.075f;
    float offsetVar = 0.04f;

    int capacité;
    int capacitéMax = 20;

    BoxCollider2D bc2d;
    Rigidbody2D rb2D;

    float spriteHeight;
    [SerializeField]
    float speed = 20;
    [SerializeField]
    float jumpPower = 20;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        Vector2 box = bc2d.size;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 position = Camera.main.WorldToScreenPoint(this.transform.position);
        if(colliderFix)
        {
            vertical = -15;
            colliderFix = false;
        }
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, box.x, Screen.width - box.x);
        position.y = Mathf.Clamp(position.y, spriteHeight * 0.5f, Screen.height - spriteHeight * 0.5f);
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }


    void Jump()
    {
        ChangeMoveState(moveStates.Jump);
        Vector2 jump = new Vector2(0, jumpPower);
        rb2D.velocity = jump;
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


    public void ChangeMoveState(moveStates currentState)
    {
        _MoveStates = currentState;
        switch (_MoveStates)
        {
            case moveStates.Run:
                rb2D.gravityScale = 0;
                EtaleBehaviour etale = etaleGO.GetComponent<EtaleBehaviour>();
                etale.ChangeEffector2D(180f);
                etale = etaleGO1.GetComponent<EtaleBehaviour>();
                etale.ChangeEffector2D(180f);
                colliderFix = true;
                break;
            case moveStates.Jump:
                rb2D.gravityScale = 1;
                EtaleBehaviour etaleFix = etaleGO.GetComponent<EtaleBehaviour>();
                etaleFix.ChangeEffector2D(0f);
                etaleFix = etaleGO1.GetComponent<EtaleBehaviour>();
                etaleFix.ChangeEffector2D(0f);
                break;
            case moveStates.Death:
                break;
        }
    }

}
