using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject laser;
    [SerializeField]
    GameObject alien1;

    float MoveUnitPerSecond = 1.0f;
    Vector3 AlienStartPos = new Vector3(-2.0f, 0.0f, 2f);

    // Start is called before the first frame update
    void Start()
    {
        for(float i = 0.0f; i < 5.0f; i += 0.6f)
        {
            Instantiate(alien1, AlienStartPos + new Vector3((float)i, 0.0f, 0.0f), Quaternion.identity);
        }
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
            Instantiate(laser, transform.position + new Vector3(0.0f, 0.0f, 0.5f), Quaternion.identity);
        }
    }
}
