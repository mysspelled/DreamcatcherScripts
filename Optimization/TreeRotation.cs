using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour {



	// Use this for initialization
	void Start () {
        transform.Rotate(0,Random.Range(-360f,360f), 0);
	}
	
	
}
