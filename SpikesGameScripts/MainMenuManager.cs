using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
	
	public static MainMenuManager instance;
	private Button playButton, soundButton, musicButton;
	private Sprite soundOn, soundOff, musicOn, musicOff;
	private Text highscoreText;

	// Use this for initialization
	void Start () {
		instance = this;
		playButton = GameObject.Find("PlayButton").GetComponent<Button>();
		playButton.onClick.AddListener(GameStart);
		soundButton = GameObject.Find("SoundButton").GetComponent<Button>();
		soundButton.onClick.AddListener(ToggleSound);
		musicButton = GameObject.Find("MusicButton").GetComponent<Button>();
		musicButton.onClick.AddListener(ToggleMusic);
		soundOn = Resources.Load<Sprite>("Icons/sound_on");
		soundOff = Resources.Load<Sprite>("Icons/sound_off");
		musicOn = Resources.Load<Sprite>("Icons/music_on");
		musicOff = Resources.Load<Sprite>("Icons/music_off");
		highscoreText = GameObject.Find("HighscoreMenu").GetComponent<Text>();
		SideSpikesManager.instance.ToggleSpikesVisibility(false);
		ScoreManager.instance.UpdateHighscore();
		ReadAudioState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GameStart () {
		GetComponent<Fade>().FadeOut();
		SideSpikesManager.instance.ToggleSpikesVisibility(true);
		BallMovement.instance.StartMovement();
	}

	void ToggleSound(){
		if(AudioManager.instance.soundState){
			AudioManager.instance.StopSound();
			PlayerPrefs.SetInt("soundState", 0);
			soundButton.GetComponent<Image>().sprite = soundOff;
		}
		else{
			AudioManager.instance.PlaySound();
			PlayerPrefs.SetInt("soundState", 1);
			soundButton.GetComponent<Image>().sprite = soundOn;
		}
	}
	void ToggleMusic(){
		if(AudioManager.instance.musicState){
			AudioManager.instance.StopMusic();
			PlayerPrefs.SetInt("musicState", 0);
			musicButton.GetComponent<Image>().sprite = musicOff;
		}
		else{
			AudioManager.instance.PlayMusic();
			PlayerPrefs.SetInt("musicState", 1);
			musicButton.GetComponent<Image>().sprite = musicOn;
		}
	}
	public void SetHighscore (int hs) {
		highscoreText.text = "Highscore:" + hs.ToString();
	}
	void ReadAudioState(){
		int state = 1;
		state = PlayerPrefs.GetInt("soundState", state);
		if(state == 0){
			AudioManager.instance.soundState = true;
		}
		else{
			AudioManager.instance.soundState = false;
		}
		state = PlayerPrefs.GetInt("musicState", state);
		if(state == 0){
			AudioManager.instance.musicState = true;
		}
		else{
			AudioManager.instance.musicState = false;
		}
		ToggleSound();
		ToggleMusic();
	}

}
