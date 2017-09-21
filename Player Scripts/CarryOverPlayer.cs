using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryOverPlayer : MonoBehaviour 
{

	public GameObject player;
	public int XP;
	public float XPbarMAX;
	public int SP;
	public GameObject w1;
	public GameObject w2;
	public GameObject w3;
	Scene currentScene;
	private string scene;

	void Start()
	{
		currentScene = SceneManager.GetActiveScene();
		scene = currentScene.name;

		if (scene == "Temple")
		{
			player = GameObject.FindGameObjectWithTag("PlayerParent");
			player.GetComponent<CharaterController> ().xp = XP;
			player.GetComponent<CharaterController> ().upgradePoints = SP;
		}
	}
	void Update()
	{
		XP = player.GetComponent<CharaterController> ().xp;
		XPbarMAX = player.GetComponent<CharaterController> ().xpBar.maxValue;
		SP = player.GetComponent<CharaterController> ().upgradePoints;
		saveVariable ();
	}

	void saveVariable()
	{
		DontDestroyOnLoad (this);
	}

}
