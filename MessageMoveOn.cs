using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageMoveOn : MonoBehaviour 
{
	public GameObject prevMessage;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			prevMessage.SetActive (false);
		}
	}
}
