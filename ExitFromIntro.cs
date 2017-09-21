using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFromIntro : MonoBehaviour 
{
	Scene currentScene;
	private string scene;
	private GameObject music;
	public GameObject text;
	private float counter;

	void Update()
	{
		counter += Time.deltaTime;
		music = GameObject.FindGameObjectWithTag ("Music");
		StartCoroutine (goToMenu ());
		text.SetActive (true);
		if(Input.GetButtonDown ("Fire1") && counter >= 2.0f)
		{
			Destroy (music);
			SceneManager.LoadScene ("LoadingSceneToForest");
		}
	}

	IEnumerator goToMenu()
	{
		yield return new WaitForSeconds (52f);
		Destroy (music);
		text.SetActive (false);
		SceneManager.LoadScene ("LoadingSceneToForest");
	}
}
