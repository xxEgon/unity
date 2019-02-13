using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Stats : MonoBehaviour {

public int hp=10;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		InvokeRepeating("Fire",0,1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(hp<0){
			DoorManager.enemiescount--;
			if(DoorManager.enemiescount <= 0){
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/doorsopen");
				DoorManager.opendoors = true;
			}
			Destroy(this.gameObject);
        }
	}
	void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
        // Add velocity to the bullet
		bullet.transform.right = player.transform.position - transform.position;
		bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*200);

        // Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);        
    }
}