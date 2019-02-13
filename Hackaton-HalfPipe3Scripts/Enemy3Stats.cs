using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Stats : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
	private GameObject player;
    private Rigidbody2D body;
	private float speed;
	// Use this for initialization
	public int hp=10;
	void Start () {
		player = GameObject.Find("Player");
		speed = 1f;
		InvokeRepeating("Fire",0,1f);
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

	void OnTriggerEnter2D (Collider2D col)
    { 
	
    }
	void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
        // Add velocity to the bullet
		bullet.transform.right = player.transform.position - transform.position;
		bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*300);

        // Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);        
    }
}