using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TomahawkAudioTrigger : MonoBehaviour {
    Audio voiceLine = new Audio();
    public AudioClip VL;
    public GameObject player;
	public GameObject message;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerParent");
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<AudioSource>().clip = VL;
            player.GetComponent<AudioSource>().pitch = 1;
            player.GetComponent<AudioSource>().volume = 1.5f;
            player.GetComponent<AudioSource>().Play();
			message.SetActive (true);
			StartCoroutine (messageEnd ());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           // Destroy(gameObject);
			gameObject.transform.position += new Vector3(1,1000,1);
        }
    }

	IEnumerator messageEnd()
	{
		yield return new WaitForSeconds (2.7f);
		message.SetActive (false);
	}

}
