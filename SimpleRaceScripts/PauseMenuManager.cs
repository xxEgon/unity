using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
		resumeButton.onClick.AddListener(ResumeGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResumeGame () {
		HidePauseMenu();
		GameStateManager.instance.ResumeGame();
	}

	public void ShowPauseMenu () {
		GetComponent<Canvas>().enabled = true;
	}

	public void HidePauseMenu () {
		GetComponent<Canvas>().enabled = false;
	}
}
