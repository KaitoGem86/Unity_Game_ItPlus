using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private int direction;
    //[SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject house;
    private SpriteRenderer playerSprite;
    private Animator anim, animHouse;
    private Rigidbody2D rigidbody;
    private float speed;
    private float jumpSpeed = 300;
    private float jumpTime = 0.6f;
    private bool _isClimbable;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        animHouse = house.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        SetPosition();
    }
   private int isMove()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            return -1;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            return 1;
        return 0;
    }

    private void Move()
    {
        rigidbody.position = rigidbody.position + Vector2.right * direction * Time.deltaTime * speed; 
    }

    private void SetDirection()
    {
        int ismove = isMove();
        direction = ismove;
        speed = ismove * ismove * 3;
        anim.SetBool("isWalk", ismove != 0);
        if (ismove != 0)
        {
            playerSprite.flipX = direction == -1;
        }
    }



    private int isJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return 1;
        }
        return 0;
    }

    private void Jump()
    {
        if (jumpTime > 0f)
        {
            jumpTime -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("isJump", false);
        }

        if (isJump() == 1)
        {
            Debug.Log("JUMP");
            jumpTime = 0.3f;
            anim.SetBool("isJump", true);
            rigidbody.AddForce(new Vector2(0f, jumpSpeed));
        }
    }

    private void SetPosition()
    {
        Jump();
        Move();
    }

    private void EnterTheHouse()
    {
        Debug.Log("ENTER THE HOUSE");
        anim.SetBool("isClimb", true);
        animHouse.SetBool("openDoor", true);
    }

    private void ExitTheHouse()
    {
        anim.SetBool("isClimb", false);
        animHouse.SetBool("openDoor", false);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("canClimb"))
        {
            _isClimbable = true;
            rigidbody.gravityScale = 0;
            anim.SetBool("isClimb", true);
            Debug.Log(_isClimbable + "<<<<<<<<<<<<<<<<<d");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("house"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnterTheHouse();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("canClimb"))
        {
            _isClimbable = false;
            Debug.Log(_isClimbable + "<<<<<<<<<<<<<<<<<d");
            rigidbody.gravityScale = 1;
            anim.SetBool("isClimb", false);
        }
        if (col.CompareTag("house"))
        {
            ExitTheHouse();
        }
    }

}
