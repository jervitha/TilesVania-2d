using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    [SerializeField] private float fast=25f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Animator anim;
    BoxCollider2D boxCollider2D;
    CircleCollider2D circleCollider2D;
    bool isAlive = true;
  
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody.gravityScale = 8f;
        circleCollider2D = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        OnClimbLadder();
        Die();
    }

   void  OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x *fast, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool isRuuning = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (isRuuning)
        {
            anim.SetBool("isRunning", true);
        }
    }
    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void OnJump(InputValue value)
    {
        if(!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            myRigidbody.velocity = new Vector2(0f, fast);
        }
    }

    void OnClimbLadder()
    {
        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody.gravityScale = 8f;
            anim.SetBool("isclimbing", false);
            return;
           
        }
        Vector2 ClimbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = ClimbVelocity;
        bool isClimbing = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        if(isClimbing)
        {
            anim.SetBool("isclimbing", isClimbing);
            myRigidbody.gravityScale = 0f;
        }
    }

    void Die()
    {
        if(boxCollider2D.IsTouchingLayers(LayerMask.GetMask("enemy")))
        {
            isAlive = false;
            anim.SetTrigger("Dying");
        }
    }

    void OnFire(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);
    }

}