using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpecial : MonoBehaviour
{
    float MoveUnitPerSecond = 2.0f;
    float PointsPerAlienSpecial = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(MoveUnitPerSecond * Time.deltaTime, 0.0f, 0.0f);

        if (Screen.width <= Camera.main.WorldToScreenPoint(transform.position).x)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Die();
        print("Destroy AlienSpecial");
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
            g.score += PointsPerAlienSpecial;

            LaserFromShip ls =
            collider.gameObject.GetComponent<LaserFromShip>();
            ls.Die();
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
