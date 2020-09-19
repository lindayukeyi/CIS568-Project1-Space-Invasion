using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Boss : MonoBehaviour
{
    public GameObject bullet;

    public float life = 20.0f;
    public float hp;
    public float UBValue = 20;
    public float BulletPeriod = 1;
    public float timer;
    public bool isScale;

    // Start is called before the first frame update
    void Start()
    {
        hp = 0;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (life == 0)
        {
            Die();
            GameObject obj = GameObject.FindWithTag("GlobalObject");
            Global g = obj.GetComponent<Global>();
            g.lives += 3;
            g.score += 500.0f;
            return;
        }

        if (gameObject != null && hp >= UBValue)
        {
            hp = 0;
            isScale = false;
            UB();
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
        }
        if (!isScale && hp >= UBValue - 6)
        {
            isScale = true;
            transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }

        if(gameObject != null &&  timer >= BulletPeriod)
        {
            timer = 0.0f;
            Instantiate(bullet, transform.position + new Vector3(0.0f, 0.0f, -4.0f), Quaternion.identity);
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("LaserFromShip"))
        {
            life -= 5.0f;
            hp += 6.0f;
            LaserFromShip ls = collider.GetComponent<LaserFromShip>();
            ls.Die();
        }
    }

    private void UB()
    {
        for (float x = -3.0f; x <= 3.0; x += 2.5f)
        {
            Instantiate(bullet, transform.position + new Vector3(x, 0.0f, -4.0f), Quaternion.identity);
        }
    }
}
