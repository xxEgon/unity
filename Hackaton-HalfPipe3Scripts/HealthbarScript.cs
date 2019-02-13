using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour {

    Image healthbar;
    int maxHealth = 200;
    public static float health;
	// Use this for initialization
	void Start () {
                healthbar = GetComponent<Image>();
                maxHealth = PlayerController.hp;
	}
	
	// Update is called once per frame
	void Update () {
                healthbar.fillAmount = PlayerController.hp / 2 * 0.01f;
	}
}
