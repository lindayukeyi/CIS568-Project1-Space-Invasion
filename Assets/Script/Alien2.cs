using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien2 : MonoBehaviour
{
    float PointsPerAlien1 = 20.0f;

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
            GameObject obj = GameObject.FindWithTag("GlobalObject");
            Global g = obj.GetComponent<Global>();
            g.score += PointsPerAlien1;

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

    public void Die()
    {
        Destroy(gameObject);
    }
}
