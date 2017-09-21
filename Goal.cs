using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public List<Transform> hintLoc;
    private int hintNum = 0;

	// Use this for initialization
	void Start () {
		
	}
	

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            transform.position = hintLoc[hintNum].position;
            hintNum++;
        }
    }
}
