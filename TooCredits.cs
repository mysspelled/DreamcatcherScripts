using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TooCredits : MonoBehaviour {

	void OnTriggerEnter()
    {
        SceneManager.LoadScene("Credits");
    }
}
