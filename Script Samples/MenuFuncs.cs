using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFuncs : MonoBehaviour
{
    public static Action pause; //pauses the game

    public void play()
    {
        SceneManager.LoadScene("GameOn", LoadSceneMode.Single);
    }

    public void instructions()
    {
        pause?.Invoke();
        SceneManager.LoadScene("Instructions", LoadSceneMode.Additive);
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void menu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void exitInstructions()
    {
        pause?.Invoke();
        SceneManager.UnloadSceneAsync("Instructions");
    }

    public void exitCredits()
    {
        SceneManager.UnloadSceneAsync("Credits");
    }
}
