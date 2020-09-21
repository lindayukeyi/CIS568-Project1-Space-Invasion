using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    private GUIStyle buttonStyle;
    // Use this for initialization
    void Start()
    {
        GameObject start = GameObject.Find("StartButton");
        GameObject exit = GameObject.Find("ExitButton");

        Button startButton = start.GetComponent<Button>();
        Button ExitButton = exit.GetComponent<Button>();

        startButton.onClick.AddListener(StartOnClick);
        ExitButton.onClick.AddListener(ExitOnClick);

    }
    // Update is called once per frame
    void Update()
    {
    }

    void StartOnClick()
    {
        SceneManager.LoadScene(1);
    }

    void ExitOnClick()
    {
        Application.Quit();
    }
}

