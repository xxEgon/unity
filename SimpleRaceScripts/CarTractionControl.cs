using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class CarTractionControl:MonoBehaviour {

	private WheelCollider[] WheelColliders; 
	private int wheelsOnGrass = 0; 
	private bool isAlreadyShown = false, isAlreadyHidden = false; 
	private bool isAlreadyShown2 = false, isAlreadyHidden2 = false; 

	private float[] distancesFromStart = new float[3]; 

	private int counter = 0; 

	// Use this for initialization
	void Start () {
		WheelColliders = GetComponentsInChildren < WheelCollider > (); 
		InvokeRepeating("GetDistanceFromStart", 2.0f, 2.0f); 
	}

	void Update () {
		HandleWheelsOnGrass(); 
	}
	
	private void HandleWheelsOnGrass () {
		wheelsOnGrass = 0; 
		for (int i = 0; i < WheelColliders.Length; i++) {
			WheelHit hit = new WheelHit(); 
			if (WheelColliders[i].GetGroundHit(out hit) && hit.collider.name == "RoadGrass") {
				wheelsOnGrass++; 
			}
		}
		if (wheelsOnGrass > 0 &&  ! isAlreadyShown) {
			GameUIManager.instance.ShowSlipperyRoadImage(); 
			isAlreadyShown = true; 
			isAlreadyHidden = false; 
		}
		else if (wheelsOnGrass == 0 &&  ! isAlreadyHidden) {
			GameUIManager.instance.HideSlipperyRoadImage(); 
			isAlreadyHidden = true; 
			isAlreadyShown = false; 
		}
		GetComponent < UnityStandardAssets.Vehicles.Car.CarController > ().m_SteerHelper = 1 - (wheelsOnGrass * 0.05f); 
		GetComponent < UnityStandardAssets.Vehicles.Car.CarController > ().m_Topspeed = GetComponent < UnityStandardAssets.Vehicles.Car.CarController > ().m_TopspeedDefault - (wheelsOnGrass * 0.05f * GetComponent < UnityStandardAssets.Vehicles.Car.CarController > ().m_Topspeed); 
	}

	private void WrongWayDetector () {
		if ( !isAlreadyShown2 && distancesFromStart[0] > distancesFromStart[1] && distancesFromStart[1] > distancesFromStart[2]) {
			//Debug.Log("WRONG WAY");
			GameUIManager.instance.ShowWrongWayText(); 
			isAlreadyShown2 = true; 
			isAlreadyHidden2 = false; 
		}
		else if ( !isAlreadyHidden2 && distancesFromStart[0] < distancesFromStart[1] && distancesFromStart[1] < distancesFromStart[2]) {
			GameUIManager.instance.HideWrongWayText(); 
			isAlreadyHidden2 = true; 
			isAlreadyShown2 = false; 
		}
	}

	private void GetDistanceFromStart () {		//every 2 seconds
		distancesFromStart[counter] = Vector3.Distance(transform.position, Vector3.zero); 
		counter++; 
		if (counter == 3) {
			counter = 0; 
			WrongWayDetector(); 
		}
	}
}
