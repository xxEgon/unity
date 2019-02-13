using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public bool musicState, soundState;
	private float musicVolume, soundVolume;
	private GameObject backgroundMusic, tapSound, gameOverSound, wallSound;
	// Use this for initialization
	void Start () {
		instance = this;
		musicState = true;
		soundState = true;
		musicVolume = 0.1f;
		soundVolume = 0.3f;
		backgroundMusic = GameObject.Find("BackgroundMusic");
		tapSound = GameObject.Find("TapSound");
		gameOverSound = GameObject.Find("GameOverSound");
		wallSound = GameObject.Find("WallSound");
		PlayMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayMusic(){
		musicState = true;
		backgroundMusic.GetComponent<AudioSource>().volume = musicVolume;
		backgroundMusic.GetComponent<AudioSource>().loop = true;
		backgroundMusic.GetComponent<AudioSource>().Play();
	}
	public void StopMusic(){
		musicState = false;
		backgroundMusic.GetComponent<AudioSource>().Stop();
	}
	public void PlaySound(){
		soundVolume = 0.3f;
		soundState = true;
	}
	public void StopSound(){
		soundVolume = 0f;
		soundState = false;
	}

	public void PlayTapSound(){
		tapSound.GetComponent<AudioSource>().volume = soundVolume;
		tapSound.GetComponent<AudioSource>().Play();
	}
	public void PlayWallSound(){
		wallSound.GetComponent<AudioSource>().volume = soundVolume;
		wallSound.GetComponent<AudioSource>().Play();
	}
	public void PlayGameOverSound(){
		gameOverSound.GetComponent<AudioSource>().volume = soundVolume;
		gameOverSound.GetComponent<AudioSource>().Play();
	}
 }
