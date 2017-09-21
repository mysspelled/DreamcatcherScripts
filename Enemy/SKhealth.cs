using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SKhealth : MonoBehaviour {

    public float health = 100;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine("GoToCredits");
        }
    }

    public void TakeDamage()
    {
        health -= 10;
    }
    IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Credits");
        Destroy(gameObject);
    }
}
