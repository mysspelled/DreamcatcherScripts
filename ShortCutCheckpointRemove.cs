using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutCheckpointRemove : MonoBehaviour
{
	public GameObject cp2;
	public GameObject sP1;
	public GameObject removeCheckpoint1;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent") {

			cp2.transform.localScale -= new Vector3 (1, 1, 1);
			cp2.transform.position += new Vector3 (1, 1000, 1);
			sP1.tag = "Untagged";
			removeCheckpoint1.SetActive (false);
		}
	}
}
