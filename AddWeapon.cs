using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddWeapon : MonoBehaviour 
{
	public GameObject player;
	public GameObject display1;
	public GameObject display2;
	//public GameObject display3;
	public GameObject destroy;
	public GameObject destroy2;
	public GameObject destroy3;
	public GameObject wall;
	public GameObject messageTrigger;
	public int cnt;
	public bool tHawk = false;
	public AudioClip pickupSound;
	public AudioSource audio;

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent")
		{
			audio.PlayOneShot (pickupSound, 0.5f);
			player.GetComponent<CharaterController> ().weaponCnt = 1;
			tHawk = true;
			display1.SetActive (true);
			StartCoroutine (StopDisplay1 ());
			StartCoroutine (StopDisplay2());
			//StartCoroutine (StopDisplay3 ());
			destroy.transform.localScale -= new Vector3(1,1,1);
			destroy3.transform.localScale -= new Vector3(1,1,1);
			destroy2.GetComponent<Light> ().enabled = false;
			Destroy (wall);
			Destroy (messageTrigger);
			//cnt ++;
		}
	}

	IEnumerator StopDisplay1()
	{
		yield return new WaitForSeconds (3);
		display1.SetActive (false);
		display2.SetActive (true);
	}
	IEnumerator StopDisplay2()
	{
		yield return new WaitForSeconds (6);
		display2.SetActive (false);
		//display3.SetActive (true);
	
	}
	/*IEnumerator StopDisplay3()
	{
		yield return new WaitForSeconds (9);
		display3.SetActive (false);
		//Destroy (destroy);
	}*/


}
