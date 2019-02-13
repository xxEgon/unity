using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	public void FadeIn(){
		StartCoroutine(DoFade(true));
	}
	public void FadeOut(){
		StartCoroutine(DoFade(false));
	}
	IEnumerator DoFade(bool fIn){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		if(fIn){
			while(canvasGroup.alpha<1f){
				canvasGroup.alpha += Time.deltaTime;
				yield return null;
			}
		}
		else{
			while(canvasGroup.alpha>0f){
				canvasGroup.alpha -= Time.deltaTime*2;
				yield return null;
			}
			GetComponent<Canvas>().enabled = false;
		}
		yield return null;
	}
}
