using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager1 : MonoBehaviour {
	private int currentRoom;
	private Sprite spr, spr2;
	private void Start() {
		spr = Resources.Load<Sprite>("Sprites/doorsclosed");
		spr2 = Resources.Load<Sprite>("Sprites/doorsopen");
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Player"){
			if(DoorManager.opendoors){
				currentRoom = --DoorManager.currentRoom;
				GameObject levelGen = GameObject.Find("LevelGenerator");
				GameObject.Find("Main Camera").transform.position = new Vector3(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15+1.5f, levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10, -10f);
				
				GameObject.Find("Player").transform.position = new Vector2(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15, levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10);
				GameObject.Find("MinimapCamera").transform.position = new Vector3(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15+1.5f, levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10, -10f);
				
			}
		}
	}
	private void Update() {
		if(DoorManager.enemiescount<=0){
			GetComponent<SpriteRenderer>().sprite = spr2;
		}
		else{
			GetComponent<SpriteRenderer>().sprite = spr;
		}
	}
}
