using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 10 * Time.deltaTime, 0);
       // gameObject.GetComponent<Material>().color = new Color(gameObject.GetComponent<Material>().color.r, gameObject.GetComponent<Material>().color.g, gameObject.GetComponent<Material>().color.b, Mathf.PingPong(Time.time, 1));
    }
}
