using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Alien2 : MonoBehaviour
{
    float PointsPerAlien1 = 20.0f;
    float MoveVerticallySpeed = 1.0f;
    float MoveHorizontallySpeed = 1.0f;
    float MoveVerticallyPeriod = 4.0f;
    float MoveHorizontalPeriod = 1.0f;
    float timerOfVertical;
    float timerOfHorizon;

    float hp = 2;

    public GameObject alien1;
    public GameObject aliendie;
    Global g;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        g = obj.GetComponent<Global>();
        MoveHorizontallySpeed += (float)g.level * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check
        if(hp == 0)
        {
            Die();
        }

        // check whether enter the bottom
        if (-5.0f > transform.position.z)
        {
            print("bottom");
            g.isGameover = true;
        }

        timerOfVertical += Time.deltaTime;
        timerOfHorizon += Time.deltaTime;

        if (timerOfVertical > MoveVerticallyPeriod)
        {
            timerOfVertical = 0;
            //transform.position += new Vector3(0.0f, 0.0f, -MoveVerticallySpeed);
            //MoveVerticallySpeed += 0.5f;
            //MoveVerticallyPeriod -= 1.0f;
            MoveHorizontallySpeed *= -1.0f;
        }
        
        if (timerOfHorizon > MoveHorizontalPeriod)
        {
            timerOfHorizon = 0;
            transform.position += new Vector3(MoveHorizontallySpeed, 0.0f, 0.0f);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // the Collision contains a lot of info, but it’s the colliding
        // object we’re most interested in.
        Collider collider = collision.collider;
        if (collider.CompareTag("LaserFromShip"))
        {
            g.score += PointsPerAlien1;

            LaserFromShip ls =
            collider.gameObject.GetComponent<LaserFromShip>();
            // let the other object handle its own death throes
            ls.Die();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0.0F, 0.0f, 10.0f);
            hp--;
            // Destroy the Bullet which collided with the Asteroid
            /*
            Rigidbody rigidbodyOld = gameObject.GetComponent<Rigidbody>();
            Vector3 vel = rigidbodyOld.velocity;
            Vector3 pos = transform.position;
            Destroy(gameObject);
            GameObject obj = Instantiate(aliendie, pos, Quaternion.identity);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0.0F, 0.0f, 10.0f);
            */
        }
        else if (collider.CompareTag("Alien1"))
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rigidbody.AddRelativeTorque(transform.up * -20);
        }
        else if(collider.CompareTag("Alien3"))
        {
            Vector3 pos = transform.position;
            Destroy(gameObject);
            GameObject obj = Instantiate(aliendie, pos, Quaternion.identity);
            Debug.Log("Collide with alien1");
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }
}
