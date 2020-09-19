using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromShip : MonoBehaviour
{
    public float MoveUnitPerSecond = 10.0f;
    public AudioClip explosion;

    public GameObject aliendie;
    public GameObject bulletdie;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, 0.0f, MoveUnitPerSecond * Time.deltaTime);
        if(Screen.height  <= Camera.main.WorldToScreenPoint(transform.position).z)
        {
            Die();
        }
    }


    public void Die()
    {
        AudioSource.PlayClipAtPoint(explosion, gameObject.transform.position);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("Aliendie")) {
            Vector3 pos = transform.position;
            Destroy(gameObject);
            Instantiate(bulletdie, pos, Quaternion.identity);
        }
    }



}
