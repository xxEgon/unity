using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Stats : MonoBehaviour {

	private GameObject player;
    private Rigidbody2D body;
	private float speed;
	public int hp = 100;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		speed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		var heading = player.transform.position - transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		transform.position += direction * (speed * Time.deltaTime);

		if(hp<0){
			DoorManager.enemiescount--;
			if(DoorManager.enemiescount <= 0){
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/doorsopen");
				DoorManager.opendoors = true;
			}
			Destroy(this.gameObject);
        }
	}



}