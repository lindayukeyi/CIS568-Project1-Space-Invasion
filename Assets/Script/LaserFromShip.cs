using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromShip : MonoBehaviour
{
    float MoveUnitPerSecond = 10.0f;
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
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Die();
    }


}
