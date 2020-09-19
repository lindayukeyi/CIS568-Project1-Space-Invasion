using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject laser;

    public Camera thirdviewCamera;

    public AudioClip shoot;

    float MoveUnitPerSecond = 3.0f;
    Vector3 AlienStartPos = new Vector3(-2.0f, 0.0f, 2f);
    int viewMode;
    float bulletSpeed;
    float BulletSpeedRaw = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = BulletSpeedRaw;
        viewMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switchCamera();
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
            GameObject obj = Instantiate(laser, transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
            LaserFromShip ls =  obj.GetComponent<LaserFromShip>();
            ls.MoveUnitPerSecond = bulletSpeed;
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void switchCamera()
    {
        Camera firstviewCamera = gameObject.GetComponent<Camera>();
        if (viewMode == 1 || Input.GetAxisRaw("firstview") > 0)
        {
            viewMode = 1;
            firstviewCamera.enabled = true;
            thirdviewCamera.enabled = false;
        }
        if(viewMode == 0 || Input.GetAxisRaw("thirdview") > 0)
        {
            viewMode = 0;
            firstviewCamera.enabled = false;
            thirdviewCamera.enabled = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Aliendie"))
        {
            bulletSpeed = 5.0f;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        bulletSpeed = BulletSpeedRaw;
    }
}
