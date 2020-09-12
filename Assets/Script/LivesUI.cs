using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    Global globalObj;
    Text livesText;
    // Use this for initialization
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        livesText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        livesText.text = globalObj.lives.ToString();
    }

}
