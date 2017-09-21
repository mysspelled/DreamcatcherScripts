using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuController : MonoBehaviour 
{
	public AudioClip click;
	public AudioSource clickSound;
	public Button Cont;
	public GameObject music;
	Scene currentScene;
	private string scene;

    void Awake()
    {
		//DontDestroyOnLoad (music);
        //make sure cursor is visable Eryc 06-09-17
		//Destroy(GameObject.Find("Player 1 1"));
		Time.timeScale = 1.0f;
	
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

	void Start()
	{
		DontDestroyOnLoad (music);
	}
	public void startGame()
	{
		StartCoroutine (StartClick ());
	}

	public void openControls()
	{
		StartCoroutine (OptionClick ());
	}

	public void openCredits()
	{
		StartCoroutine (CreditsClick ());
	}

	public void quitGame()
	{
		StartCoroutine (ExitClick ());
	}
	IEnumerator StartClick()
	{
		yield return new WaitForSeconds (0.09f);
		SceneManager.LoadScene ("StoryIntroScene");
	}
	IEnumerator OptionClick()
	{
		yield return new WaitForSeconds (0.09f);
		SceneManager.LoadScene ("Controls");
	}
	IEnumerator CreditsClick()
	{
		yield return new WaitForSeconds (0.09f);
		SceneManager.LoadScene ("Credits");
	}
	IEnumerator ExitClick()
	{
		yield return new WaitForSeconds (0.09f);
		Application.Quit ();
		Debug.Log ("Quit is selected");
	}
}
