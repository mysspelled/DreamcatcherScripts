using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestoyPrevCP : MonoBehaviour {

	public GameObject message;
	public GameObject thisTrigger;
	public GameObject player;
	public GameObject spawnLoc;
	public GameObject spawnLoc2;
	public GameObject HUD;
	public Texture fullHealth;
	public Texture dead;
	public GameObject GameOverText;
	public GameObject Background;
	public GameObject Background2;
	public GameObject restartButton;
	public GameObject restartButton2;
	public GameObject restartButton3;
	public GameObject quitButton;
	public GameObject cam;
	public GameObject triggerRemove;
	public GameObject newCp;
	public GameObject removeOldCp;
	//public GameObject currentSpawnLoc;
	public bool notFirst = false;
	//public GameObject firstCP;
	public bool cpRemove = true;
	public GameObject light1;
	public GameObject light2;
	public AudioClip cpSound;
	public AudioSource audio;



	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent")
		{
			audio.PlayOneShot (cpSound, 0.15f);
			removeOldCp.tag = "Untagged";
			spawnLoc2.tag = "Untagged";
			newCp.tag = "Checkpoint";
			removeOldCp.transform.localScale -= new Vector3 (1, 1, 1);
			message.SetActive (true);
			StartCoroutine (StopMessage ());
			triggerRemove.transform.localScale -= new Vector3 (1, 1, 1);
			notFirst = true;
			cpRemove = false;
			light1.SetActive (false);
			light2.SetActive (true);
			//firstCP.GetComponent<CheckpointSystem> ().isFirst = cpRemove;
		}
	}

	void Update () 
	{
		//HUD = GameObject.FindGameObjectWithTag ("PlayerParent").GetComponent<CharaterController> ().HUD;
		//message = GameObject.FindGameObjectWithTag ("PlayerParent").GetComponent<Canvas> ().C;
		//cam = GameObject.FindGameObjectWithTag("MainCamera");
		if(GameObject.FindGameObjectWithTag("PlayerParent").GetComponent<CharaterController>().playerHealth <= 0 && notFirst == true && cpRemove == false)
		{
            //Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            HUD.GetComponent<RawImage>().texture = dead;
            GameOverText.SetActive(true);
			Background.SetActive (false);
            Background2.SetActive(true);
			Background2.GetComponent<Image> ().CrossFadeAlpha (200,1700,false);
            quitButton.SetActive(true);
            cam.GetComponent<AnotherOrbitCamTest>().enabled = false;
            restartButton.SetActive(false);
            restartButton3.SetActive(false);
            restartButton2.SetActive(true);
        }
	}

    IEnumerator DeadMessage()
    {
        yield return new WaitForSeconds(2f);
        
    }

	IEnumerator StopMessage()
	{
		yield return new WaitForSeconds (4);
		message.SetActive (false);
		thisTrigger.GetComponent<BoxCollider> ().isTrigger = false;
		thisTrigger.transform.position += new Vector3 (1, 1000, 1);

	}

	/*public void restartLevel()
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		HUD.GetComponent<RawImage>().texture = fullHealth;
		GameOverText.SetActive(false);
		Background.SetActive(false);
		restartButton.SetActive (false);
		restartButton2.SetActive(false);
		quitButton.SetActive(false);
		cam.GetComponent<AnotherOrbitCamTest>().enabled = true;
		player.GetComponent<CharaterController> ().playerHealth = 100;
		//Instantiate (player, spawnLoc.transform.position, Quaternion.identity);
		player.transform.position = spawnLoc.transform.position;
		//int scene = SceneManager.GetActiveScene().buildIndex;
		// SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}*/


}
