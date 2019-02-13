using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Death(string txt){
		GameObject.Find("DeathText").GetComponent<Text>().text = txt;
		GetComponent<Canvas>().enabled = true;
		StartCoroutine(LoadFirstScene());
	}
	private IEnumerator LoadFirstScene(){
		yield return new WaitForSeconds(5);
		GetComponent<Canvas>().enabled = false;
		PlayerController.hp = 200;
		DoorManager.enemiescount = 0;
		SceneManager.LoadScene("MenuScene");
	}
}
