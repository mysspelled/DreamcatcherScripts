using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour {

	public GameObject player;
	//public float BowWeaponDamage;
   // public float ClubWeaponDamage;
	public float HawkWeaponDamage;
	public float lifeSteal;
	public bool isLifeSteal;
    private float stopSound = 0;
    private AudioSource audioSource;
    public AudioClip hitNoise;
    public GameObject hitPart;
	public bool isHawk;
	//public bool isClub;
	//public bool isBow;
	public bool isHawkActive;
	public bool isClubActive;
	public bool isBowActive;
	public GameObject inGameCont;
	public bool tUpgrade1;
	public bool tUpgrade2;
	public bool tUpgrade3;
	//public bool cUpgrade1;
	//public bool cUpgrade2;
	//public bool cUpgrade3;
	//public bool bUpgrade1;
	//public bool bUpgrade2;
	//public bool bUpgrade3;
	Scene currentScene;
	private string scene;

	void Awake()
	{

	
	}
    void Start()
    {
        
        currentScene = SceneManager.GetActiveScene();
		scene = currentScene.name;
        audioSource = GetComponent<AudioSource>();
		if (scene == "Temple")
		{
			isHawk = true;
            isHawkActive = true;
			//isBow = true;
			//isClub = true;
		}
	
    }

	void Update()
	{
        hitPart = GameObject.FindGameObjectWithTag("EnemyHit");
        //isClub = player.GetComponent<CharaterController> ().clubActive;
        //isHawk = player.GetComponent<CharaterController> ().BowActive;
        isLifeSteal = inGameCont.GetComponent<InGameController> ().tUp2;
		tUpgrade1 = inGameCont.GetComponent<InGameController> ().tUp1;
		tUpgrade2 = inGameCont.GetComponent<InGameController> ().tUp2;
		tUpgrade3 = inGameCont.GetComponent<InGameController> ().tUp3;
		//cUpgrade1 = inGameCont.GetComponent<InGameController> ().cUp1;
		//cUpgrade2 = inGameCont.GetComponent<InGameController> ().cUp2;
		//cUpgrade3 = inGameCont.GetComponent<InGameController> ().cUp3;
		//bUpgrade1 = inGameCont.GetComponent<InGameController> ().bUp1;
		//bUpgrade2 = inGameCont.GetComponent<InGameController> ().bUp2;
		//bUpgrade3 = inGameCont.GetComponent<InGameController> ().bUp3;

		if (scene != "Temple")
		{
			/*if (GameObject.FindGameObjectWithTag ("pickup").GetComponent<AddWeapon> ().tHawk == true) {
				isHawk = true;
				isHawkActive = true;
			}
			if (GameObject.FindGameObjectWithTag ("PlayerParent").GetComponent<CharaterController> ().hawkActive == false) {
				//isHawk = true;
				isHawkActive = false;
			}*/
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
			}
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
			}*/
		if (player.GetComponent<CharaterController>().hawkActive == true) 
		{
			isHawk = true;
			isHawkActive = true;
		}
		if (player.GetComponent<CharaterController>().hawkActive == false) 
		{
			//isHawk = true;
			isHawkActive = false;
	}
		}



		/*if(player.GetComponent<CharaterController>().hawkActive == true)
		{
			isHawkActive = true;
		}
		if(player.GetComponent<CharaterController>().hawkActive == false)
		{
			isHawkActive = false;
		}
		if(player.GetComponent<CharaterController>().clubActive == true)
		{
			isClubActive = true;
		}
		if(player.GetComponent<CharaterController>().clubActive == false)
		{
			isClubActive = false;
		}
		if(player.GetComponent<CharaterController>().BowActive == true)
		{
			isBowActive = true;
		}
		if(player.GetComponent<CharaterController>().BowActive == false)
		{
			isBowActive = false;
		}*/
		/*if (isClub == true) 
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
		}*/

	}

	void LateUpdate()
	{
		/*if (isBow == true) 
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
		}*/
	}
	void FixedUpdate()
	{

		//Debug.Log ("isHawk:" + isHawk + "Damage:" + HawkWeaponDamage);
		//Debug.Log ("lifeSteal:" + isLifeSteal);
		//Debug.Log ("isHawk:" + isHawk);

	if(player.GetComponent<CharaterController> ().playerHealth > 100)
	{
			player.GetComponent<CharaterController> ().playerHealth = 100;
	}
		if (isHawk == true) {
			HawkWeaponDamage = 10;
			if (tUpgrade1 == true) {
				HawkWeaponDamage = 15;
				if (isLifeSteal == true) {
					lifeSteal = 2.5f;
				}
			}
			if (tUpgrade2 == true) {
				tUpgrade1 = false;
				HawkWeaponDamage = 15;
				if (isLifeSteal == true) {
					lifeSteal = 2.5f;
				}
			}
			if (tUpgrade3 == true) {
				tUpgrade2 = false;
				HawkWeaponDamage = 20;
				if (isLifeSteal == true) {
					lifeSteal = 2.5f;
				}
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
	Debug.Log ("Hawk attacking for " + HawkWeaponDamage);
	if(isHawkActive == true)
	{
        
		if (other.gameObject.tag == "hitBox") 
		{
                hitPart = Instantiate(hitPart, other.transform.position, other.transform.rotation) as GameObject;
                Debug.Log ("HIT");
                //hitPart = Instantiate(hitPart, other.transform.position, other.transform.rotation) as ParticleSystem;
                //isHawk = player.GetComponent<CharaterController> ().hawkActive;
                //other.GetComponent<Health>().health -= ClubWeaponDamage;
                PlaySound(hitNoise);
			//handles lifesteal for tomahawk.
			
				other.GetComponent<Health> ().health -= HawkWeaponDamage;
			if(isLifeSteal == true)
			{
				player.GetComponent<CharaterController> ().playerHealth += lifeSteal;
				//other.GetComponent<Health>().health -= lifeSteal;
				if(player.GetComponent<CharaterController> ().playerHealth > 100)
				{
					player.GetComponent<CharaterController> ().playerHealth = 100;
				}
			}
		}
			/*if (isClubActive == true) 
			{
				other.GetComponent<Health>().health -= ClubWeaponDamage;
			}
			if (isBowActive == true) 
			{
				other.GetComponent<Health>().health -= BowWeaponDamage;
			}*/
        }
        if(other.gameObject.tag == "Boss")
        {
            hitPart = Instantiate(hitPart, other.transform.position, other.transform.rotation) as GameObject;
            Debug.Log("HIT");
			/*if(isHawk == true && inGameCont.GetComponent<InGameController>().tUp2 == true)
			{
				other.GetComponent<Health>().health -= (weaponDamage + 5);
			}
			if(isHawk == true && inGameCont.GetComponent<InGameController>().tUp3 == true)
			{
				other.GetComponent<Health>().health -= (weaponDamage + 10);
			}
			else
			{
				other.GetComponent<Health>().health -= weaponDamage;
			}*/
            //other.GetComponent<Health>().health -= ClubWeaponDamage;
            PlaySound(hitNoise);
			//handles lifesteal for tomahawk.
			if(isHawkActive == true)
			{
				other.GetComponent<Health> ().health -= HawkWeaponDamage;
				if(isLifeSteal == true)
				{
					player.GetComponent<CharaterController> ().playerHealth += lifeSteal;
					//other.GetComponent<Health>().health -= lifeSteal;
					if(player.GetComponent<CharaterController> ().playerHealth > 100)
					{
						player.GetComponent<CharaterController> ().playerHealth = 100;
					}
				}
			}
			/*if (isClubActive == true) 
			{
				other.GetComponent<Health>().health -= ClubWeaponDamage;
			}
			if (isBowActive == true) 
			{
				other.GetComponent<Health>().health -= BowWeaponDamage;
			}*/
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
