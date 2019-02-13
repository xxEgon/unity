using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {
	private Item item;
	private string name;
	private string type;
	// Use this for initialization
	void Start () {
		name = gameObject.name;
		type = name.Substring(0,1);
		name = name.Substring(0,name.Length-7);
		item = new Item(
			name,
			Resources.Load<Sprite>("Sprites/"+name),
			type
		);
		Debug.Log(name+" adafa");	
		//Debug.Log("name - " + item.GetItemName() +", icon -" + item.GetItemIcon());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{
			// tu wykonaj zebranie itema ze sceny
			// jeśli ma jakiegoś określonego taga
			// dla pewności loguj ten fakt
			bool picked = InventoryManager.instance.AddItem(item);
			if (picked) Destroy(gameObject); // item zebrany
		}
	} */
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.transform.name == "Player"){
			bool picked;
			Item replaced;
			Sprite spr;
			SpriteRenderer sprRenderer;
			switch(type){
				case "0":
					picked = InventoryManager.instance.AddItem(item);
					if (picked) Destroy(gameObject);
					break;
				case "1":
					if(InventoryManager.instance.activeExists){
						replaced = InventoryManager.instance.ReplaceActive(item);
						item = replaced;
						spr = replaced.GetItemIcon();
						sprRenderer = (SpriteRenderer) GetComponent<Renderer>();
						sprRenderer.sprite = spr;
						gameObject.name = replaced.GetItemName();
					}
					else{
						picked = InventoryManager.instance.AddActive(item);
						if (picked) Destroy(gameObject);
					}
					break;
				case "2":
					replaced = InventoryManager.instance.ReplaceWeapon(item);
					item = replaced;
					spr = replaced.GetItemIcon();
					sprRenderer = (SpriteRenderer) GetComponent<Renderer>();
					sprRenderer.sprite = spr;
					gameObject.name = replaced.GetItemName();
					break;
			}
			replaced = null;
		}
	}
}
