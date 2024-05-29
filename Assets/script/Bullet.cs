using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    Rigidbody2D myrigidBody;
    PlayerMovement playerMovement;
    float xspeed;
    void Start()
    {
        myrigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        xspeed = playerMovement.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myrigidBody.velocity = new Vector2(xspeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
