using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour 
{
	#region Variables
    
	public GameObject contFromUpgradesButton;
	public GameObject lockedUpgradeText;
	public GameObject unlockedUpgradeText;
	public GameObject FirstHawkupgradeText;
	public GameObject SecondHawkupgradeText;
	public GameObject ThirdHawkupgradeText;
	public GameObject FirstClubupgradeText;
	public GameObject SecondClubupgradeText;
	public GameObject ThirdClubupgradeText;
	public GameObject FirstBowupgradeText;
	public GameObject SecondBowupgradeText;
	public GameObject ThirdBowupgradeText;
	public GameObject areYouSureBGD;
	public GameObject areYouSureText;
	public GameObject yesButton;
	public GameObject noButton;
	public GameObject UpgBackground;
	public GameObject tUpgText;
	public GameObject cUpgText;
	public GameObject bUpgText;
	public GameObject tUpg1;
	public GameObject tUpg2;
	public GameObject tUpg3;
	public GameObject cUpg1;
	public GameObject cUpg2;
	public GameObject cUpg3;
	public GameObject bUpg1;
	public GameObject bUpg2;
	public GameObject bUpg3;
	public GameObject UpgradePntCnt;
	public GameObject retFromUpgradesBut;
	public float pnt;
	public GameObject cpSystem;
	public GameObject HUD;
	public GameObject healthBar;
	public GameObject xpBar;

	bool autolockonOff = false;
	//public GameObject pausedText;
	public GameObject optionsText;
	public GameObject skillsText;

	//public GameObject autoLockToggle;
	//public GameObject autoLockDisplay;

	public GameObject controlsImage;
	public GameObject controlsButton;
	public GameObject returnFromControls;

	public GameObject pauseMenu;
	public GameObject contButton;
	public GameObject cpRestartButton;
	public GameObject optionsButton;
	public GameObject upgradesButton;
	public GameObject quitButton;
	public GameObject returnFromOptions;
	public GameObject fadeOut;
	public GameObject player;
	private GameObject cam;

	//SLider Stuff
	public Slider xSensSlider;
	public GameObject xSensSliderGO;
	public GameObject ySensSliderGO;
	public Slider ySensSlider;
	public GameObject xSenDisplay;
	public GameObject ySenDisplay;
	public Text xValue;
	public Text yValue;
	public GameObject invertToggle;
	public GameObject invertText;
	public float xSens;
	public float ySens;

	public bool tUp1 = false;
	public bool tUp2 = false;
	public bool tUp3 = false;
	public bool cUp1 = false;
	public bool cUp2 = false;
	public bool cUp3 = false;
	public bool bUp1 = false;
	public bool bUp2 = false;
	public bool bUp3 = false;
	public bool templeHawk = false;
	public bool templeClub = false;
	public bool templeBow = false;

	private bool isPauseMenu;
	Scene currentScene;
	private string scene;
	public AudioClip getSkillSound;
	public AudioSource audio;
	#endregion

	public void Awake()
	{	
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		//player = GameObject.FindGameObjectWithTag ("Player");
		xValue.text = "1";
		yValue.text = "1";
	}

	public void Start()
	{
		//isPauseMenu = false;
		audio = GetComponent<AudioSource>();
		currentScene = SceneManager.GetActiveScene();
		scene = currentScene.name;
		//cpRestartButton.transform.localScale -= new Vector3 (1, 1, 1);
		if(scene == "Temple")
		{
			templeHawk = true;
			templeClub = true;
			templeBow = true;
		}
	}

	public void Update()
	{
        
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        currentScene = SceneManager.GetActiveScene();
        scene = currentScene.name;
        if (scene == "Temple")
        {
            templeHawk = true;
            templeClub = true;
            templeBow = true;
        }

        //isPauseMenu = false;
        xValue.text = "X sensitivity: " + xSensSlider.value.ToString();
		yValue.text = "Y sensitivity: " + ySensSlider.value.ToString ();
		//reference to currentpoints 
		pnt = player.GetComponent<CharaterController>().upgradePoints;

		//Edited 5-30-17 by Daniel to allow keyboard and controller access to pause menu.

		if(isPauseMenu == false)
		{
			if (Input.GetKeyDown (KeyCode.P)|| Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("xb_start") && isPauseMenu == false) 
			{
				//show cursor in menue Eryc 06-09-17
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				contButton.GetComponent<Button> ().interactable = true;
				Time.timeScale = 0.0f;
				isPauseMenu = true;
				pauseMenu.SetActive (true);
				//pausedText.SetActive (true);
				contButton.SetActive (true);
				cpRestartButton.SetActive (true);
				cpRestartButton.GetComponent<Button> ().interactable = false;
				controlsButton.SetActive (true);
				upgradesButton.SetActive (true);
				optionsButton.SetActive (true);
				quitButton.SetActive (true);
				player.GetComponent<CharaterController>().enabled = false;
				cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
				if (scene != "Temple") 
				{
					if(cpSystem.GetComponent<CheckpointSystem>().UIcheck == false)
					{
						//cpRestartButton.transform.localScale += new Vector3 (1, 1, 1);
						cpRestartButton.GetComponent<Button> ().interactable = true;
					}
				}
				if (scene == "Temple") 
				{
					//Need to fix this when i get into temple 7/12/17
					cpRestartButton.GetComponent<Button> ().interactable = true;
				}
			}
		}

		/*if(autolockonOff == false)
		{
			player.GetComponent<AutoLockOn> ().enabled = false;
			player.GetComponent<LockOn> ().enabled = true;
			//Debug.Log("auto lock is on");
		}
		if(autolockonOff == true)
		{
			player.GetComponent<AutoLockOn> ().enabled = true; 
			player.GetComponent<LockOn> ().enabled = false;
			//Debug.Log("auto lock is off");
		}*/
	}

	void LateUpdate()
	{
			PointButtonHandler ();
	 /* if(isPauseMenu == true)
		{
			if (Input.GetButtonDown ("xb_back") || Input.GetKeyDown (KeyCode.Return) && isPauseMenu == true) {
				continueGame ();
			}
		} */
	}

	public void pauseTheGame()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		contButton.GetComponent<Button> ().interactable = true;
		Time.timeScale = 0.0f;
		isPauseMenu = true;
		pauseMenu.SetActive (true);
		//pausedText.SetActive (true);
		contButton.SetActive (true);
		cpRestartButton.SetActive (true);
		cpRestartButton.GetComponent<Button> ().interactable = false;
		controlsButton.SetActive (true);
		upgradesButton.SetActive (true);
		optionsButton.SetActive (true);
		quitButton.SetActive (true);
		player.GetComponent<CharaterController>().enabled = false;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		if (scene != "Temple") 
		{
			if(cpSystem.GetComponent<CheckpointSystem>().UIcheck == false)
			{
				//cpRestartButton.transform.localScale += new Vector3 (1, 1, 1);
				cpRestartButton.GetComponent<Button> ().interactable = true;
			}
		}
		if (scene == "Temple") 
		{
			//Need to fix this when i get into temple 7/12/17
			cpRestartButton.GetComponent<Button> ().interactable = true;
		}
	}

	public void continueGame()
	{
		StartCoroutine (ContinueWait ());
		Time.timeScale = 1.0f;
		isPauseMenu = false;
		//pausedText.SetActive (false);
		pauseMenu.SetActive (false);
		contButton.SetActive (false);
		cpRestartButton.SetActive (false);
		controlsButton.SetActive (false);
		quitButton.SetActive (false);
		upgradesButton.SetActive (false);
		optionsButton.SetActive (false);
		player.GetComponent<CharaterController>().enabled = true;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = true;
        //hide cursor again Eryc 06-09-17
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
		
	public void openOptions()
	{
		isPauseMenu = true;
		//autoLockToggle.SetActive (true);
		//autoLockDisplay.SetActive (true);
		controlsButton.SetActive (false);
		//pausedText.SetActive (false);
		optionsText.SetActive (true);
		returnFromOptions.SetActive (true);
		contButton.SetActive (false);
		cpRestartButton.SetActive (false);
		quitButton.SetActive (false);
		upgradesButton.SetActive (false);
		optionsButton.SetActive (false);
		xSensSliderGO.SetActive (true);
		ySensSliderGO.SetActive (true);
		xSenDisplay.SetActive (true);
		ySenDisplay.SetActive (true);
		invertText.SetActive (true);
		invertToggle.SetActive (true);
	}

	public void retFromOptions()
	{
		isPauseMenu = true;
		//autoLockToggle.SetActive (false);
		//autoLockDisplay.SetActive (false);
		controlsImage.SetActive (false);
		controlsButton.SetActive (true);
		returnFromControls.SetActive (false);
		//pausedText.SetActive (true);
		optionsText.SetActive (false);
		skillsText.SetActive (false);
		contButton.SetActive (true);
		cpRestartButton.SetActive (true);
		upgradesButton.SetActive (true);
		optionsButton.SetActive (true);
		quitButton.SetActive (true);
		ySensSliderGO.SetActive (false);
		xSensSliderGO.SetActive (false);
		xSenDisplay.SetActive (false);
		ySenDisplay.SetActive (false);
		returnFromOptions.SetActive (false);
		invertText.SetActive (false);
		invertToggle.SetActive (false);
		player.GetComponent<CharaterController>().enabled = false;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		pauseMenu.SetActive (true);
	}

	public void retFromUpgrade()
	{
		isPauseMenu = true;
		UpgBackground.SetActive (false);
		tUpgText.SetActive (false);
		cUpgText.SetActive (false);
		bUpgText.SetActive (false);
		pauseMenu.SetActive (true);
		controlsButton.SetActive (true);
		returnFromControls.SetActive (false);
		//pausedText.SetActive (true);
		optionsText.SetActive (false);
		skillsText.SetActive (false);
		contButton.SetActive (true);
		cpRestartButton.SetActive (true);
		upgradesButton.SetActive (true);
		optionsButton.SetActive (true);
		quitButton.SetActive (true);
		player.GetComponent<CharaterController>().enabled = false;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		retFromUpgradesBut.SetActive (false);
		tUpg1.SetActive(false);
		tUpg2.SetActive(false);
		tUpg3.SetActive (false);
		cUpg1.SetActive(false);
		cUpg2.SetActive(false);
		cUpg3.SetActive (false);
		bUpg1.SetActive(false);
		bUpg2.SetActive(false);
		bUpg3.SetActive (false);
		UpgradePntCnt.SetActive (false);
		FirstHawkupgradeText.SetActive (false);
		SecondHawkupgradeText.SetActive (false);
		ThirdHawkupgradeText.SetActive (false);
		FirstClubupgradeText.SetActive (false);
		SecondClubupgradeText.SetActive (false);
		ThirdClubupgradeText.SetActive (false);
		FirstBowupgradeText.SetActive (false);
		SecondBowupgradeText.SetActive (false);
		ThirdBowupgradeText.SetActive (false);
		contFromUpgradesButton.SetActive (false);
		lockedUpgradeText.SetActive (false);
		unlockedUpgradeText.SetActive (false);
	}

	public void ContinueFromUpgrades()
	{
		//retFromOptions ();
		continueGame ();
		retFromUpgradesBut.SetActive (false);
		tUpg1.SetActive(false);
		tUpg2.SetActive(false);
		tUpg3.SetActive (false);
		cUpg1.SetActive(false);
		cUpg2.SetActive(false);
		cUpg3.SetActive (false);
		bUpg1.SetActive(false);
		bUpg2.SetActive(false);
		bUpg3.SetActive (false);
		UpgradePntCnt.SetActive (false);
		FirstHawkupgradeText.SetActive (false);
		SecondHawkupgradeText.SetActive (false);
		ThirdHawkupgradeText.SetActive (false);
		FirstClubupgradeText.SetActive (false);
		SecondClubupgradeText.SetActive (false);
		ThirdClubupgradeText.SetActive (false);
		FirstBowupgradeText.SetActive (false);
		SecondBowupgradeText.SetActive (false);
		ThirdBowupgradeText.SetActive (false);
		contFromUpgradesButton.SetActive (false);
		lockedUpgradeText.SetActive (false);
		unlockedUpgradeText.SetActive (false);
		tUpgText.SetActive (false);
		cUpgText.SetActive (false);
		bUpgText.SetActive (false);
		UpgBackground.SetActive (false);
		skillsText.SetActive (false);
	}

	public void openSkills()
	{
		isPauseMenu = true;
		UpgBackground.SetActive (true);
		contFromUpgradesButton.SetActive (true);
		lockedUpgradeText.SetActive (true);
		unlockedUpgradeText.SetActive (true);
		tUpgText.SetActive (true);
		cUpgText.SetActive (true);
		bUpgText.SetActive (true);
		skillsText.SetActive (true);
		pauseMenu.SetActive (false);
		//pausedText.SetActive (false);
		controlsButton.SetActive (false);
		contButton.SetActive (false);
		cpRestartButton.SetActive (false);
		quitButton.SetActive (false);
		upgradesButton.SetActive (false);
		optionsButton.SetActive (false);
		retFromUpgradesBut.SetActive (true);
	
		tUpg1.SetActive(true);
		//tUpg1.GetComponent<Button> ().enabled = false;
		tUpg1.GetComponent<Button> ().interactable = false;
		tUpg2.SetActive(true);
		//tUpg2.GetComponent<Button> ().enabled = false;
		tUpg2.GetComponent<Button> ().interactable = false;
		tUpg3.SetActive (true);
		//tUpg3.GetComponent<Button> ().enabled = false;
		tUpg3.GetComponent<Button> ().interactable = false;
		cUpg1.SetActive(true);
		//cUpg1.GetComponent<Button> ().enabled = false;
		cUpg1.GetComponent<Button> ().interactable = false;
		cUpg2.SetActive(true);
		//cUpg2.GetComponent<Button> ().enabled = false;
		cUpg2.GetComponent<Button> ().interactable = false;
		cUpg3.SetActive (true);
		//cUpg3.GetComponent<Button> ().enabled = false;
		cUpg3.GetComponent<Button> ().interactable = false;
		bUpg1.SetActive(true);
		//bUpg1.GetComponent<Button> ().enabled = false;
		bUpg1.GetComponent<Button> ().interactable = false;
		bUpg2.SetActive(true);
		//bUpg2.GetComponent<Button> ().enabled = false;
		bUpg2.GetComponent<Button> ().interactable = false;
		bUpg3.SetActive (true);
		//bUpg3.GetComponent<Button> ().enabled = false;
		bUpg3.GetComponent<Button> ().interactable = false;
		UpgradePntCnt.SetActive (true);
		UpgradePntCnt.GetComponent<Text>().text = "Current Points: " + pnt;
		if(tUp1 == true)
		{
			FirstHawkupgradeText.SetActive (true);
		}
		if(tUp2 == true)
		{
			SecondHawkupgradeText.SetActive (true);
		}
		if(tUp3 == true)
		{
			ThirdHawkupgradeText.SetActive (true);
		}
		if(cUp1 == true)
		{
			FirstClubupgradeText.SetActive (true);
		}
		if(cUp2 == true)
		{
			SecondClubupgradeText.SetActive (true);
		}
		if(cUp3 == true)
		{
			ThirdClubupgradeText.SetActive (true);
		}
		if(bUp1 == true)
		{
			FirstBowupgradeText.SetActive (true);
		}
		if(bUp2 == true)
		{
			SecondBowupgradeText.SetActive (true);
		}
		if(bUp3 == true)
		{
			ThirdBowupgradeText.SetActive (true);
		}
	}

	public void openControls()
	{
		isPauseMenu = true;
		pauseMenu.SetActive (false);
		controlsImage.SetActive (true);
		returnFromControls.SetActive (true);
		//pausedText.SetActive (false);
		controlsButton.SetActive (false);
		//returnFromOptions.SetActive (true);
		contButton.SetActive (false);
		quitButton.SetActive (false);
		upgradesButton.SetActive (false);
		optionsButton.SetActive (false);
	}

	public void areYouSure()
	{
		pauseMenu.SetActive (false);
		contButton.SetActive (false);
		cpRestartButton.SetActive (false);
		controlsButton.SetActive (false);
		quitButton.SetActive (false);
		upgradesButton.SetActive (false);
		optionsButton.SetActive (false);
		player.GetComponent<CharaterController>().enabled = false;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		//hide cursor again Eryc 06-09-17
		areYouSureBGD.SetActive (true);
		areYouSureText.SetActive (true);
		yesButton.SetActive (true);
		noButton.SetActive (true);
	}

	public void iAmNotSure()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		contButton.GetComponent<Button> ().interactable = true;
		Time.timeScale = 0.0f;
		isPauseMenu = true;
		pauseMenu.SetActive (true);
		//pausedText.SetActive (true);
		contButton.SetActive (true);
		cpRestartButton.SetActive (true);
		cpRestartButton.GetComponent<Button> ().interactable = false;
		controlsButton.SetActive (true);
		upgradesButton.SetActive (true);
		optionsButton.SetActive (true);
		quitButton.SetActive (true);
		player.GetComponent<CharaterController>().enabled = false;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		if (scene != "Temple") 
		{
			if(cpSystem.GetComponent<CheckpointSystem>().UIcheck == false)
			{
				//cpRestartButton.transform.localScale += new Vector3 (1, 1, 1);
				cpRestartButton.GetComponent<Button> ().interactable = true;
			}
		}
		if (scene == "Temple") 
		{
			//Need to fix this when i get into temple 7/12/17
			cpRestartButton.GetComponent<Button> ().interactable = true;
		}
		areYouSureBGD.SetActive (false);
		areYouSureText.SetActive (false);
		yesButton.SetActive (false);
		noButton.SetActive (false);
	}

	public void quitLevel()
	{
		Time.timeScale = 1.0f;
		//StartCoroutine (killPlayer ());
		//fadeOut.SetActive (true);

		SceneManager.LoadScene ("MainMenu");
		DestroyImmediate(GameObject.FindGameObjectWithTag("PlayerParent"));
		DestroyImmediate(GameObject.FindGameObjectWithTag("EnemyHit"));
		//Destroy(GameObject.Find("Player 1 1"));
		//Destroy(GameObject.Find("EnemyHit"));
	}
	IEnumerator killPlayer()
	{
		yield return new WaitForSeconds (1f);


	}

	public void SubmitSliderSetting()
	{
		
	}

	/*public void autoLockOnOff()
	{
		
		if(autolockonOff == false)
		{
			autolockonOff = true;
		}
		else if(autolockonOff == true)
		{
			autolockonOff = false;
		}
	}*/

	void PointButtonHandler()
	{
		if(pnt >= 1 && pnt <= 9 )
		{
			if(scene != "Temple")
			{
				if(tUp1 == false && GameObject.FindGameObjectWithTag("pickup").GetComponent<AddWeapon>().tHawk == true )
				{
					tUpg1.GetComponent<Button> ().enabled = true;
					tUpg1.GetComponent<Button> ().interactable = true;
				}
				else if(tUp1 == true && tUp2 == false && GameObject.FindGameObjectWithTag("pickup").GetComponent<AddWeapon>().tHawk == true )
				{
					tUpg2.GetComponent<Button> ().enabled = true;
					tUpg2.GetComponent<Button> ().interactable = true;
				}
				else if(tUp1 == true && tUp2 == true && tUp3 == false && GameObject.FindGameObjectWithTag("pickup").GetComponent<AddWeapon>().tHawk == true)
				{
					tUpg3.GetComponent<Button> ().enabled = true;
					tUpg3.GetComponent<Button> ().interactable = true;
				}

				if (cUp1 == false && GameObject.FindGameObjectWithTag("pickup2").GetComponent<AddClub>().club == true) 
				{
					cUpg1.GetComponent<Button> ().enabled = true;
					cUpg1.GetComponent<Button> ().interactable = true;
				}
				else if (cUp1 == true && cUp2 == false && GameObject.FindGameObjectWithTag("pickup2").GetComponent<AddClub>().club == true) 
				{
					cUpg2.GetComponent<Button> ().enabled = true;
					cUpg2.GetComponent<Button> ().interactable = true;
				} 
				else if (cUp1 == true && cUp2 == true && cUp3 == false && GameObject.FindGameObjectWithTag("pickup2").GetComponent<AddClub>().club == true) 
				{
					cUpg3.GetComponent<Button> ().enabled = true;
					cUpg3.GetComponent<Button> ().interactable = true;
				}

				if(bUp1 == false  && GameObject.FindGameObjectWithTag("pickup3").GetComponent<addBow>().bow == true)
				{
					bUpg1.GetComponent<Button> ().enabled = true;
					bUpg1.GetComponent<Button> ().interactable = true;
				}
				else if(bUp1 == true && bUp2 == false && GameObject.FindGameObjectWithTag("pickup3").GetComponent<addBow>().bow == true )
				{
					bUpg2.GetComponent<Button> ().enabled = true;
					bUpg2.GetComponent<Button> ().interactable = true;
				}
				else if(bUp1 == true && bUp2 == true && bUp3 == false && GameObject.FindGameObjectWithTag("pickup3").GetComponent<addBow>().bow == true)
				{
					bUpg3.GetComponent<Button> ().enabled = true;
					bUpg3.GetComponent<Button> ().interactable = true;
				}
			}
			if(scene == "Temple")
			{
				if(tUp1 == false && templeHawk == true )
				{
					tUpg1.GetComponent<Button> ().enabled = true;
					tUpg1.GetComponent<Button> ().interactable = true;
				}
				else if(tUp1 == true && tUp2 == false && templeHawk == true  )
				{
					tUpg2.GetComponent<Button> ().enabled = true;
					tUpg2.GetComponent<Button> ().interactable = true;
				}
				else if(tUp1 == true && tUp2 == true && tUp3 == false && templeHawk == true )
				{
					tUpg3.GetComponent<Button> ().enabled = true;
					tUpg3.GetComponent<Button> ().interactable = true;
				}

				if (cUp1 == false && templeClub == true) 
				{
					cUpg1.GetComponent<Button> ().enabled = true;
					cUpg1.GetComponent<Button> ().interactable = true;
				}
				else if (cUp1 == true && cUp2 == false && templeClub == true) 
				{
					cUpg2.GetComponent<Button> ().enabled = true;
					cUpg2.GetComponent<Button> ().interactable = true;
				} 
				else if (cUp1 == true && cUp2 == true && cUp3 == false && templeClub == true) 
				{
					cUpg3.GetComponent<Button> ().enabled = true;
					cUpg3.GetComponent<Button> ().interactable = true;
				}

				if(bUp1 == false  && templeBow == true)
				{
					bUpg1.GetComponent<Button> ().enabled = true;
					bUpg1.GetComponent<Button> ().interactable = true;
				}
				else if(bUp1 == true && bUp2 == false &&  templeBow == true )
				{
					bUpg2.GetComponent<Button> ().enabled = true;
					bUpg2.GetComponent<Button> ().interactable = true;
				}
				else if(bUp1 == true && bUp2 == true && bUp3 == false &&  templeBow == true)
				{
					bUpg3.GetComponent<Button> ().enabled = true;
					bUpg3.GetComponent<Button> ().interactable = true;
				}
			}
		}
	}
		
	#region Upgrades
	public void tHawkUpgrade1()
	{
		tUp1 = true;

		//player.GetComponent<CharaterController> ().hawkActive = true;
		//GameObject.FindGameObjectWithTag ("weapon").GetComponent<Weapon> ().isLifeSteal = true;
		tUpg1.GetComponent<Button> ().enabled = false;
		tUpg1.GetComponent<Button> ().interactable = false;

		//tUpg1.GetComponent<Button> ().transition. = false;
		//tUpg1.GetComponent<Button> ().GetComponent<Text>().color;
		tUpg1.SetActive (false);
		FirstHawkupgradeText.SetActive (true);
		//tUpg1.GetComponent<Button>().GetComponent<Text> ().text = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void tHawkUpgrade2()
	{
		tUp2 = true;
		tUpg2.GetComponent<Button> ().enabled = false;
		tUpg2.GetComponent<Button> ().interactable = false;
		SecondHawkupgradeText.SetActive (true);
		//tUpg2.SetActive (false);
		//tUpg2.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void tHawkUpgrade3()
	{
		tUp3 = true;
		tUpg3.GetComponent<Button> ().enabled = false;
		tUpg3.GetComponent<Button> ().interactable = false;
		ThirdHawkupgradeText.SetActive (true);
		//tUpg3.SetActive (false);
		//tUpg3.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void clubUpgrade1()
	{
		cUp1 = true;
		cUpg1.GetComponent<Button> ().enabled = false;
		cUpg1.GetComponent<Button> ().interactable = false;
		FirstClubupgradeText.SetActive (true);
		//cUpg1.SetActive (false);
		//cUpg1.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints --;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void clubUpgrade2()
	{
		cUp2 = true;
		cUpg2.GetComponent<Button> ().enabled = false;
		cUpg2.GetComponent<Button> ().interactable = false;
		SecondClubupgradeText.SetActive (true);
		//cUpg2.SetActive (false);
		//cUpg2.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt --;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void clubUpgrade3()
	{
		cUp3 = true;
		cUpg3.GetComponent<Button> ().enabled = false;
		cUpg3.GetComponent<Button> ().interactable = false;
		ThirdClubupgradeText.SetActive (true);
		//cUpg3.SetActive (false);
		//cUpg3.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void bowUpgrade1()
	{
		bUp1 = true;
		bUpg1.GetComponent<Button> ().enabled = false;
		bUpg1.GetComponent<Button> ().interactable = false;
		FirstBowupgradeText.SetActive (true);
		//bUpg1.SetActive (false);
		//bUpg1.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void bowUpgrade2()
	{
		bUp2 = true;
		bUpg2.GetComponent<Button> ().enabled = false;
		bUpg2.GetComponent<Button> ().interactable = false;
		SecondBowupgradeText.SetActive (true);
		//bUpg2.SetActive (false);
		//bUpg2.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	public void bowUpgrade3()
	{
		bUp3 = true;
		bUpg3.GetComponent<Button> ().enabled = false;
		bUpg3.GetComponent<Button> ().interactable = false;
		ThirdBowupgradeText.SetActive (true);
		//bUpg3.SetActive (false);
		//bUpg3.GetComponent<Text> () = "Upgrade Acquired.";
		player.GetComponent<CharaterController> ().upgradePoints--;
		pnt--;
		//retFromUpgrade ();
		openSkills();
		//continueGame ();
	}
	#endregion

	public IEnumerator ContinueWait()
	{
		yield return new WaitForSeconds (1);
	}
}
