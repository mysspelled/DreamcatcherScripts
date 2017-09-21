using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGate : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerParent")
        {
            anim.SetBool("open",true);
            
        }
    }
}
