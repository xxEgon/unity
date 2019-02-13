using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class RoadManager:MonoBehaviour {

	public static RoadManager instance; 
	Vector3 lastRoadPosition = Vector3.zero; 
    Vector3 newRoadPosition, lastRoadPosition2; 

	public float MinCurve = 10f; 
	public float MaxCurve = 30f; 

	public void UpdateRoad() {
		GetComponent < RoadCreator > ().UpdateRoad(); 
		DestroyImmediate(GetComponent < MeshCollider > ()); 
        transform.gameObject.AddComponent < MeshCollider > (); 
	}

	public void AddRoad(Vector3 position) {
		GetComponent < PathCreator > ().AddSegmentUsingScript(new Vector2(position.x, position.z)); 
	}
	
	void AddRoadRandomly(bool isFirst) {
		float x, z; 
		if (isFirst) {
			x = Random.Range(250, 260); 
			z = Random.Range(250, 260); 
			newRoadPosition = new Vector3(lastRoadPosition.x + x, 0f, lastRoadPosition.z + z); 
		}	
		else {
			x = Random.Range(MinCurve, MaxCurve); 
			do {		
				z = Random.Range(MinCurve, MaxCurve); 
				newRoadPosition = new Vector3(lastRoadPosition.x + x, 0f, lastRoadPosition.z + z); 
			} while (Vector3.Distance(newRoadPosition, lastRoadPosition) < 200); 
		}
        AddRoad(newRoadPosition); 
		lastRoadPosition = newRoadPosition; 
	}

	void AddRoadManually(Vector3 position) {	
		newRoadPosition = position; 
		AddRoad(newRoadPosition); 
		lastRoadPosition = newRoadPosition; 
	}

	void Start () {
		instance = this; 
		
		AddRoadManually(new Vector3(300, 0, 300)); 

		AddRoadRandomly(true); 
		for (int i = 0; i < 19; i++) {
			AddRoadRandomly(false); 
		}

		UpdateRoad(); 
		DecorationsManager.instance.AddRoadDecorations(); 
	}
}
