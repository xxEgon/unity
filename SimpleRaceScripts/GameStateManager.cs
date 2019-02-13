using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	public static GameStateManager instance;
	public bool GamePaused;

	// Use this for initialization
	void Start () {
		instance = this;
		GamePaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PauseGame () {
		Time.timeScale = 0f;
		GamePaused = true;
	}

	public void ResumeGame () {
		Time.timeScale = 1f;
		GamePaused = false;
	}
}
