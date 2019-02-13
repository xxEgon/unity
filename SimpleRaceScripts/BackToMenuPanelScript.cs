using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuPanelScript : MonoBehaviour {

	private GameObject mainMenuCanvas, mainMenuPanel;

	void Start () {
		mainMenuCanvas = GameObject.Find("MainMenuCanvas");
		mainMenuPanel = GameObject.Find("MainMenuPanel");
		GetComponent<Button>().onClick.AddListener(BackButtonClick);
	}

	void BackButtonClick () {
		mainMenuCanvas.GetComponent<MainMenuManager>().HideShowPanel(transform.parent.gameObject, mainMenuPanel);
	}

}
