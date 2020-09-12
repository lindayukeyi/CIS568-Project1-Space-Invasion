﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromShip : MonoBehaviour
{
    float MoveUnitPerSecond = 1.0f;
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
        transform.position += new Vector3(0.0f, 0.0f, MoveUnitPerSecond * Time.deltaTime);
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Die();
    }

}
