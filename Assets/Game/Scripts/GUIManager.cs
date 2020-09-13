using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the game play GUI
/// </summary>

public class GUIManager : MonoBehaviour {

    //ref to the score
    public Text inGameScoreText;

    //ref to score text in game over panel
    public Text scoreOverText;
    //ref to hiscore text in game over panel
    public Text hiScoreOverText;

    //ref to game over panel
    //public GameObject gameOverMenu;
    //ref to game over panel animator
    public Animator gameOverAnim;

    //ref to background music
    private AudioSource bgMusic;

    private bool gameIsOver = false;

	//this is used to show ads after specific amount of game overs like 5
	public int AdsAfterGameOver = 5;

	// Use this for initialization
	void Start ()
    {
		GameManager.singleton.gamesPlayed++;

        //check for the music statis and depending on that we make AudioListener vol 1 or 0
        if (GameManager.singleton.isMusicOn == true)
        {
            AudioListener.volume = 1;
        }
        else if (GameManager.singleton.isMusicOn == false)
        {
            AudioListener.volume = 0;
        }

        //we get the audioSource attached to this object
        bgMusic = GetComponent<AudioSource>();
        GameManager.singleton.currentScore = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (GameManager.singleton.gamesPlayed >= AdsAfterGameOver)
		{
			AdmobAdsManager.instance.ShowInterstitialAd();
			GameManager.singleton.gamesPlayed = 0;
		}

        //we check for the game manager
        if (GameManager.singleton != null)
        {
            //and keep updating score value
            inGameScoreText.text = GameManager.singleton.currentScore.ToString();
        }

        //we check if the game is over 
        if (GameManager.singleton.isGameOver == true)
        {
            if (!gameIsOver)
            {
                gameIsOver = true;
                //if yes the we stop the music and diplay game over panel
                bgMusic.Stop();

                //we update the score text and hi score text
                scoreOverText.text = "" + GameManager.singleton.currentScore;

                //we play the slideIn animation
                gameOverAnim.Play("SlideIn");
                int highScore = 0;
                switch (GameManager.singleton.currentMode)
                {
                    case GameMode.Addition:
                        highScore = PlayerPrefs.GetInt(GameMode.Addition.ToString(), 0);
                        if (GameManager.singleton.currentScore > highScore)
                        {
                            PlayerPrefs.SetInt(GameMode.Addition.ToString(), GameManager.singleton.currentScore);
                        }
                        break;
                    case GameMode.Subtraction:
                        highScore = PlayerPrefs.GetInt(GameMode.Subtraction.ToString(), 0);
                        if (GameManager.singleton.currentScore > highScore)
                        {
                            PlayerPrefs.SetInt(GameMode.Subtraction.ToString(), GameManager.singleton.currentScore);
                        }
                        break;
                    case GameMode.Multiplication:
                        highScore = PlayerPrefs.GetInt(GameMode.Multiplication.ToString(), 0);
                        if (GameManager.singleton.currentScore > highScore)
                        {
                            PlayerPrefs.SetInt(GameMode.Multiplication.ToString(), GameManager.singleton.currentScore);
                        }
                        break;
                    case GameMode.Division:
                        highScore = PlayerPrefs.GetInt(GameMode.Division.ToString(), 0);
                        if (GameManager.singleton.currentScore > highScore)
                        {
                            PlayerPrefs.SetInt(GameMode.Division.ToString(), GameManager.singleton.currentScore);
                        }
                        break;
                    case GameMode.Mix:
                        highScore = PlayerPrefs.GetInt(GameMode.Mix.ToString(), 0);
                        if (GameManager.singleton.currentScore > highScore)
                        {
                            PlayerPrefs.SetInt(GameMode.Mix.ToString(), GameManager.singleton.currentScore);
                        }
                        break;
                }

                hiScoreOverText.text = highScore.ToString();
            }
        }
	}

    //ref method to retry button
    public void RetryButton()
    {
        //Application.LoadLevel("GamePlay"); // if you are using unity below 5.3 version
        //when player press retry button the game play scene is loaded and the game over bool is made false
        SceneManager.LoadScene("GamePlay");//use this for 5.3 version
        //we make it false because  we need to play the game again
        GameManager.singleton.isGameOver = false;
    }

    //this method is used for taking screen shot and sharing it on any social app , mail , etc
    public void ShareButton()
    {
        ShareScreenScript.instance.ButtonShare(); //this is for android change it for iOS
    }

    //ref method for home button
    public void HomeButton()
    {
        //Application.LoadLevel("MainMenu"); // if you are using unity below 5.3 version
        SceneManager.LoadScene("MainMenu");
        GameManager.singleton.isGameOver = false;
    }

    //ref method for back button
    public void BackButton()
    {
        //Application.LoadLevel("ModeSelector"); // if you are using unity below 5.3 version
        SceneManager.LoadScene("ModeSelector");
    }






}
