using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playCredits : MonoBehaviour 
{
	//public MovieTexture credits;

	void Awake()
	{
		//credits.Play ();
	}

	void Start () 
	{
		//GetComponent<RawImage>().texture = credits as MovieTexture;
	}

	void Update () 
	{
		StartCoroutine (endOfCredits ());
	}

	IEnumerator endOfCredits()
	{
		yield return new WaitForSeconds (40.0f);
		SceneManager.LoadScene ("MainMenu");
	}
}
