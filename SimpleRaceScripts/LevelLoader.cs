using System.Collections; 
using UnityEngine; 
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class LevelLoader:MonoBehaviour {

	Canvas loadingCanvas;
	Slider slider;

	void Start () {
		loadingCanvas = GameObject.Find("LoadingCanvas").GetComponent<Canvas>();
		slider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
	}

	public void LoadLevel(string levelName) {
		StartCoroutine(LoadAsync(levelName)); 
	}

	IEnumerator LoadAsync (string levelName) {
		AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
		//operation.allowSceneActivation = true;
		loadingCanvas.enabled = true; 	
		
		while (operation.isDone == false) {
			//float progress = Mathf.Clamp01(operation.progress / .9f);	
			slider.value = operation.progress;
			//Debug.Log(operation.progress); 

			yield return null; 
		}
	}

}
