using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMissMeassage : MonoBehaviour 
{

	public GameObject display1;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent")
		{
			
			display1.SetActive (true);
			StartCoroutine (StopDisplay ());
		}
	}

	IEnumerator StopDisplay()
	{
		yield return new WaitForSeconds (2);
		display1.SetActive (false);
	}
}
