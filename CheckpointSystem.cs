using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour 
{
	public GameObject message;
	public GameObject thisTrigger;
	public GameObject player;
	public GameObject spawnLoc;
	public GameObject HUD;
	public Texture fullHealth;
	public Texture dead;
	public GameObject GameOverText;
	public GameObject Background;
	public GameObject restartButton;
	public GameObject restartButton2;
	public GameObject quitButton;
	public GameObject cam;
	public GameObject triggerRemove;
	//public GameObject currentSpawnLoc;
	public bool isFirst = false;
	public bool UIcheck = true;
	public GameObject fadeIn;
	private GameObject spawn;

	void Start()
	{
		//currentSpawnLoc = -1;
		UIcheck = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent")
		{
			message.SetActive (true);
			spawnLoc.tag = "Checkpoint";
			StartCoroutine (StopMessage ());
			triggerRemove.transform.localScale -= new Vector3 (1, 1, 1);
			isFirst = true;
			UIcheck = false;
			//currentSpawnLoc = 0;
		}
	}

	void Update () 
	{
		if(player.GetComponent<CharaterController>().playerHealth <= 0 && isFirst == true && UIcheck == false)
		{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            HUD.GetComponent<RawImage>().texture = dead;
            GameOverText.SetActive(true);
            Background.SetActive(true);
            quitButton.SetActive(true);
            cam.GetComponent<AnotherOrbitCamTest>().enabled = false;
            restartButton.SetActive(true);
            restartButton2.SetActive(false);
        }
	}
    IEnumerator DeadMessage()
    {
        yield return new WaitForSeconds(2);
        
    }

	IEnumerator StopMessage()
	{
		yield return new WaitForSeconds (3);
		message.SetActive (false);
		thisTrigger.GetComponent<BoxCollider> ().isTrigger = false;
		thisTrigger.transform.position += new Vector3 (1, 1000, 1);
		//Destroy (triggerRemove);
	}

	public void restartLevel()
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		HUD.GetComponent<RawImage>().texture = fullHealth;
		fadeIn.SetActive(true);
		StartCoroutine(FadeIn());
		GameOverText.SetActive(false);
		Background.SetActive(false);
		restartButton.SetActive(false);
		restartButton2.SetActive(false);
		quitButton.SetActive(false);
		cam.GetComponent<AnotherOrbitCamTest>().enabled = true;
		player.GetComponent<CharaterController> ().playerHealth = 100;
		//Instantiate (player, spawnLoc.transform.position, Quaternion.identity);
		spawn = GameObject.FindGameObjectWithTag("Checkpoint");
		player.transform.position = spawn.transform.position;
		//player.transform.position = spawnLoc.transform.position;
		//int scene = SceneManager.GetActiveScene().buildIndex;
		// SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	IEnumerator FadeIn()
	{
		yield return new WaitForSeconds (0.8f);
		fadeIn.SetActive (false);
	}

}
