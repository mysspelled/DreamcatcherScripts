using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BowWeapon : MonoBehaviour 
{
	public GameObject player;
	public float BowWeaponDamage;
	private float stopSound = 0;
	private AudioSource audioSource;
	public AudioClip hitNoise;
	public bool isBow;
	public bool isBowActive;
	private GameObject inGameCont;
	public bool bUpgrade1;
	public bool bUpgrade2;
	public bool bUpgrade3;
	Scene currentScene;
    public GameObject hitPart;
    private string scene;

	void Start()
	{
		inGameCont = GameObject.FindGameObjectWithTag ("UIController");
		currentScene = SceneManager.GetActiveScene();
		scene = currentScene.name;
		audioSource = gameObject.GetComponent<AudioSource>();
		if (scene == "Temple")
		{
			isBow = true;
		}
	}

	void Update()
	{
        hitPart = GameObject.FindGameObjectWithTag("EnemyHit");
        currentScene = SceneManager.GetActiveScene();
        scene = currentScene.name;
        bUpgrade1 = inGameCont.GetComponent<InGameController> ().bUp1;
		bUpgrade2 = inGameCont.GetComponent<InGameController> ().bUp2;
		bUpgrade3 = inGameCont.GetComponent<InGameController> ().bUp3;

		if (scene != "Temple") 
		{
			if (GameObject.FindGameObjectWithTag ("pickup3").GetComponent<addBow> ().bow == true) {
				isBow = true;
				BowWeaponDamage = 8;
				//isClubActive = false;
				//isBowActive = true;
				isBowActive = true;
			}
			if (GameObject.FindGameObjectWithTag ("PlayerParent").GetComponent<CharaterController> ().BowActive == false) {
				//isBow = true;
				BowWeaponDamage = 8;
				//isClubActive = false;
				//isBowActive = true;
				isBowActive = false;
			}
		}

		if (isBow == true) 
		{
			BowWeaponDamage = 8;
			if (bUpgrade1 == true) 
			{
				BowWeaponDamage = 12;

			}
			if (bUpgrade2 == true) 
			{
				bUpgrade1 = false;
				BowWeaponDamage = 16;

			}
			if (bUpgrade3 == true)
			{
				bUpgrade2 = false;
				BowWeaponDamage = 19;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("Bow attacking for " + BowWeaponDamage);
		if (other.gameObject.tag == "hitBox") 
		{
            hitPart = Instantiate(hitPart, other.transform.position, other.transform.rotation) as GameObject;
            Debug.Log ("HIT");
			//isHawk = player.GetComponent<CharaterController> ().hawkActive;
			other.GetComponent<Health>().health -= BowWeaponDamage;
			PlaySound(hitNoise);
		}
		if(other.gameObject.tag == "Boss")
		{
			Debug.Log("HIT");
			other.GetComponent<Health>().health -= BowWeaponDamage;
			PlaySound(hitNoise);
		}
	}

	private void PlaySound(AudioClip clip)
	{
		stopSound += Time.deltaTime;
		//audioSource.clip = clip;
		if (stopSound >= audioSource.clip.length)
		{
			stopSound = 0;
			audioSource.Stop();
			audioSource.loop = false;
		}
		else if (!audioSource.isPlaying)
		{
			audioSource.Play();
			audioSource.loop = false;
		}
	}
}
