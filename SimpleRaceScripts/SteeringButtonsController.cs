using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SteeringButtonsController: MonoBehaviour {
	// Use this for initialization
	void Start () {
		GameObject.Find("Car").GetComponent<CarUserControl>().v = 0;
		GameObject.Find("Car").GetComponent<CarUserControl>().h = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AccelerationOn(){
		GameObject.Find("Car").GetComponent<CarUserControl>().v = 1;
	}
	public void BrakeOn(){
		GameObject.Find("Car").GetComponent<CarUserControl>().v = -1;
	}
	public void VerticalOff(){
		GameObject.Find("Car").GetComponent<CarUserControl>().v = 0;
	}
	public void LeftOn(){
		GameObject.Find("Car").GetComponent<CarUserControl>().h = -1;
	}
	public void RightOn(){
		GameObject.Find("Car").GetComponent<CarUserControl>().h = 1;
	}
	public void HorizontalOff(){
		GameObject.Find("Car").GetComponent<CarUserControl>().h = 0;
	}
}
