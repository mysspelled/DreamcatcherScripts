using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 100;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("PlayerParent");
	}
	
	
    public void TakeDamage()
    {
        health -= 10;
    }
}
