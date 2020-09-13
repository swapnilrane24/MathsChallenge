using UnityEngine;
using System.Collections;
using System.IO; // this is required for input and output of data
using System;
using System.Runtime.Serialization.Formatters.Binary;//this is required to convert data into binary

public enum GameMode
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Mix
}

public class GameManager : MonoBehaviour {

    //we make static so in games only one script is name as this
    public static GameManager singleton;

    //variable of gamedata
    private GameData data;

    //data not to store on device
    public float timeForQuestion;
    public int currentScore;
    public bool isGameOver;

    public GameMode currentMode;
	public int gamesPlayed = 0;


    //data to store on device
	public bool canShowAds;
    public int hiScore;
    public bool isMusicOn;
    public bool isGameStartedFirstTime;

    private AudioSource bgMusic;

    //it is call only once in a scene
    void Awake()
    {
        MakeSingleton();
        InitializeVariables();
    }

    void MakeSingleton()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Application.loadedLevelName == "GamePlay")
        {
            bgMusic.Stop();
        }
    }

    void InitializeVariables()
    {
        Load();

        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {

            isGameStartedFirstTime = false;
            hiScore = 0;
            isMusicOn = true;
			canShowAds = true;

            data = new GameData();


            data.setHiScore(hiScore);
            data.setIsMusicOn(isMusicOn);
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
			data.setCanShowAds (canShowAds);

            Save();

            Load();

        }
        else
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            hiScore = data.getHiScore();
			canShowAds = data.getCanShowAds ();
        }


    }



    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");
            if (data != null)
            {
                data.setHiScore(hiScore);
                data.setIsMusicOn(isMusicOn);
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
				data.setCanShowAds (canShowAds);
                bf.Serialize(file, data);

            }
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }



    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e)
        { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }


}

[Serializable]
class GameData
{
    private int hiScore;
    private bool isMusicOn;
    private bool isGameStartedFirstTime;
	private bool canShowAds;



    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }


    //HiScore
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return hiScore;
    }

    //Music
    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getIsMusicOn()
    {
        return isMusicOn;
    }

	public void setCanShowAds(bool canShowAds)
	{
		this.canShowAds = canShowAds;
	}

	public bool getCanShowAds()
	{
		return canShowAds;
	}

}
