using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTree : MonoBehaviour
{

    public GameObject player;
    public Animator anim;
    public GameObject hitBox;

    private float distToPlayer;
    private float turnSmoothing = 3;
    private int combo;
    private bool isDead;
    private bool playerInTerritory = false;
    private bool attacking;
    private float attackNow = 0;
    private float comboCounter = 0;
    private float meleeAttRate = 8;
    private float attakDist = 20;
    private float health;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerParent");
        
    }

    // Update is called once per frame
    void Update()
    {
        
        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        health = hitBox.GetComponent<Health>().health;

        if(health <= 0)
        {
            Death();
        }

        if (playerInTerritory)
        {
            comboCounter += Time.deltaTime;
            attackNow += Time.deltaTime;
            
                Attack();
                TargetPlayer();
        }
        else if(!playerInTerritory)
        {
            anim.SetBool("WillAttack", false);
        }


    }

    void Attack()
    {
        anim.SetBool("WillAttack", true);
        if (attackNow >= meleeAttRate)
        {
            anim.SetBool("WillAttack", true);
            attackNow = 0;
            StartCoroutine("NotAttacking");
            
            if (comboCounter >= 15)
            {
                anim.SetInteger("Combo", Random.Range(1, 3));
                StartCoroutine("NotAttacking");
                comboCounter = 0;
                
            }
        }




    }

    void TargetPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSmoothing);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInTerritory = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInTerritory = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInTerritory = false;
        }
    }

    void Death()
    {
        anim.SetBool("WillAttack", false);
        anim.SetInteger("Combo", 0);
        anim.SetBool("IsDead", true);
        StartCoroutine("StartDeath");

    }


    IEnumerator StartDeath()
    {

        yield return new WaitForSeconds(6);
        player.GetComponent<CharaterController>().xp += 50;
        Destroy(gameObject);

    }
    IEnumerator NotAttacking()
    {

        yield return new WaitForSeconds(6);
        anim.SetBool("WillAttack", false);
        attacking = false;

    }
}
