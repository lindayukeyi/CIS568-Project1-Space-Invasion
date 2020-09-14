using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject laser;

    public AudioClip shoot;

    float MoveUnitPerSecond = 3.0f;
    Vector3 AlienStartPos = new Vector3(-2.0f, 0.0f, 2f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            
            transform.position += new Vector3(MoveUnitPerSecond * Time.deltaTime, 0.0f, 0.0f);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position -= new Vector3(MoveUnitPerSecond * Time.deltaTime, 0.0f, 0.0f);
        }


        if(GameObject.FindWithTag("LaserFromShip") == null && Input.GetAxisRaw("shoot") > 0)
        {
            AudioSource.PlayClipAtPoint(shoot, gameObject.transform.position);
            Instantiate(laser, transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
