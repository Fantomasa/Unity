using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    [SerializeField]
    string gameID = "84296";

    void Awake()
    {
        Advertisement.Initialize(gameID, false);
    }

    public void ShowAd()
    {
#if UNITY_EDITOR
        StartCoroutine(WaitForAd());
#endif
        StartCoroutine(WaitForAd());

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.isReady())
        {
            Advertisement.Show("rewardedVideoZone", options);
        }
        else
        {
            GMan.instance.restartButton.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Time.timeScale = 1f;
                GMan.instance.lives = 3;
                GMan.instance.gameOver.SetActive(false);
                GMan.instance.livesText.text = "Lives: 3";
                GMan.instance.startAudio.SetActive(true);
                break;
            case ShowResult.Failed:
                GMan.instance.restartButton.SetActive(true);
                Time.timeScale = 1f;

                break;
        }
    }

    IEnumerator WaitForAd()
    {
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;


    }
}
