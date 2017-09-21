using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatteryDeath : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerParent")
        {
            Debug.Log("YaDeadBitch");
            other.GetComponent<CharaterController>().playerHealth = -10;
        }
    }
}
