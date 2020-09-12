using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{


    [SerializeField]
    GameObject alien1;
    [SerializeField]
    GameObject alien2;
    [SerializeField]
    GameObject alien3;
    [SerializeField]
    GameObject alienSpecial;

    [SerializeField]
    GameObject ship;

    [SerializeField]
    GameObject LaserFromAlien;

    [SerializeField]
    GameObject gameover;

    private int InitalLives = 1; // # of lives at the beginning
    public float TimeToSpawnSpecialAlien = 5;
    public float TimeToSpawnLaserFromAlien = 1;


    public int level; // Denote the level of each scene
    public int lives; // Denote the remaing lives of the player
    public float score; // Denote the points of the player get
    public bool isGameover;

    Vector3 worldPos;
    public float timerOfSpercialAlien;
    public float timerOfLaserFromAlien;
    public Vector3 originInScreenCoords;


    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        lives = InitalLives;
        score = 0;
        isGameover = false;

        print("start");
        originInScreenCoords =Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, 
                                                            (float)Screen.height / 2f, 
                                                            originInScreenCoords.z));
        print(Screen.width);
        print(Screen.height);

        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether game over
        if(lives <= 0)
        {
            print("Game Over!!!!");
            GameOver();
            isGameover = true;
            return;
        }

        timerOfSpercialAlien += Time.deltaTime;
        timerOfLaserFromAlien += Time.deltaTime;

        // Randomly laucn a special red alien
        if (timerOfSpercialAlien >= TimeToSpawnSpecialAlien)
        {
            timerOfSpercialAlien = 0;
            if(GameObject.FindWithTag("AlienSpecial") == null)
            {
                Instantiate(alienSpecial, worldPos + new Vector3(-10.0f, 0.0f, 6.0f), Quaternion.identity);
            }

        }

        // Randomly launch lasers
        if(timerOfLaserFromAlien >= TimeToSpawnLaserFromAlien)
        {
            timerOfLaserFromAlien = 0;
            float x = Random.Range(-7.5f, 7.5f);
            Vector3 pos = new Vector3(x, 0.0f, 0.2f) + worldPos;
            Instantiate(LaserFromAlien, pos, Quaternion.identity);
        }

        // Check whether to restart game
        if (GameObject.FindWithTag("Alien1") == null && GameObject.FindWithTag("Alien2") == null
&& GameObject.FindWithTag("Alien3") == null && GameObject.FindWithTag("AlienSpecial") == null)
        {
            lives++; // Every new level will be assigned one more life
            level++; // More difficult when level up
            GameObject obj = GameObject.FindWithTag("Ship");
            Ship s = obj.GetComponent<Ship>();
            s.Die();
            RestartGame();
        }
    }


    // Restart the game
    private void RestartGame()
    {
        CreateShip();
        CreateAliens();
        CreateBunkers();
    }

    private void CreateShip()
    {
        // Create a new ship
        Instantiate(ship, worldPos + new Vector3(0.0f, 0.0f, -6.0f), Quaternion.identity);
    }

    private void CreateAliens()
    {
        float height = 1.0f;
        for(height = 1.0f; height <= 6.0f; height += 1.5f)
        {
            for(float x = -7.5f; x <= 7.5f; x += 1.5f)
            {
                if(height == 1.0f)
                {
                    Instantiate(alien1, worldPos + new Vector3(x, 0.0f, height), Quaternion.identity);
                }
                else if(height == 2.5f)
                {
                    Instantiate(alien2, worldPos + new Vector3(x, 0.0f, height), Quaternion.identity);

                }
                else if(height == 4.0f)
                {
                    Instantiate(alien3, worldPos + new Vector3(x, 0.0f, height), Quaternion.identity);
                }
            }
        }

        
    }

    private void GameOver()
    {
        GameObject[] alien1s = GameObject.FindGameObjectsWithTag("Alien1");
        GameObject[] alien2s = GameObject.FindGameObjectsWithTag("Alien2");
        GameObject[] alien3s = GameObject.FindGameObjectsWithTag("Alien3");
        GameObject[] alienSpecials = GameObject.FindGameObjectsWithTag("AlienSpecial");

        for(int i = 0; i < alien1s.Length; i++)
        {
            Alien1 a = alien1s[i].GetComponent<Alien1>();
            a.Die();
        }

        for (int i = 0; i < alien2s.Length; i++)
        {
            Alien2 a = alien2s[i].GetComponent<Alien2>();
            a.Die();
        }

        for (int i = 0; i < alien3s.Length; i++)
        {
            Alien3 a = alien3s[i].GetComponent<Alien3>();
            a.Die();
        }

        for (int i = 0; i < alienSpecials.Length; i++)
        {
            AlienSpecial a = alienSpecials[i].GetComponent<AlienSpecial>();
            a.Die();
        }

    }

    private void CreateBunkers()
    {

    }
}
