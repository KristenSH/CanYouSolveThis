using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int currentScore;
    public static int highScore;

    public static int currentLevel;
    public static int unlockedLevel;

    public static void CompleteLevel()
    {
        if (currentLevel < 2)
        {
            currentLevel++;

            SceneManager.LoadScene(currentLevel);
        } 
        
        else
        {
            print("Game is completed!");
        }

    }
}
