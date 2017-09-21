using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addBow : MonoBehaviour {

	public GameObject player;
	public GameObject display0;
	public GameObject display1;
	public GameObject display2;
	//public GameObject display3;
	public GameObject destroy;
	public GameObject destroy2;
	public GameObject destroy3;
	public int cnt;
	public GameObject wall;
	public GameObject messageTrigger;
	public bool bow = false;
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
			player.GetComponent<CharaterController> ().weaponCnt =  3;
			//GameObject.FindGameObjectWithTag ("pickup2").GetComponent<AddClub> ().club = false;
			bow = true;
			display0.SetActive (true);
			Destroy (wall);
			//Destroy (messageTrigger);
			messageTrigger.transform.position += new Vector3(0,1000,0);
			StartCoroutine (StopDisplay1 ());
			StartCoroutine (StopDisplay2());
			StartCoroutine (StopDisplay3 ());
			//StartCoroutine (StopDisplay4 ());
			destroy.transform.localScale -= new Vector3(1,1,1);
			destroy3.transform.localScale -= new Vector3(1,1,1);
			destroy2.GetComponent<Light> ().enabled = false;
			//cnt ++;
		}
	}

	IEnumerator StopDisplay1()
	{
		yield return new WaitForSeconds (3);
		display0.SetActive (false);
		display1.SetActive (true);
	}
	IEnumerator StopDisplay2()
	{
		yield return new WaitForSeconds (6);
		display1.SetActive (false);
		display2.SetActive (true);
	}
	IEnumerator StopDisplay3()
	{
		yield return new WaitForSeconds (9);
		display2.SetActive (false);
		//display3.SetActive (true);
	}

}
