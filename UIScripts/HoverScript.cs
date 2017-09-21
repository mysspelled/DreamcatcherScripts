using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

	public AudioClip click;
	public AudioClip hover;
	public AudioSource clickSound;

	public void OnPointerEnter(PointerEventData ped)
	{
        clickSound.volume = .50f;
        clickSound.pitch = Random.Range(0.8f, 1f);
		clickSound.PlayOneShot (hover, 0.3f);
	}
	public void OnPointerDown(PointerEventData ped)
	{
        clickSound.volume = .50f;
        clickSound.pitch = Random.Range(0.8f, 1f);
        clickSound.PlayOneShot (click, 0.6f);
	}

}
