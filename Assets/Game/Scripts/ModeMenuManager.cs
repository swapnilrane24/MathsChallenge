using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script manages the mode scene menu
/// </summary>

public class ModeMenuManager : MonoBehaviour {

    [SerializeField] private Text addScoreText, subtractionScoreText, multiplicationScoreText, divisionScoreText, mixScoreText;


    private AudioSource clickSound;

    void Start()
    {
        clickSound = GetComponent<AudioSource>();

        addScoreText.text = PlayerPrefs.GetInt(GameMode.Addition.ToString(), 0).ToString();
        subtractionScoreText.text = PlayerPrefs.GetInt(GameMode.Subtraction.ToString(), 0).ToString();
        multiplicationScoreText.text = PlayerPrefs.GetInt(GameMode.Multiplication.ToString(), 0).ToString();
        divisionScoreText.text = PlayerPrefs.GetInt(GameMode.Division.ToString(), 0).ToString();
        mixScoreText.text = PlayerPrefs.GetInt(GameMode.Mix.ToString(), 0).ToString();
    }

    //method to be called when we press addition button
    public void AdditionMode()
    {
        GameManager.singleton.currentMode = GameMode.Addition;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");

        clickSound.Play();
    }

    public void SubtractionMode()
    {
        GameManager.singleton.currentMode = GameMode.Subtraction;
       // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void MultiplicationMode()
    {
        GameManager.singleton.currentMode = GameMode.Multiplication;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void DivisionMode()
    {
        GameManager.singleton.currentMode = GameMode.Division;
        // Application.LoadLevel("GamePlay"); // use this for unity below 5.3 version
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void MixMode()
    {
        GameManager.singleton.currentMode = GameMode.Mix;
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
