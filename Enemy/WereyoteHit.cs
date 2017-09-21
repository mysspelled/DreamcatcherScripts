using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereyoteHit : MonoBehaviour {

    public GameObject wereyote;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            Debug.Log("WereHIT");
            //wereyote.GetComponent<Weryote>().health -= other.gameObject.GetComponent<Weapon>().weaponDamage;
            wereyote.GetComponent<Weryote>().isHit = true;
        }
    }
}
