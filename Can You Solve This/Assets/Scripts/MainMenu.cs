using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startLevel;
   
    public string setting;

    public void NewGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void Setting()
    {
        SceneManager.LoadScene(setting);
    }

    public void QuitGame()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }
}
