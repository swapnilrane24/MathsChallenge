using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;

    /// <summary>
    /// This script manages the Ads , after every game over skippable video ads is pop.
    /// This ads work on unity version 5.3 and above , if you are using below 5.3 you need to do few changes.
    /// When implementing ads just remove the "//" from the codes 
    /// </summary>


public class UnityAdsManager : MonoBehaviour {

    private int i = 0;

    // Use this for initialization
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update ()
    {

        if (GameManager.singleton.isGameOver == true)
        {
            /*
            if (i == 0)
                {
                    ShowAd();
                    i++;
                }
            */
        }

    }

    /*
    public void ShowAd()
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    */
}
