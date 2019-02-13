using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public Transform roomSprite, doorsSprite, doorsSpriteOut, wallsSprite;
	public Transform[] items;
	private int mapHeight=6, mapWidth= 8, startPoint, a, b;
	private int[,] tabLevel;
	private int[] indexes1;
	private int[] indexes2;
	private int index = 0;
	private float roomWidth, roomHeight;
	
	void Start () {
		roomWidth = roomSprite.GetComponent<SpriteRenderer>().bounds.size.x;
		roomHeight = roomSprite.GetComponent<SpriteRenderer>().bounds.size.y;
		indexes1= new int[mapWidth*mapHeight];
		indexes2= new int[mapWidth*mapHeight];

		Debug.Log(roomWidth+", "+roomHeight);
		Generate();
	}

	void Generate () {
		Debug.Log("START");
		tabLevel= new int[mapWidth, mapHeight];
		startPoint= Random.Range(0, mapHeight);
		int i= 1;
		tabLevel[0, startPoint]= i;
		a= 0;
		b= startPoint;
		
		bool levelNotGenerated= true;
		while(levelNotGenerated) {
			levelNotGenerated= addRoomToTab();
		}
		WriteTab();
	}

	bool addRoomToTab () {
		bool roomNotAdded= true;
		int ran;
		while(roomNotAdded){
			ran= Random.Range(0, 4);
			switch(ran) {
				case 3:
				case 0:
					if(a+1 > mapWidth-1) {
						//Debug.Log("Reached end");
						roomNotAdded= false;
						return false;
					}
					if(tabLevel[a+1, b] == 0){
						tabLevel[a+1, b]= 1;
						indexes1[index]= a+1;
						indexes2[index]= b;
						index++;
						a++;
						roomNotAdded= false;
					}
					break;
				case 1:
					if(b+1<= mapHeight-1)
					if(tabLevel[a, b+1] == 0 ){
						tabLevel[a, b+1]= 2;
						indexes1[index]= a;
						indexes2[index]= b+1;
						index++;
						b++;
						roomNotAdded= false;
					} 
					break;
				case 2:
					if(b-1>= 0)
					if(tabLevel[a, b-1] == 0){	
						tabLevel[a, b-1]= 3;
						indexes1[index]= a;
						indexes2[index]= b-1;
						index++;
						b--;
						roomNotAdded= false;
					} 
					break;
			}
		}
		//Debug.Log("Room added");
		return true;
	}
	void WriteTab () {
		//Debug.Log("WriteTab");
		int prevI= -1, prevJ= -1, m, n;
		float przes = 0.8f;
		Transform doors;
		for(int i=0 ;i< index; i++){
				m= indexes1[i]*15;
				n= indexes2[i]*10;

				Instantiate(roomSprite, new Vector3(m, n, 0), Quaternion.identity);
				Instantiate(wallsSprite, new Vector3(m, n, 0), Quaternion.identity);
				if(prevI!= -1) {
					if(tabLevel[m/15, n/10] == 2) {
						//Debug.Log("2");
						Instantiate(doorsSprite, new Vector3(prevI, prevJ+roomHeight/2-przes, -0.1f), Quaternion.identity);
						doors = Instantiate(doorsSpriteOut, new Vector3(m, n-roomHeight/2+przes, -0.1f), Quaternion.Euler(0, 0, 180));
					}
					else if(tabLevel[m/15, n/10] == 1) {
						//Debug.Log("1");
						doors = Instantiate(doorsSprite, new Vector3(prevI+roomWidth/2-przes, prevJ, -0.1f), Quaternion.Euler(0, 0, -90));
						doors = Instantiate(doorsSpriteOut, new Vector3(m-roomWidth/2+przes, n, -0.1f), Quaternion.Euler(0, 0, 90));
	
					}
					else if(tabLevel[m/15, n/10] == 3) {
						//Debug.Log("3");
						doors = Instantiate(doorsSprite, new Vector3(prevI, prevJ-roomHeight/2+przes, -0.1f), Quaternion.Euler(0, 0, 180));
						Instantiate(doorsSpriteOut, new Vector3(m, n+roomHeight/2-przes, -0.1f), Quaternion.identity);
					}
				}
				prevI= m;
				prevJ= n;
		}
		for(int i = 0; i<7; i++){
			int rand = Random.Range(0,mapWidth);
			Debug.Log(rand);
			Instantiate(items[i], new Vector2(indexes1[rand]*15+5f, indexes2[rand]*10), Quaternion.identity);
		}
		//Camera.main.transform.position = new Vector3((mapWidth/2)*15, (mapHeight/2)*10, -10f);
		GameObject.Find("Main Camera").transform.position = new Vector3(indexes1[0]*15+1.5f, indexes2[0]*10, -10f);
		GameObject.Find("Player").transform.position = new Vector2(indexes1[0]*15+1.5f, indexes2[0]*10);
	}
	public int[] GetIndexes1(){
		return indexes1;
	}
	public int[] GetIndexes2(){
		return indexes2;
	}
	public int GetIndex(){
		return index;
	}
	public int[,] GetTabLevel(){
		return tabLevel;
	}
}