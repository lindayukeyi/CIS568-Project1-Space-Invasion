using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    Global obj;
    Text gameoverText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        obj = g.GetComponent<Global>();
        gameoverText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(obj.isGameover)
        {
            gameoverText.text = "GAME OVER!";
        }
    }
}
