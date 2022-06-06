using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{

    public void SinglePlayer()
    {
        print("singleplayer");
        SceneManager.LoadScene(1);
    }
    public void Multiplayer()
    {
        print("multiplayer");

    }
 
    public void QuitApp()
    {
        print("quit");
        Application.Quit();
    }
}
