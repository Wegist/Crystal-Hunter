using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
     public GameObject menu;
     public GameObject settingsmenu;
     public void Play()
    {
        SceneManager.LoadScene(1);
    }

    //public void Settings()
    //{
       // menu.SetActive(false);
       // settingsmenu.SetActive(true);
    //}

    public void Exit()
    {
        Application.Quit();
    }
    public void Main()
    {
        SceneManager.LoadScene(0);
    }
}
