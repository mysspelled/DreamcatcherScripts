using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLockOn : MonoBehaviour 
{
	private GameObject closest;
	private GameObject temp;
	//public Transform[] enemy;
	//public float closestDistanceSqr = 1000.0f;

	// Use this for initialization
	void Start () 
	{
		//temp = null;
	}

	// Update is called once per frame
	void Update () 
	{
		
		temp = FindClosestEnemy ();
		if(temp != null)
		{
			transform.LookAt(temp.transform);
			//transform.LookAt (GetClosestEnemy(enemy));
			if(temp.GetComponent<Health>().health <= 0.0f)
			{
				temp = null;
				//temp = FindClosestEnemy ();
				//transform.LookAt (temp.transform);
			}
		}
	}

	void FixedUpdate()
	{
		//temp = null;
	}


	//added 6-8-17 by Daniel
	/*Transform GetClosestEnemy (Transform[] enemies)
	{
		Transform bestTarget = null;
		//float closestDistanceSqr = Mathf.Infinity;
		float closestDistanceSqr = 1000.0f;// this effects the how close an enemy has to get to become locked on
		Vector3 currentPosition = transform.position;
		foreach(Transform potentialTarget in enemies)
		{
			Vector3 directionToTarget = potentialTarget.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}

		return bestTarget;
	}*/

	GameObject FindClosestEnemy()
	{
		GameObject[] enemyTarget = GameObject.FindGameObjectsWithTag("Enemy");
		Vector3 position = transform.position;
		//float distance = Mathf.Infinity;
		float distance = 50.0f;

		foreach (GameObject go in enemyTarget) 
		{
			if(go == this.gameObject)       // Here, you check if the game object is not this game object
			{
				continue;
			}
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) 
			{
				closest = go;
				distance = curDistance;
				//Debug.Log(closest.name);
			}
		}
		return closest;
	}
}
