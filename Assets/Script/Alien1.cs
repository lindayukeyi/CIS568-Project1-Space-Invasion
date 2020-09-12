using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien1 : MonoBehaviour
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
        else
        {
            // if we collided with something else, print to console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }
    }
