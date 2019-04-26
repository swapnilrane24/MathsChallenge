using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the mode scene menu
/// </summary>

public class ModeMenuManager : MonoBehaviour {


    private AudioSource clickSound;

    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    //method to be called when we press addition button
    public void AdditionMode()
    {

        GameManager.singleton.currentMode = 1;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");

        clickSound.Play();

    }

    public void SubtractionMode()
    {
        GameManager.singleton.currentMode = 2;
       // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void MultiplicationMode()
    {
        GameManager.singleton.currentMode = 3;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void DivisionMode()
    {
        GameManager.singleton.currentMode = 4;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void MixMode()
    {
        GameManager.singleton.currentMode = 5;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        clickSound.Play();
    }
}
