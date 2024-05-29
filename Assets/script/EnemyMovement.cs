using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed=1f;
    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rigidbody.velocity = new Vector2(movementSpeed, 0f);
    }
    private void OncollisionExit2D(Collider2D collision)
    {
        movementSpeed = -movementSpeed;
        FlipEnemyFacing();
    }
    void FlipEnemyFacing()
    {
       
    
       transform.localScale = new Vector2(-(Mathf.Sign(rigidbody.velocity.x)), 1f);
        
    }
}
