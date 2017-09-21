using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainCheckpoint : MonoBehaviour 
{
	public GameObject UpMessage;
	public GameObject UpMessage2;
	public GameObject player;
	public GameObject cam;
	public GameObject UpOverlay;
	public bool dontMove;

	void Update()
	{
		player.GetComponent<CharaterController> ().bowDontRun = dontMove;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent") 
		{
			StartCoroutine (ExplainUpgrade ());
		}
	}
	IEnumerator ExplainUpgrade()
	{
		yield return new WaitForSeconds (0);
		//Time.timeScale = 0.0f;
		UpMessage.SetActive(true);
		UpOverlay.SetActive (true);
		StartCoroutine(ExplainUpgrade2());
		player.GetComponent<CharaterController> ().walkSpeed = 3;
		player.GetComponent<CharaterController> ().runSpeed = 3;
	}
	IEnumerator ExplainUpgrade2()
	{
		yield return new WaitForSeconds (3);
		UpMessage.SetActive(false);
		UpMessage2.SetActive(true);
		StartCoroutine(ExplainUpgrade3());
	}
	IEnumerator ExplainUpgrade3()
	{
		yield return new WaitForSeconds (3);
		player.GetComponent<CharaterController> ().walkSpeed = 10;
		player.GetComponent<CharaterController> ().runSpeed = 20;
		UpMessage2.SetActive(false);
		UpOverlay.SetActive (false);
		//Time.timeScale = 0.0f;
		//player.GetComponent<CharaterController>().enabled = false;
		//cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		//InGameCont.GetComponent<InGameController> ().openSkills ();
		//Cursor.visible = true;
		//Cursor.lockState = CursorLockMode.None;
	}
}
