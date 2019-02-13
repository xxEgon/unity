using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarAIManager:MonoBehaviour {

	private CarAIControl carAIControl; 
	private int currentWaypoint;
	private bool aiStarted = false; 
	List < GameObject > waypointsList;
	//GameObject cube;
	
	private void Start () {
		carAIControl = GetComponent<CarAIControl>();
		currentWaypoint = 0;
		// cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		// cube.name = "Cube";
		// cube.transform.localScale = new Vector3(2,2, 2);
		// DestroyImmediate(cube.GetComponent<BoxCollider>());
	}

	private void FixedUpdate() {
		if( aiStarted && Vector3.Distance(transform.position, waypointsList[currentWaypoint].transform.position) < 50f) {
			currentWaypoint++;
			carAIControl.SetTarget(waypointsList[currentWaypoint].transform);
			//cube.transform.position = waypointsList[currentWaypoint].transform.position;
			//Debug.Log("SetNewWaypoint: " + waypointsList[currentWaypoint].transform.position);
		}
	}

	public void InitTarget (List < GameObject > waypoints) {
		waypointsList = waypoints;
		carAIControl.SetTarget(waypointsList[currentWaypoint].transform);
		aiStarted = true;
	}


}
