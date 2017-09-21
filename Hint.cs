using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject goal = GameObject.FindGameObjectWithTag("Goal");
        float step = 4 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, step);
    }
}
