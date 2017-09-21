using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTrigger : MonoBehaviour {

    public GameObject orb;
	public GameObject orb2;
    public GameObject gate;
	public GameObject message;
	public AudioClip pickupSound;
	public AudioSource audio;


	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGERD");
        if (other.gameObject.tag == "Player")
        {
			message.SetActive (true);
			//StartCoroutine (CloseMessage ());
            if (Input.GetKeyDown(KeyCode.E))
            {
				audio.PlayOneShot (pickupSound, 0.7f);
                Debug.Log("OrbFOund");
               // gameObject.SetActive(false);
				orb2.transform.position += new Vector3(1,1000,1);
				orb2.transform.localScale -= new Vector3(1,1,1);
                orb.SetActive(true);
                gate.GetComponent<OpenGate>().orbsFound++;
				message.SetActive (false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("TRIGGERD");
        if (other.gameObject.tag == "Player")
        {
			message.SetActive (true);
			//StartCoroutine (CloseMessage ());
            if (Input.GetKeyDown(KeyCode.E))
            {
				audio.PlayOneShot (pickupSound, 0.7f);
                Debug.Log("OrbFOund");
               // gameObject.SetActive(false);
				orb2.transform.position += new Vector3(1,1000,1);
                orb.SetActive(true);
                gate.GetComponent<OpenGate>().orbsFound++;
				message.SetActive (false);
            }
        }
    }

	IEnumerator CloseMessage()
	{
		yield return new WaitForSeconds (5);
		message.SetActive (false);
	}
	IEnumerator Destroy()
	{
		yield return new WaitForSeconds (1.5f);
		Destroy (orb2);
	}
}
