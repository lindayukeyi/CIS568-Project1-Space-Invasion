using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Boss : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip ubSound;
    public AudioClip bonusSound;

    AudioSource bossSound;

    public float life = 20.0f;
    public float mp;
    public float UBValue = 20;
    public float BulletPeriod = 1;
    public float timer;
    public bool isScale;

    // Start is called before the first frame update
    void Start()
    {
        mp = 0;
        timer = 0.0f;
        bossSound = GetComponent<AudioSource>();
        bossSound.Play();
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

        if (gameObject != null && mp >= UBValue)
        {
            mp = 0;
            isScale = false;
            UB();
            UBValue -= 2;
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
        }
        if (!isScale && mp >= UBValue - 6)
        {
            isScale = true;
            transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }

        if(gameObject != null &&  timer >= BulletPeriod)
        {
            timer = 0.0f;
            Instantiate(bullet,  new Vector3(transform.position.x, 0.0f, transform.position .z - 4.0f), Quaternion.identity);
        }

    }

    public void Die()
    {
        bossSound.Stop();
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("LaserFromShip"))
        {
            life -= 5.0f;
            mp += 6.0f;
            LaserFromShip ls = collider.GetComponent<LaserFromShip>();
            ls.Die();
        }
    }

    private void UB()
    {
        AudioSource.PlayClipAtPoint(ubSound, gameObject.transform.position);
        Global g = GameObject.Find("GlobalObject").GetComponent<Global>();
        float distance = 2.5f - g.level * 0.2f;
        for (float x = -3.0f; x <= 3.0; x += distance)
        {
            Instantiate(bullet,  new Vector3(transform.position.x + x, 0.0f, transform.position .z - 4.0f), Quaternion.identity);
        }
    }
}
