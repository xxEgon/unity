using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsManager : MonoBehaviour {

	public static ColorsManager instance;
	private GameObject walls, spikesBottom, spikesTop, spikesRight, spikesLeft, background, score;
	private Color colorWalls, colorBackground, colorScore;

	// Use this for initialization
	void Start () {
		instance = this;

		walls = GameObject.Find("Walls");
		spikesBottom = GameObject.Find("SpikesBottom");
		spikesTop = GameObject.Find("SpikesTop");
		spikesRight = GameObject.Find("SpikesRight");
		spikesLeft = GameObject.Find("SpikesLeft");
		background = GameObject.Find("Background");
		score = GameObject.Find("Score");

		colorWalls = GameObject.Find("WallBottom").GetComponent<SpriteRenderer>().color;
		colorBackground = background.GetComponent<SpriteRenderer>().color;
		colorScore = score.GetComponent<Text>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateSpritesColors2(GameObject g) {
		float childrenCount = g.transform.childCount;
		for(int i= 0;i<  childrenCount; i++) {
			g.transform.GetChild(i).GetComponent<SpriteRenderer>().color = colorWalls;
		}
	}

	void UpdateSpritesColors () {
		UpdateSpritesColors2(walls);
		UpdateSpritesColors2(spikesTop);
		UpdateSpritesColors2(spikesBottom);
		UpdateSpritesColors2(spikesRight);
		UpdateSpritesColors2(spikesLeft);
		background.GetComponent<SpriteRenderer>().color = colorBackground;
		score.GetComponent<Text>().color = colorScore;
	}

	public void ChangeColors () {
		float r= Random.Range(0.2f, 0.7f), g= Random.Range(0.2f, 0.7f), b= Random.Range(0.2f, 0.7f);
		float backgroundDifference = 0.2f, scoreDifference = 0.15f;

		colorWalls = new Color(r, g, b);
		colorBackground = new Color(colorWalls.r+ backgroundDifference, colorWalls.g+ backgroundDifference, colorWalls.b+ backgroundDifference);
		colorScore = new Color(colorWalls.r+ scoreDifference, colorWalls.g+ scoreDifference, colorWalls.b+ scoreDifference);
		UpdateSpritesColors();
	}
}
