using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //private AdManger adManager;
    public Text scoreText;

    void Start()
    {
        scoreText.text = "Higth score:\n" + PlayerPrefs.GetInt("score").ToString();
    }

   public void ToGame()
    {
        SceneManager.LoadScene("Game");
       // adManager.ShowBanner();
    }
}
