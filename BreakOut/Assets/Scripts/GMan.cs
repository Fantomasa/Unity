using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GMan : MonoBehaviour
{

    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public Text livesText;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject bricksPrefab;
    public GameObject paddle;
    public GameObject deathParticles;
    public GameObject unityAds;
    public GameObject startAudio;
    public GameObject restartButton;

    public static GMan instance = null;

    private GameObject clonePaddle;


    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (!instance.Equals(this))
        {
            Destroy(gameObject);
        }
        if (lives > 0)
        {
            Setup();
        }
        Advertisement.Initialize("84296",false);
        
    }

    public void Setup()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        //Instantiate(bricksPrefab, transform.position, Quaternion.identity);
    }

    void CheckGameOver()
    {
        if (bricks < 1)
        {
            youWon.SetActive(true);
            Time.timeScale = 0.25f;
            Invoke("CheckLevel", resetDelay);
            startAudio.SetActive(false);
        }

        if (lives < 1)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0.1f;
            startAudio.SetActive(false);
            //Invoke("Reset", resetDelay);
        }
    }

    void CheckLevel()
    {
        for (int i = 1; i < 10; i++)
        {
            if (Application.loadedLevelName.Equals("Level_" + i))
            {
                Application.LoadLevel("Level_" + (i + 1));
                Time.timeScale = 1f;
            }
        }
    }

    void Reset()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
            Destroy(clonePaddle);
            Invoke("SetupPaddle", resetDelay);
            CheckGameOver();
        }        
    }

    void SetupPaddle()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        bricks--;
        CheckGameOver();
    }

    public void OnClickPause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void OnClickRestart()
    {
        Application.LoadLevel("Level_1");
    }
	public void OnClickExit ()
    {
        Application.Quit();
	}
}
