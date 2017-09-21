using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisHit : MonoBehaviour {

    public GameObject mantis;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
           
            mantis.GetComponent<Mantis>().isHit = true;
        }
    }
}
