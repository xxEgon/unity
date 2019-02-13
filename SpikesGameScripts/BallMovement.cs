using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
	public static BallMovement instance;
	private float moveForce, jumpForce;
	private Rigidbody2D rigidBody;
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		instance = this;
		jumpForce = 6f;
		moveForce = 170f;
		rigidBody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidBody.simulated = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
	bool firstJump = true;
	public void Jump() {
		if(spriteRenderer.enabled && firstJump) {
			firstJump = false;
			rigidBody.simulated = true;
			rigidBody.AddForce(new Vector2(moveForce, 0), ForceMode2D.Force);
		}
		rigidBody.velocity = new Vector2( rigidBody.velocity.x ,0f); 
		rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		AudioManager.instance.PlayTapSound();
	}

	public void ChangeDirection (bool right) {
		if(right) 
			rigidBody.AddForce(new Vector2(moveForce, 0), ForceMode2D.Force);
		else
			rigidBody.AddForce(new Vector2(-moveForce, 0), ForceMode2D.Force);
	}
	
	public void StartMovement () {
		spriteRenderer.enabled = true;
	}

	public void StopMovement () {
		rigidBody.velocity = Vector2.zero; 
		moveForce = 0;
		jumpForce = 0;
		rigidBody.simulated = false;
		spriteRenderer.enabled = false;
		Explode();
	}

	void Explode() {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
    }
}
