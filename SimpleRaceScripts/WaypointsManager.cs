using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class WaypointsManager:MonoBehaviour {

	public List < GameObject > waypointsList = new List < GameObject > (); 

	void Start () {

	}
	
	void Update () {
		
	}

	public void CreateWaypoints ( Vector2[] points) {
		for (int i = 0; i < points.Length; i += 100) {
			if(points[i].x > 250 && points[i].y > 250) {
				GameObject waypoint = new GameObject(); 
				waypoint.transform.parent = this.transform; 
				waypoint.transform.position = new Vector3(points[i].x, 1, points[i].y); 
				waypoint.name = "Waypoint"+i;
				waypointsList.Add(waypoint);
			}
			
		}
		GameObject.Find("CarAI").GetComponent<CarAIManager>().InitTarget(waypointsList);
	}
}
