using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    public float minDist = 1.0f;
    public float maxDist = 10.0f;
    public float smooth = 10.0f;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;

    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDir * maxDist);
        RaycastHit hit;

        if(Physics.Linecast(transform.parent.position,desiredCamPos,out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.8f), minDist, maxDist);
        }
        else
        {
            distance = maxDist;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
