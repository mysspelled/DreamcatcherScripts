using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDisapear : MonoBehaviour {

    public GameObject orb1;
    public GameObject orb2;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            orb1.SetActive(false);
            orb2.SetActive(false);
        }
    }
}
