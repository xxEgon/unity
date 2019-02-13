using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
using UnityStandardAssets.Vehicles.Car; 

public class StatsManager:MonoBehaviour {

	private Text speedText, timeText; 
	private float timeLeft = 10; 
	private bool isCountingDown = true; 

	void Start () {
		speedText = transform.Find("CurrentSpeedText").GetComponent < Text > (); 
		timeText = transform.Find("TimeText").GetComponent < Text > (); 
	}
	
	void LateUpdate () {
		speedText.text = "Speed: " + GameObject.Find("Car").GetComponent < CarController > ().CurrentSpeedKPH + " kph"; 	

		if (isCountingDown) {
			timeLeft -= Time.deltaTime; 

			if (timeLeft <= 5) {
				timeText.color = Color.red; 
			}

			timeText.text = "Time: " + Mathf.Round(timeLeft).ToString() + " s"; 
			
			if (timeLeft < 0) {
				Debug.Log("GAME OVER"); 
				isCountingDown = false; 
			}
		}
		
	}
}
