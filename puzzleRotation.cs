using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleRotation : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("I'm triggered...");
		}
	}
}
		
