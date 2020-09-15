using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // the Collision contains a lot of info, but it’s the colliding
        // object we’re most interested in.
        Collider collider = collision.collider;
        if (collider.CompareTag("LaserFromShip"))
        {

            LaserFromShip ls =
            collider.gameObject.GetComponent<LaserFromShip>();
            // let the other object handle its own death throes
            ls.Die();
            // Destroy the Bullet which collided with the Asteroid
            Destroy(gameObject);
        }
        if(collider.CompareTag("LaserFromAlien"))
        {
            LaserFromAlien ls = collider.gameObject.GetComponent<LaserFromAlien>();
            ls.Die();
            Destroy(gameObject);
        }
        if(collider.CompareTag("Alien1"))
        {
            Alien1 a = collider.gameObject.GetComponent<Alien1>();
            a.Die();
            Destroy(gameObject);
        }
        if (collider.CompareTag("Alien2"))
        {
            Alien2 a = collider.gameObject.GetComponent<Alien2>();
            a.Die();
            Destroy(gameObject);
        }
        if (collider.CompareTag("Alien3"))
        {
            Alien3 a = collider.gameObject.GetComponent<Alien3>();
            a.Die();
            Destroy(gameObject);
        }
    }
}
