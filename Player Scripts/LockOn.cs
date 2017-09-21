using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
	private GameObject closest;
	private GameObject temp;
    private GameObject player;
	//public Transform[] enemy;
	public bool lockOn = false;
    private 
	//public float closestDistanceSqr = 1000.0f;

	// Use this for initialization
	void Start () 
	{
        player = GameObject.FindGameObjectWithTag("PlayerParent");
        lockOn = false;
	}

	// Update is called once per frame
	void Update () 
	{
        if(player.GetComponent<CharaterController>().aiming == false){
            if (Input.GetButton("Fire2") || Input.GetAxis("xb_L2") > 0.0f && lockOn == false)
            {
                lockOn = true;
            }
            else if (Input.GetButtonUp("Fire2") || Input.GetAxis("xb_L2") > 0.0f && lockOn == true)
            {
                lockOn = false;
            }
        }
		
		Debug.Log ("lockon" + lockOn);
	}

	void LateUpdate()
	{
		if(lockOn == true)
		{
			temp = FindClosestEnemy ();
			if(temp != null)
			{
				//transform.LookAt(temp.transform);
                Vector3 direction = temp.transform.position - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                //transform.LookAt (GetClosestEnemy(enemy));
                if (temp == null)
				{
					temp = FindClosestEnemy ();
					//transform.LookAt (temp.transform);
                    direction = temp.transform.position - transform.position;
                    direction.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                }
			}
		}
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

	public void LockOnOff()
	{

		if(lockOn == false)
		{
			lockOn = true;
		}
		else if(lockOn == true)
		{
			lockOn = false;
		}
	}
}
