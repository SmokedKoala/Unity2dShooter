using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControll : MonoBehaviour
{
    public int counter = 0;
    public GameObject current_window;
    public void StartScene()
    {
        Application.LoadLevel("Scene2");
    }

    public void MenuScene()
    {
        Application.LoadLevel("Menu");
    }

    public void Options(GameObject window)
    {
        window.SetActive(true);
        current_window.SetActive(false);
    }
    public void Auth(GameObject window)
    {
        if (counter == 0)
        {
            counter++;
        } else
        {
            window.SetActive(true);
            current_window.SetActive(false);
        }

    }
    public void Quit() 
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
