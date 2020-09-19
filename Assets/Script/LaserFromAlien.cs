using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromAlien : MonoBehaviour
{
    float MoveUnitPerSecond = -10.0f;
    public GameObject laserDie;
    public GameObject explosionPrefab;

    public AudioClip explosion;

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
            AudioSource.PlayClipAtPoint(explosion, gameObject.transform.position);

            Destroy(gameObject);

            Ship s = collider.GetComponent<Ship>();
            Vector3 pos = collider.transform.position;
            s.Die();
            Instantiate(explosionPrefab, pos, Quaternion.identity);
        }
        else if(collider.CompareTag("Board"))
        {
            // if we collided with something else, print to console
            // what the other thing was
            Vector3 pos = transform.position;
            Destroy(gameObject);
            Instantiate(laserDie, pos, Quaternion.identity);
        }
    }


}
