using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageForInteract : MonoBehaviour 
{

	public GameObject message;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent") 
		{
			message.SetActive (true);
			//StartCoroutine (CloseMessage ());
		}
		else
		{
			message.SetActive (false);
		}
	
	}


	IEnumerator CloseMessage()
	{
		yield return new WaitForSeconds (5);
		message.SetActive (false);
	}
}
