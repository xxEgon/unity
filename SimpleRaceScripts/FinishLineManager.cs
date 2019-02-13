using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineManager : MonoBehaviour {

	private int AwayFromRoadEnd;

	// Use this for initialization
	void Start () {
		AwayFromRoadEnd = 1500;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetFinishLine(Vector2[] points) { 
		transform.position = new Vector3(points[points.Length - AwayFromRoadEnd].x, 2, points[points.Length - AwayFromRoadEnd].y);
		transform.LookAt(new Vector3(points[points.Length - AwayFromRoadEnd - 5].x, 2, points[points.Length - AwayFromRoadEnd - 5].y));
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Race finished by " + other.gameObject.name);

		if(other.gameObject.name == "Car"){
			Debug.Log("YOU FINISHED!");
			StartCoroutine(GameRestart());
		}
			
	}

	IEnumerator GameRestart() {
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("menu");
	}
}
