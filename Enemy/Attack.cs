using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour {

    public int attackOneDamage = 10;
    private bool canHit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerParent"&& canHit)
        {

            canHit = false;
            Debug.Log("BitchSlap");
            other.GetComponent<CharaterController>().playerHealth -= attackOneDamage;
            other.GetComponent<CharaterController>().isHit = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerParent")
        {
            canHit = true;
        }
    }

}


