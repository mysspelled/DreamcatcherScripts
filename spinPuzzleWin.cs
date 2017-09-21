using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spinPuzzleWin : MonoBehaviour 
{
	public bool win;
	public bool win2;
	public bool win3;
	private bool messageDisplayed = false;
	public GameObject message;
	public GameObject ring1;
	public GameObject ring2;
	public GameObject ring3;
	public GameObject ringTexture;
	Renderer renderer;
	public Material mat;
	public Light light;
	public Light light2;
	public Light light3;
	public Light light4;
	public GameObject spinButton1;
	public GameObject spinButton2;
	public GameObject spinButton3;
	public GameObject messageTrigger1;
	public GameObject messageTrigger2;
	public GameObject messageTrigger3;
	public GameObject door;
	public GameObject eMessage;
	public AudioClip winSound;
	public AudioSource youWinSource;

	void Awake()
	{
		mat.DisableKeyword("_EMISSION");
	}

	void Start()
	{
		ringTexture.GetComponent<Renderer> ();
		//mat.DisableKeyword("_EMISSION");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "correctLocation")
		{
			Debug.Log ("Correct Location");
			if (other.gameObject == ring1) 
			{
				win = true;
				light2.enabled = false;
			}
			if(other.gameObject == ring2)
			{
				win2 = true;
				light3.enabled = false;
			}
			if(other.gameObject == ring3)
			{
				win3 = true;
				light4.enabled = false;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "correctLocation")
		{
			if (other.gameObject == ring1) 
			{
				win = false;
				light2.enabled = true;
			}
			if(other.gameObject == ring2)
			{
				win2 = false;
				light3.enabled = true;
			}
			if(other.gameObject == ring3)
			{
				win3 = false;
				light4.enabled = true;
			}

		}
	}

	void Update()
	{
		if(win == true && win2 == true && win3 == true)
		{
			Debug.Log ("YOU WIN!!");
			message.SetActive (true);
			youWinSource.PlayOneShot (winSound, 0.03f);
			light.enabled = false;
			mat.EnableKeyword("_EMISSION");
			StartCoroutine (RemoveMessage ());

			Destroy (door);
			spinButton1.GetComponent<rotationPuzzle> ().enabled = false;
			spinButton2.GetComponent<rotationPuzzle> ().enabled = false;
			spinButton3.GetComponent<rotationPuzzle> ().enabled = false;
			/*messageTrigger1.GetComponent<MessageForInteract> ().enabled = false;
			messageTrigger2.GetComponent<MessageForInteract> ().enabled = false;
			messageTrigger3.GetComponent<MessageForInteract> ().enabled = false;*/
			eMessage.SetActive (false);
			messageTrigger1.transform.localScale -= new Vector3 (1, 1, 1);
			messageTrigger2.transform.localScale -= new Vector3 (1, 1, 1);
			messageTrigger3.transform.localScale -= new Vector3 (1, 1, 1);

			//ringTexture.GetComponent<Shader>()._em

		}
	}

	IEnumerator RemoveMessage()
	{
		//message.SetActive (true);
		yield return new WaitForSeconds (5);
		Destroy (message);

	}
}
