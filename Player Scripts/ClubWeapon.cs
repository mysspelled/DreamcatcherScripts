using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClubWeapon : MonoBehaviour {

	public GameObject player;
	public float ClubWeaponDamage;
	private float stopSound = 0;
	private AudioSource audioSource;
	public AudioClip hitNoise;
	public bool isClub;
	//public bool isHawkActive;
	public bool isClubActive;
	//public bool isBowActive;
	public GameObject inGameCont;
	public bool cUpgrade1;
	public bool cUpgrade2;
	public bool cUpgrade3;
	Scene currentScene;
	private string scene;
    public GameObject hitPart;
    //public GameObject thawkWeapon;

    void Start()
	{
       // DontDestroyOnLoad(gameObject);
        currentScene = SceneManager.GetActiveScene();
		scene = currentScene.name;
		audioSource = GetComponent<AudioSource>();
		if (scene == "Temple")
		{
			isClub = true;

		}
	}
	void Update()
	{
        hitPart = GameObject.FindGameObjectWithTag("EnemyHit");
        cUpgrade1 = inGameCont.GetComponent<InGameController> ().cUp1;
		cUpgrade2 = inGameCont.GetComponent<InGameController> ().cUp2;
		cUpgrade3 = inGameCont.GetComponent<InGameController> ().cUp3;

		if (scene != "Temple") 
		{
			/*if (GameObject.FindGameObjectWithTag ("pickup2").GetComponent<AddClub> ().club == true) {
				isClub = true;
				ClubWeaponDamage = 5;
				//isHawkActive = false;
				isClubActive = true;
				//isClubActive = true;
			}
			if (GameObject.FindGameObjectWithTag ("PlayerParent").GetComponent<CharaterController> ().clubActive == false) {
				//isClub = true;
				ClubWeaponDamage = 5;
				//isHawkActive = false;
				isClubActive = false;
				//isClubActive = true;
			}*/
			/*if(thawkWeapon.GetComponent<Weapon>().isClubActive == true)
			{
				isClub = true;
				ClubWeaponDamage = 5;
				//isHawkActive = false;
				isClubActive = true;
				//isClubActive = true;
			}
			if (thawkWeapon.GetComponent<Weapon>().isHawkActive == true) 
			{
				//isClub = true;
				//ClubWeaponDamage = 5;
				//isHawkActive = false;
				isClubActive = false;
				//isClubActive = true;
			}*/
			if (player.GetComponent<CharaterController>().clubActive == true) 
			{
				isClub = true;
				ClubWeaponDamage = 5;
				//isHawkActive = false;
				isClubActive = true;
				//isClubActive = true;
			}
			if (Input.GetKey(KeyCode.R)) 
			{
				//isClub = true;
				//ClubWeaponDamage = 5;
				//isHawkActive = false;
				isClubActive = false;
				//isClubActive = true;
			}
		}

		if (isClub == true) 
		{
			ClubWeaponDamage = 5;
			if (cUpgrade1 == true) 
			{
				ClubWeaponDamage = 8;

			}
			if (cUpgrade2 == true) 
			{
				cUpgrade1 = false;
				ClubWeaponDamage = 12;

			}
			if (cUpgrade3 == true)
			{
				cUpgrade2 = false;
				ClubWeaponDamage = 15;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("Club attacking for " + ClubWeaponDamage);
		if (other.gameObject.tag == "hitBox") 
		{
            hitPart = Instantiate(hitPart, other.transform.position, other.transform.rotation) as GameObject;
            Debug.Log ("HIT");
			//isHawk = player.GetComponent<CharaterController> ().hawkActive;
			other.GetComponent<Health>().health -= ClubWeaponDamage;
			PlaySound(hitNoise);
		}
		if(other.gameObject.tag == "Boss")
		{
			Debug.Log("HIT");
			other.GetComponent<Health>().health -= ClubWeaponDamage;
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
