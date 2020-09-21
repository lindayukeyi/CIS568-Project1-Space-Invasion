using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject board;

    [SerializeField]
    GameObject LaserFromAlien;

    [SerializeField]
    GameObject gameover;

    [SerializeField]
    GameObject bunker;

    public GameObject boss;

    private int InitalLives = 3; // # of lives at the beginning
    public float TimeToSpawnSpecialAlien = 5;
    public float TimeToSpawnLaserFromAlien = 1;


    public int level; // Denote the level of each scene
    public int lives; // Denote the remaing lives of the player
    public float score; // Denote the points of the player get
    public bool isGameover = false;
    public bool isBossDie = false;

    Vector3 worldPos;
    public float timerOfSpercialAlien;
    public float timerOfLaserFromAlien;
    public Vector3 originInScreenCoords;
    public Vector3 shipPos;
    bool isBeatBoss;
    float displayTime;


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
        shipPos = worldPos;
        displayTime = 0.0f;
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether game over
        if(isGameover || lives <= 0.0f)
        {
            print("Game Over!!!!");
            GameOver();
            isGameover = true;
            StartCoroutine(GameOverCoroutine());
            return;
        }

        timerOfSpercialAlien += Time.deltaTime;
        timerOfLaserFromAlien += Time.deltaTime;

        // Randomly laucn a special red alien
        if (!isBeatBoss && timerOfSpercialAlien >= TimeToSpawnSpecialAlien)
        {
            timerOfSpercialAlien = 0;
            if(GameObject.FindWithTag("AlienSpecial") == null)
            {
                Instantiate(alienSpecial, worldPos + new Vector3(-10.0f, 0.0f, 6.0f), Quaternion.identity);
            }

        }

        // Randomly launch lasers
        if(!isBeatBoss && timerOfLaserFromAlien >= TimeToSpawnLaserFromAlien)
        {
            timerOfLaserFromAlien = 0;
            float x = Random.Range(-7.5f, 7.5f);
            GameObject obj = GameObject.FindWithTag("Alien1");
            float z = obj.transform.position.z;
            Vector3 pos = new Vector3(x, 0.0f, z - 2.0f) + worldPos;
            Instantiate(LaserFromAlien, pos, Quaternion.identity);
        }

        // If ship is destroyed
        if(GameObject.FindWithTag("Ship") == null)
        {
            CreateShip();
        }

        // Check whether to restart game
        if (!isBeatBoss && GameObject.FindWithTag("Alien1") == null && GameObject.FindWithTag("Alien2") == null
&& GameObject.FindWithTag("Alien3") == null && GameObject.FindWithTag("AlienSpecial") == null)
        {
            isBeatBoss = true;
            CreateBoss();
        }

        if(isBeatBoss && GameObject.FindWithTag("Boss") == null)
        {
            isBossDie = true;
            print("Boss die");
            lives++; // Every new level will be assigned one more life
            level++; // More difficult when level up
            GameObject obj = GameObject.FindWithTag("Ship");
            Ship s = obj.GetComponent<Ship>();
            s.Die();
            RestartGame();
        }

        if(isBossDie)
        {
            displayTime += Time.deltaTime;
            if(displayTime >= 5)
            {
                displayTime = 0.0f;
                isBossDie = false;
            }
        }

    }


    // Restart the game
    private void RestartGame()
    {
        isBeatBoss = false;
        //isBossDie = false;
        CreateShip();
        CreateAliens();
       // CreateBunkers();
    }

    private void CreateShip()
    {
        // Create a new ship
        shipPos = worldPos + new Vector3(0.0f, 0.0f, -6.0f);
        Instantiate(ship, shipPos, Quaternion.identity);
        Instantiate(board, shipPos + new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
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
        for(float x = -6.0f; x <= 6.0f; x += 4.0f)
        {
            Instantiate(bunker, worldPos + new Vector3(x, 0.0f, -3.0f), Quaternion.identity);
            Instantiate(bunker, worldPos + new Vector3(x - 0.5f, 0.0f, -4.0f), Quaternion.identity);
            Instantiate(bunker, worldPos + new Vector3(x + 1.0f, 0.0f, -4.0f), Quaternion.identity);
        }
    }

    IEnumerator GameOverCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene(0);

    }

    IEnumerator BonusCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    // Creat the boss
    private void CreateBoss()
    {
        Instantiate(boss, new Vector3(0.0f, -2.0f, 6.0f), Quaternion.identity);
    }

}
