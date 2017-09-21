using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
	private float counter;
	private GameObject music;
    void Start()
    {
		Time.timeScale = 1.0f;
		DestroyImmediate (GameObject.Find ("Player 1 1"));
		DestroyImmediate(GameObject.FindGameObjectWithTag("EnemyHit"));
        Cursor.lockState = CursorLockMode.Confined;
    }
	void Update()
	{
		counter += Time.deltaTime;
		music = GameObject.FindGameObjectWithTag ("Music");

		if (Input.GetButtonDown ("Fire1")&& counter >= 2.0f) 
		{
			Destroy (music);
			SceneManager.LoadScene ("MainMenu");
		}
		StartCoroutine (goToMenu ());
	}
	public void retToMenu()
	{
		StartCoroutine (EnterClick ());
		StartCoroutine (goToMenu ());
	}

	IEnumerator EnterClick()
	{
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("MainMenu");
	}

	IEnumerator goToMenu()
	{
		yield return new WaitForSeconds (98f);
		SceneManager.LoadScene ("MainMenu");
	}
}
