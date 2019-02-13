using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class DecorationsManager:MonoBehaviour {
	
	public static DecorationsManager instance; 
	public GameObject[] decorationsTrees = new GameObject[4]; 
	public GameObject[] decorationsOther = new GameObject[3]; 

	void Start () {
		instance = this; 
		decorationsTrees[0] = Resources.Load("tree_8") as GameObject; 
		decorationsTrees[1] = Resources.Load("tree_9") as GameObject; 
		decorationsTrees[2] = Resources.Load("tree_10") as GameObject; 
		decorationsTrees[3] = Resources.Load("tree_11") as GameObject; 
		
		decorationsOther[0] = Resources.Load("Mushroom_Red_05") as GameObject; 
		decorationsOther[1] = Resources.Load("Plant_Brush_03") as GameObject; 
		decorationsOther[2] = Resources.Load("Plant_Brush_04") as GameObject; 
	}

	public void AddRoadDecorations() {
		Vector2[] points = GameObject.Find("Road").GetComponent < RoadCreator > ().points; 
		float roadWidth = GameObject.Find("Road").GetComponent < RoadCreator > ().roadWidth;
		float roadBorderWidth = GameObject.Find("Road").GetComponent < RoadCreator > ().roadBorderWidth;
	
		for (int i = 0; i < points.Length; i += 5) {

			Vector2 forward = Vector2.zero; 
            if (i < points.Length - 1) {
                forward += points[(i + 1) % points.Length] - points[i]; 
            }
            if (i > 0) {
                forward += points[i] - points[(i - 1 + points.Length) % points.Length]; 
            }

            forward.Normalize(); 
            Vector3 side = new Vector3( - forward.y, 0, forward.x);
			float sign = 1;
            if(Random.Range(0,2) == 0) {
				 sign = -1;
			}
			side*= sign;

			Vector3 objPos = Vector3.zero;
			objPos.x = points[i].x + side.x * roadWidth/roadBorderWidth + (Random.Range(0.5f, 60f) * sign) * .5f; 
			objPos.y = 0;
			objPos.z = points[i].y + side.z * roadWidth/roadBorderWidth + (Random.Range(0.5f, 60f) * sign) * .5f; 
			
			GameObject obj;
			float randomScale;
			if(i% 10 == 0) {
				obj = Instantiate(decorationsTrees[Random.Range(0, decorationsTrees.Length)], objPos, Quaternion.identity); 
				randomScale = Random.Range(0.5f, 1.1f);
			}	
			else {
				obj = Instantiate(decorationsOther[Random.Range(0, 3)], objPos, Quaternion.identity); 
				randomScale = Random.Range(0.3f, 0.6f);
			}
			obj.transform.parent = this.transform;
			obj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
		}
		
	}

}
