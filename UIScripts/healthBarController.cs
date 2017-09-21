using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Created 6-4-17 by Daniel
public class healthBarController : MonoBehaviour 
{
	public GameObject player;
	public Slider healthbar;
	private float hb;
	private GameObject spwnDecide;
	//public GameObject spawnLoc;
	public GameObject HUD;
	public Texture fullHealth;
	public GameObject GameOverText;
	public GameObject Background;
	public GameObject Background2;
	public GameObject restartButton;
	public GameObject restartButton2;
	public GameObject restartButton3;
	public GameObject quitButton;
	public GameObject cam;
	public GameObject controller;
	private GameObject spawn;
	public GameObject fadeIn;
	public GameObject fadeOut;
	//public Image fadeInImage;



//	private GameObject cam;

	public void Awake()
	{	
		//cam = GameObject.FindGameObjectWithTag ("MainCamera");
		//hb = healthbar.GetComponent<Slider> ().value;
		//hb = player.GetComponent<CharContMoveTEST> ().playerHealth;
	}

	public void Start()
	{
		//healthbar = GetComponent<Slider> ();
	}

	public void Update()
	{
		//hb = player.GetComponent<CharContMoveTEST> ().playerHealth;
		//Debug.Log (healthbar.value);
		/*Debug.Log("hb");
		if(hb <= 0)
		{
			GameOverText.SetActive (true);
			Background.SetActive (true);
			restartButton.SetActive (true);
			quitButton.SetActive (true);
			player.GetComponent<CharContMoveTEST>().enabled = false;
			cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		}*/
	}
	public void restartLevel()
	{
		Time.timeScale = 1.0f;
		//int scene = SceneManager.GetActiveScene().buildIndex;
		//SceneManager.LoadScene(scene, LoadSceneMode.Single);
		fadeOut.SetActive(true);
		fadeOut.GetComponent<Image>().CrossFadeAlpha(0,1,false);
		StartCoroutine(FadeOut());
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		HUD.GetComponent<RawImage>().texture = fullHealth;
		GameOverText.SetActive(false);
		Background.SetActive(false);
		Background2.SetActive(false);
		restartButton.SetActive (false);
		restartButton2.SetActive(false);
		restartButton3.SetActive(false);
		quitButton.SetActive(false);
		cam.GetComponent<AnotherOrbitCamTest>().enabled = true;
		player.GetComponent<CharaterController> ().playerHealth = 100;
		spawn = GameObject.FindGameObjectWithTag("SpawnPoint");
		player.transform.position = spawn.transform.position;
	}
	public void restartForestLevel()
	{
		Time.timeScale = 1.0f;
		fadeOut.SetActive(true);
		fadeOut.GetComponent<Image>().CrossFadeAlpha(0,1,false);
		StartCoroutine(FadeOut());
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		HUD.GetComponent<RawImage>().texture = fullHealth;
		GameOverText.SetActive(false);
		Background.SetActive(false);
		Background2.SetActive(false);
		restartButton.SetActive (false);
		restartButton2.SetActive(false);
		restartButton3.SetActive(false);
		quitButton.SetActive(false);
		cam.GetComponent<AnotherOrbitCamTest>().enabled = true;
		player.GetComponent<CharaterController> ().playerHealth = 100;
		//Instantiate (player, spawnLoc.transform.position, Quaternion.identity);
		//spwnDecide = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<DestoyPrevCP>().currentSpawnLoc;
		spwnDecide = GameObject.FindGameObjectWithTag("Checkpoint");
		player.transform.position = spwnDecide.transform.position;
		//int scene = SceneManager.GetActiveScene().buildIndex;
		// SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	public void CPRestartButton()
	{
		Time.timeScale = 1.0f;
		fadeOut.SetActive(true);
		fadeOut.GetComponent<Image>().CrossFadeAlpha(0,1,false);
		StartCoroutine(FadeOut());
		Cursor.visible = false;
		fadeIn.SetActive (false);
		controller.GetComponent<InGameController> ().continueGame ();
		Cursor.lockState = CursorLockMode.Locked;
		cam.GetComponent<AnotherOrbitCamTest>().enabled = true;
		player.GetComponent<CharaterController> ().playerHealth = 100;
		spwnDecide = GameObject.FindGameObjectWithTag("Checkpoint");
		player.transform.position = spwnDecide.transform.position;
	}

	public void restartLevel2()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("templeLevel_floor1");
	}
	public void exitLevel()
	{
		Time.timeScale = 1.0f;
		Background.SetActive (false);
		Background2.SetActive(false);
		SceneManager.LoadScene ("MainMenu");
		DestroyImmediate(GameObject.FindGameObjectWithTag("PlayerParent"));
		DestroyImmediate(GameObject.FindGameObjectWithTag("EnemyHit"));
	}

	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds (1f);
		fadeOut.SetActive (false);
		fadeIn.SetActive (false);
	}
	IEnumerator FadeIn()
	{
		yield return new WaitForSeconds (2f);
		//fadeIn.SetActive (false);
		//fadeIn.GetComponent<Image> ().CrossFadeAlpha (200,200,false);
		ExitforReal ();
	}
	void ExitforReal()
	{
		
	}
}
