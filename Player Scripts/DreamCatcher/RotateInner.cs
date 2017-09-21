using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInner : MonoBehaviour {
    public float rotation = 10;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }
}
