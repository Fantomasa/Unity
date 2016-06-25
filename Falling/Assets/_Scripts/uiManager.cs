using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class uiManager : MonoBehaviour {

	public static uiManager ui;
	public Button[] buttons;
	public Text scoreText;
	public Text HighScore;
	bool gameOver;
	int score;
	int high;

	void Start(){
		score = 0;
		InvokeRepeating ("scoreUpdate", 1f, 0.5f);
		gameOver = false;
		high = PlayerPrefs.GetInt ("HighScore1", 0);

	}
	public void checkHighScore(){
		if (score > high) {
			PlayerPrefs.SetInt("HighScore1",score);
		}

	}
	void scoreUpdate(){
		if (!gameOver) {
			score += 1;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("boll"))
			scoreText.text = "   Score: " + score;
			HighScore.text = "High score: " + high;
		gameEnd ();
	}

	public void Play(){
		Application.LoadLevel ("Game");
	}

	public void Pause(){

		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		} else if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
	}

	void gameEnd(){
		if (!GameObject.FindGameObjectWithTag ("boll")) {
			foreach(Button button in buttons){
				button.gameObject.SetActive(true);
			}
			checkHighScore();
		}
	}

	public void Menu(){
		Application.LoadLevel ("Menu");
	}

	public void Exit(){
		Application.Quit ();
	}
}
