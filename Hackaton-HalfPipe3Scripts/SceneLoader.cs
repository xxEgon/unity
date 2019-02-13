﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(StartGame);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame(){
		SceneManager.LoadScene("SampleScene");
		
	}
}
