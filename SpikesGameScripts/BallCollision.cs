using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

	public static BallCollision instance;
	public bool gameOver;
	

	// Use this for initialization
	void Start () {
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(!gameOver){
			switch(other.gameObject.name){
			case "WallLeft":		
				BallMovement.instance.ChangeDirection(true);
				ScoreManager.instance.AddScore();
				AudioManager.instance.PlayWallSound();
				SideSpikesManager.instance.MoveSpikes(true);
				SideSpikesManager.instance.RandomizeSpikes(true);
				break;
			case "WallRight":
				BallMovement.instance.ChangeDirection(false);
				ScoreManager.instance.AddScore();
				AudioManager.instance.PlayWallSound();
				SideSpikesManager.instance.MoveSpikes(false);
				SideSpikesManager.instance.RandomizeSpikes(false);
				break;
			case "SpikesTop":
			case "SpikesBottom":
			case "SideSpike":
				gameOver = true;
				GameObject.Find("GameOverCanvas").GetComponent<GameOver>().ShowGameOverPanel();
				BallMovement.instance.StopMovement();
				AudioManager.instance.PlayGameOverSound();
				ScoreManager.instance.UpdateHighscore();
				break;
			}
		}
	}

}
