using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOuter : MonoBehaviour {
    public float rotation;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, -rotation * Time.deltaTime);
    }
}
