using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shortcutDestroyCPs : MonoBehaviour 
{
	public GameObject cp2;
	public GameObject cp3;
	public GameObject cp4;
	public GameObject cp5;
	public GameObject sP1;
	public GameObject sP2;
	public GameObject sP3;
	public GameObject sP4;
	public GameObject removeCheckpoint1;
	public GameObject removeCheckpoint2;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent")
		{
			
			cp2.transform.localScale -= new Vector3 (1, 1, 1);
			cp2.transform.position += new Vector3 (1, 1000, 1);
			//cp2.GetComponent<BoxCollider> ().isTrigger = false;
			cp3.transform.localScale -= new Vector3 (1, 1, 1);
			cp3.transform.position += new Vector3 (1, 1000, 1);
			//cp3.GetComponent<BoxCollider> ().isTrigger = false;
			cp4.transform.localScale -= new Vector3 (1, 1, 1);
			cp4.transform.position += new Vector3 (1, 1000, 1);
			//cp4.GetComponent<BoxCollider> ().isTrigger = false;
			cp5.transform.localScale -= new Vector3 (1, 1, 1);
			cp5.transform.position += new Vector3 (1, 1000, 1);
			//cp5.GetComponent<BoxCollider> ().isTrigger = false;
			sP1.tag = "Untagged";
			sP2.tag = "Untagged";
			sP3.tag = "Untagged";
			sP4.tag = "Untagged";
			removeCheckpoint1.SetActive (false);
			removeCheckpoint2.SetActive (false);

		}
	}

}
