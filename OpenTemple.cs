using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTemple : MonoBehaviour {

    public Animator anim;

    void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("Open", true);
        }
    }
}
