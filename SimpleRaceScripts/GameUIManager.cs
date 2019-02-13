using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class GameUIManager:MonoBehaviour {
	public static GameUIManager instance; 
	private PauseMenuManager pauseMenuManager; 
	private Image slipperyRoadImage; 

	private Text wrongWayText; 

	// Use this for initialization
	void Start () {
		instance = this; 
		pauseMenuManager = GameObject.Find("PauseMenuCanvas").GetComponent < PauseMenuManager > (); 
		Button pauseButton = GameObject.Find("PauseButton").GetComponent < Button > (); 
		slipperyRoadImage = GameObject.Find("SlipperyRoadImage").GetComponent < Image > (); 
		wrongWayText = GameObject.Find("WrongWayText").GetComponent < Text > (); 
		pauseButton.onClick.AddListener(PauseGame); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PauseGame () {
		pauseMenuManager.ShowPauseMenu(); 
		GameStateManager.instance.PauseGame(); 
	}
	public void ShowSlipperyRoadImage () {
		slipperyRoadImage.enabled = true; 
	}
	public void HideSlipperyRoadImage () {
		slipperyRoadImage.enabled = false; 
	}
	public void ShowWrongWayText () {
		wrongWayText.enabled = true; 
	}
	public void HideWrongWayText () {
		wrongWayText.enabled = false; 
	}
}
