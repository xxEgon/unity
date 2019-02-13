using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {
	public static int currentRoom = 0;
	public Transform[] enemies;
	public static int enemiescount;
	public static bool opendoors;
	private Sprite spr, spr2;
	private void Start() {
		opendoors = true;
		spr = Resources.Load<Sprite>("Sprites/doorsclosed");
		spr2 = Resources.Load<Sprite>("Sprites/doorsopen");
		Debug.Log(spr);
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Player"){
			if(opendoors){
				currentRoom++;
				GameObject levelGen = GameObject.Find("LevelGenerator");
				GameObject.Find("Main Camera").transform.position = new Vector3(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15+1.5f, levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10, -10f);
				
				GameObject.Find("Player").transform.position = new Vector2(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15, levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10);

				GameObject.Find("MinimapCamera").transform.position = new Vector3(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15+1.5f, levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10, -10f);

				if(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom] == levelGen.GetComponent<LevelGenerator>().GetIndexes1()[levelGen.GetComponent<LevelGenerator>().GetIndex()-1]
				&& levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom] == levelGen.GetComponent<LevelGenerator>().GetIndexes2()[levelGen.GetComponent<LevelGenerator>().GetIndex()-1]
				){
					Debug.Log("WYGRANA");
					 GameObject.Find("DeathCanvas").GetComponent<GameOver>().Death("YOU WON");
				}

				enemiescount = Random.Range(0,6);
				for(int k = 0; k<enemiescount;k++){
					int j = Random.Range(0,3);
					Instantiate(enemies[j], new Vector2(levelGen.GetComponent<LevelGenerator>().GetIndexes1()[currentRoom]*15+Random.Range(-5,5), levelGen.GetComponent<LevelGenerator>().GetIndexes2()[currentRoom]*10+Random.Range(-3,3)), Quaternion.identity);
					
				}
				PlayerController.hp+=20;
				if(enemiescount!=0){
					opendoors = false;
					GameObject.Find("Player").GetComponent<Collider2D>().enabled = false;
					StartCoroutine(EnableCollider());
				}
			}
		}
	}
	private void Update() {
		if(enemiescount<=0){
			GetComponent<SpriteRenderer>().sprite = spr2;
		}
		else{
			GetComponent<SpriteRenderer>().sprite = spr;
		}
	}
	private IEnumerator EnableCollider(){
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("Player").GetComponent<Collider2D>().enabled = true;
	}
}
