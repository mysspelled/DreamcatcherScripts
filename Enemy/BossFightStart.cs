using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightStart : MonoBehaviour {

    ShadowKing startFight;
    float stopSound = 0;
    AudioSource audioSource;
    public AudioClip crushYou;
	public Slider ShadowHealthSlider;
	public GameObject ShadowHealth;
	public GameObject hitBox;
	
	void Start () {
        startFight = GetComponentInParent<ShadowKing>();
        audioSource = GetComponent<AudioSource>();
		//ShadowHealth = GameObject.Find ("ShadowManHealthBar");
		//ShadowHealthSlider.value = ShadowHealth.GetComponent<Slider> ().maxValue;
	}

	void Update()
	{
		ShadowHealthSlider.value = hitBox.GetComponent<Health>().health;
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
			ShadowHealth.SetActive (true);
            stopSound += Time.deltaTime;
            audioSource.clip = crushYou;
            if (stopSound >= audioSource.clip.length)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
            else if (!audioSource.isPlaying)
            {
                
                audioSource.Play();
               audioSource.loop = false;
            }
            startFight.intro = true;
        }
    }

}
