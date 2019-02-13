using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class ScoreManager:MonoBehaviour {

	public static ScoreManager instance; 
	public int score, highscore; 
	// Use this for initialization
	void Start () {
		instance = this; 
		score = 0; 
		highscore = PlayerPrefs.GetInt("highscore", highscore); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore () {
		score++; 
		transform.GetComponent < Text > ().text = score.ToString(); 
		SideSpikesManager.instance.AddSpikes(); 
		ColorsManager.instance.ChangeColors(); 
	}

	public void UpdateHighscore () {
		if (score > highscore) {
			highscore = score; 
			PlayerPrefs.SetInt("highscore", highscore); 
			GameObject.Find("GameOverCanvas").GetComponent < GameOver > ().ShowHighScoreText(); 
		}
		MainMenuManager.instance.SetHighscore(highscore); 
	}
}
