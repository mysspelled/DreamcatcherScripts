using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationPuzzle : MonoBehaviour 
{
	public Transform ring;
	bool isMoving = false;
	bool isMovingOther = false;
	public AudioClip stoneSound;
	public AudioSource puzzle;
	private float stopSound = 0;
	private bool rotating;


	//public GameObject message;

	void Start()
	{
        puzzle = GetComponent<AudioSource>();
		//targetRotation = transform.rotation;
	}

	void OnTriggerStay(Collider other)
	{
		
		if (other.gameObject.tag == "PlayerParent") 
		{
			//message.SetActive (true);
			if(Input.GetKey(KeyCode.E))
			{
              //  PlaySound(stoneSound);
				isMoving = true;
			}
			else{
               // puzzle.Stop();
				isMoving = false;
				//rotating = false;
				//puzzle.Stop ();
			}
			if(Input.GetKey(KeyCode.Q))
			{
                //PlaySound(stoneSound);
                isMovingOther = true;
			}
			else{
               // puzzle.Stop();
                isMovingOther = false;
				//puzzle.Stop ();
				//rotating = false;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "PlayerParent")
		{
			isMoving = false;
			isMovingOther = false;
			//rotating = true;
		}
	}
		

	void LateUpdate()
	{
		if(isMoving == true)
		{
            PlaySound(stoneSound);
			//PlaySound (stoneSound);
			rotate ();
			//rotating = true;
			//PlaySound (stoneSound);
		}
		if(isMovingOther == true)
		{
            PlaySound(stoneSound);
            //PlaySound (stoneSound);
            ReverseRotate ();
			//rotating = true;
			//PlaySound (stoneSound);
		}
	}

	void rotate()
	{
		ring.Rotate (0, (45 * Time.deltaTime/2), 0);
		//PlaySound (stoneSound);
		//ring.rotation = Quaternion.Slerp()
		//Quaternion newRotation = Quaternion.AngleAxis(Mathf.Infinity,Vector3.up);
		//ring.rotation = Quaternion.Slerp (ring.rotation, newRotation, Time.deltaTime * 1);
	}
	void ReverseRotate()
	{
		ring.Rotate (0, -(45 * Time.deltaTime/2), 0);

		//ring.rotation = Quaternion.Slerp()
		//Quaternion newRotation = Quaternion.AngleAxis(Mathf.Infinity,Vector3.up);
		//ring.rotation = Quaternion.Slerp (ring.rotation, newRotation, Time.deltaTime * 1);
	}

	private void PlaySound(AudioClip clip)
	{
		//stopSound += Time.deltaTime;
		//audioSource.clip = clip;
		/*if (stopSound >= clip.length)
		{
			stopSound = 0;
			//puzzle.Stop();
			puzzle.loop = false;
		}*/
		if (rotating == true && !puzzle.isPlaying)
		{
			puzzle.Play();
			//puzzle.loop = true;
		}
		if(rotating == false)
		{
			puzzle.Stop ();
		}
	}
    
}
