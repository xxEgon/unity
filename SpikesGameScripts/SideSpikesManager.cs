using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpikesManager : MonoBehaviour {
	public static SideSpikesManager instance;
	private bool animationRight, animationLeft;
	private GameObject spikesLeft, spikesRight;
	private Transform[] smallSpikesLeft, smallSpikesRight;
	private int numberOfSpikes;
	// Use this for initialization
	void Start () {
		instance = this;
		spikesLeft = GameObject.Find("SpikesLeft");
		spikesRight = GameObject.Find("SpikesRight");
		smallSpikesLeft = new Transform[12];
		smallSpikesRight = new Transform[12];
		for(int i = 0; i<spikesLeft.transform.childCount;i++){
			smallSpikesLeft[i] = spikesLeft.transform.GetChild(i);
			smallSpikesRight[i] = spikesRight.transform.GetChild(i);
		}
		numberOfSpikes = 3;
		SideSpikesManager.instance.RandomizeSpikes(true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(animationRight && transform.position.x < 0.54f)
			transform.Translate(new Vector2(0.03f, 0f));
		else
			animationRight = false;
		if(animationLeft && transform.position.x > 0.01f)
			transform.Translate(new Vector2(-0.03f, 0f));
		else
			animationLeft = false;
	}
	public void MoveSpikes(bool right){
		if(right) {
			animationLeft = true;
		}
		else{
			animationRight = true;
		}
	}

	public void ToggleSpikesVisibility (bool on) {
		spikesLeft.SetActive(on);
		spikesRight.SetActive(on);
	}
	public void AddSpikes(){
		if(ScoreManager.instance.score % (numberOfSpikes*numberOfSpikes) == 0 && numberOfSpikes<9){
			numberOfSpikes++;
		}
	}
	
	List<int> randomNumbers = new List<int>();
	int r;
	public void RandomizeSpikes(bool right){
		randomNumbers.Clear();
		for(int i = 0; i < numberOfSpikes; i++)
		{
			do{
				r = Random.Range(0,12);
			}
			while(randomNumbers.Contains(r));
			randomNumbers.Add(r);
		}
		if(right){
			for(int i = 0; i< spikesRight.transform.childCount;i++){
				smallSpikesRight[i].gameObject.SetActive(true);
				if(!randomNumbers.Contains(i)){
					smallSpikesRight[i].gameObject.SetActive(false);
				}
			}
		}
		else{
			for(int i = 0; i< spikesLeft.transform.childCount;i++){
				smallSpikesLeft[i].gameObject.SetActive(true);
				if(!randomNumbers.Contains(i)){
					smallSpikesLeft[i].gameObject.SetActive(false);
				}
			}
		}
	}
}
