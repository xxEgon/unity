using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	//public static GameOver instance;
	// Use this for initialization
	GameObject score;
	Button replayButton;
	Text highscoreText;

	void Start () {
		//instance = this;
		replayButton = GameObject.Find("ReplayButton").GetComponent<Button>();
		score = GameObject.Find("GameOverScore");
		highscoreText = GameObject.Find("GameOverHighscore").GetComponent<Text>();

		replayButton.onClick.AddListener(ReplayGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ReplayGame () {
		SceneManager.LoadScene("scene1");
	}

	public void ShowGameOverPanel () {
		GetComponent<Canvas>().enabled = true;
		score.GetComponent<Text>().text = ScoreManager.instance.score.ToString();
		AudioManager.instance.StopMusic();
		GetComponent<Fade>().FadeIn();
	}

	public void ShowHighScoreText () {
		highscoreText.enabled = true;
	}

}
