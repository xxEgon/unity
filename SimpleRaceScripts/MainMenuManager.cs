using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	private Button playButton, settingsButton, checkpointsModeButton;
	private GameObject chosenPanel, chosenPanel2, mainMenuPanel, playMenuPanel, settingsMenuPanel;
	private bool hideChosenPanel = false, showChosenPanel = false, animationFinished = false;
	private float translationSpeed, panelWidth;

	// Use this for initialization
	void Start () {
		mainMenuPanel = transform.Find("MainMenuPanel").gameObject;
		playMenuPanel = transform.Find("PlayMenuPanel").gameObject;
		settingsMenuPanel = transform.Find("SettingsMenuPanel").gameObject;
		
		panelWidth = mainMenuPanel.transform.GetComponent<RectTransform>().rect.width;
		
		playMenuPanel.transform.Translate(-panelWidth, 0, 0);
		settingsMenuPanel.transform.Translate(-panelWidth, 0, 0);

		translationSpeed = panelWidth/10;

		playButton = GameObject.Find("PlayButton").GetComponent<Button>();
		playButton.onClick.AddListener(PlayButtonClick);

		settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
		settingsButton.onClick.AddListener(SettingsButtonClick);

		checkpointsModeButton = GameObject.Find("CheckpointsModeButton").GetComponent<Button>();
		checkpointsModeButton.onClick.AddListener(CheckpointsModeButtonClick);
	}

	void PlayButtonClick () {
		HideShowPanel(mainMenuPanel, playMenuPanel);
	}

	void SettingsButtonClick () {
		HideShowPanel(mainMenuPanel, settingsMenuPanel);
	}

	void CheckpointsModeButtonClick () {
		GameObject.Find("LevelLoadManager").GetComponent<LevelLoader>().LoadLevel("scene2");
	}

	public void HideShowPanel (GameObject panelToHide, GameObject panelToShow) {
		chosenPanel2 = panelToShow;
		HidePanel(panelToHide);
	}

	void ShowPanel (GameObject panel) {
		animationFinished = false;
		chosenPanel = panel;
		showChosenPanel = true;
	}

	void HidePanel (GameObject panel) {
		animationFinished = false;
		chosenPanel = panel;
		hideChosenPanel = true;
	}

	void FixedUpdate () {
		if(hideChosenPanel){
			if(chosenPanel.transform.position.x > -panelWidth/2)
				chosenPanel.transform.Translate(-translationSpeed, 0, 0);
			else {
				hideChosenPanel = false;
				ShowPanel(chosenPanel2);
			}			
		}
		if(showChosenPanel){
			if(chosenPanel.transform.position.x < panelWidth/2 - 1)
				chosenPanel.transform.Translate(translationSpeed, 0, 0);
			else
				showChosenPanel = false;
		}
	}

}
