using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTopTeleportScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colliderf)
    {
       // var collisionPoint = .clo(transform.position);
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spaceship"))
        {
            var ship = collision.gameObject;
            var interVelocity = ship.GetComponent<Rigidbody2D>().velocity;

            ship.GetComponent<Rigidbody2D>().MovePosition(new Vector2(ship.transform.position.x, -ship.transform.position.y));
            ship.GetComponent<Rigidbody2D>().velocity = interVelocity;
        }
    }
    */



}
