using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulet : MonoBehaviour {
private GameObject player;
	// Use this for initialization
	void Start () {
		player=GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
    { 
		if(col.name=="Player")
		{
			Debug.Log(this);
			PlayerController.hp-=10;
			Destroy(this.gameObject,0);
		}
    }
}
