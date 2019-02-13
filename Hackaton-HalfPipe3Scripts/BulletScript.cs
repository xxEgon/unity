using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	private GameObject Enemy1;
	private GameObject Enemy2;
	private GameObject Enemy3;
	void Start () {
		// Destroy the bullet after 2 seconds
		Destroy(gameObject, 2.0f);
		Enemy1=GameObject.Find("Enemy1(Clone)");
		Enemy2=GameObject.Find("Enemy2(Clone)");
		Enemy3=GameObject.Find("Enemy3(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
    { 
		if(col.name=="Enemy1(Clone)")
		{
			Debug.Log(this);
			Enemy1.GetComponent<Enemy1Stats>().hp-=10;
			
			Destroy(this.gameObject,0);
		}

		if(col.name=="Enemy2(Clone)")
		{
			Debug.Log(this);
			Enemy2.GetComponent<Enemy2Stats>().hp-=10;
			Destroy(this.gameObject,0);
		}
		if(col.name=="Enemy3(Clone)")
		{
			Debug.Log(this);
			Enemy3.GetComponent<Enemy3Stats>().hp-=10;
			Destroy(this.gameObject,0);
		}
		if(col.name=="walls(Clone)"||col.name=="doorsSprite(Clone)"||col.name=="doorsSpriteOut(Clone)"){
			Destroy(this.gameObject,0);
		}
    }


}
