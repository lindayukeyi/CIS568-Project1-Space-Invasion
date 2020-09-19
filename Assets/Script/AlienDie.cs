using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDie : MonoBehaviour
{
    public Vector3 forceDirection = new Vector3(0.0f, 0.0f, -1.0f);
    public float thrust = 5.0f;
    Rigidbody rb;

    public GameObject laserFromShip;

    // Start is called before the first frame update
    void Start()
    {
        print("Created aliendie");
        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(forceDirection * thrust);
        if(transform.position.x >= 10.0f || transform.position.x  <= -10.0f)
        {
            Destroy(gameObject);
        }
    }

}
