using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BonusUI : MonoBehaviour
{
    Global obj;
    Text bonusText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        obj = g.GetComponent<Global>();
        bonusText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.isBossDie)
        {
            print("bonus");
            bonusText.text = "Bonus!\n3 LIVES!\n500 POINTS!";
        }
        else
        {
            bonusText.text = "";
        }
    }
}
