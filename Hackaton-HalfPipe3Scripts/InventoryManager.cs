using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
	public delegate void OnItemPicked();
	public OnItemPicked onItemPicked;

	private List<Item> itemsList = new List<Item>();
	private Item activeItem;
	private Item weaponItem;
	public bool activeExists;
	private float[,] passives = {{1,1,1,1.2f,1},{1,1,1,1.1f,1},{1.1f,1,1,1,1.2f},{1,1.2f,1,1,1.2f},{1,1,0.8f,1,1},{1.2f,1,0.9f,1,1},{1,1.2f,1,1,1}};
    public string[] passivenames = {"0cart","0jetpack","0stone","0tesla","0belt","0sunlight","0hole"};
	private void Awake()
    {
        if (instance != null)
        {
             Debug.LogError("jest już instancja tej klasy: " + InventoryManager.instance);
            return;
        }
        instance = this;
		activeExists = false;
		weaponItem = new Item("2pistol",Resources.Load<Sprite>("Sprites/2pistol"),"2");
    }
	public bool AddItem(Item item)
	{
		Debug.Log(""+item.GetItemName());
		int index = Array.IndexOf(passivenames,item.GetItemName());
		Debug.Log(index);
		ApplyBuffs(index);
		itemsList.Add(item);
		if (onItemPicked != null) onItemPicked.Invoke();
		return true;
	}
	public bool AddActive(Item item)
	{
		activeItem = item;
		activeExists = true;
		return true;
	}
	public Item ReplaceActive(Item item){
		Item temp = activeItem;
		activeItem = item;
		return temp;
	}
	public Item ReplaceWeapon(Item item){
		Item temp = weaponItem;
		weaponItem = item;
		return temp;
	}
	public List<Item> GetItemsList()
	{
		return itemsList;
	}
	public void ApplyBuffs(int index){
		GameObject player = GameObject.Find("Player");
		player.GetComponent<PlayerShooting>().gunDamage*=passives[index,0];
		player.GetComponent<PlayerShooting>().attackSpeed*=passives[index,1];
		player.GetComponent<PlayerShooting>().fireRate*=passives[index,2];
		player.GetComponent<PlayerController>().speed*=passives[index,3];
		player.GetComponent<PlayerShooting>().weaponRange*=passives[index,4];
	}
}
