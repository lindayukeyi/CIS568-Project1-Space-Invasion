using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromAlien : MonoBehaviour
{
    float MoveUnitPerSecond = -10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, 0.0f, MoveUnitPerSecond * Time.deltaTime);
        if (Screen.height <= Camera.main.WorldToScreenPoint(transform.position).z)
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
    }

    void OnCollisionEnter(Collision collision)
    {
        // the Collision contains a lot of info, but it’s the colliding
        // object we’re most interested in.
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship"))
        {
            GameObject obj = GameObject.FindWithTag("GlobalObject");
            Global g = obj.GetComponent<Global>();
            g.lives--;

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
