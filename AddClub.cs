using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClub : MonoBehaviour
{
	public GameObject player;
	public GameObject display1;
	//public GameObject display2;
	//public GameObject display3;
	public GameObject destroy;
	public GameObject destroy2;
	public GameObject destroy3;
	public int cnt;
	public GameObject wall;
	public GameObject messageTrigger;
	public bool club = false;
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
			player.GetComponent<CharaterController> ().weaponCnt = 2;
			//GameObject.FindGameObjectWithTag ("pickup").GetComponent<AddWeapon> ().tHawk = false;
			club = true;
			display1.SetActive (true);
			StartCoroutine (StopDisplay1 ());
			Destroy (wall);
			Destroy (messageTrigger);
			//StartCoroutine (StopDisplay2());
			//StartCoroutine (StopDisplay3 ());
			destroy.transform.localScale -= new Vector3(1,1,1);
			destroy.transform.position += new Vector3(1,1000,1);
			destroy3.transform.position += new Vector3(1,1000,1);
			destroy2.GetComponent<Light> ().enabled = false;
			//cnt ++;
		}
	}

	IEnumerator StopDisplay1()
	{
		yield return new WaitForSeconds (5);
		display1.SetActive (false);
		//display2.SetActive (true);
		//Destroy (destroy);
	}
	/*IEnumerator StopDisplay2()
	{
		yield return new WaitForSeconds (10);
		display2.SetActive (false);
		display3.SetActive (true);

	}
	IEnumerator StopDisplay3()
	{
		yield return new WaitForSeconds (15);
		display3.SetActive (false);
		Destroy (destroy);
	}*/


}
