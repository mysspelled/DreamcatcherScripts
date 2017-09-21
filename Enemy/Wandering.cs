using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{

    public Animator anim;
    public GameObject hitBox;
    public GameObject wepHitBox;
    public ParticleSystem deadPart;
    private ParticleSystem dedPartInt;
    NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTimer = 20f;
    private float timer;
    public Territory territory;

    private AudioSource audioSource;

    private float attackNow = 0f;
    private float comboCounter = 0f;
    private bool target;
    private float meleeAttRate = 5f;
    private float rangeAttRate = 109f;
    private float attackDist = 5f;
    private float health = 100f;
    public float turnSmoothing;
    public bool intro;
    private bool attacking;
    private GameObject player;
    public float distToPlayer;
    private bool comboAttack;
    bool playerInTerritory;
    private bool firstAttack;
    public int xp = 50;
    public bool isHit = false;
    public AudioClip attack1;
    public AudioClip idle;
    public AudioClip attack2;
    public AudioClip dead;
    private float stopSound;

    void Start()
    {
        firstAttack = true;
        player = GameObject.FindGameObjectWithTag("PlayerParent");
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        intro = false;
        turnSmoothing = 0.2f;
        agent.stoppingDistance = attackDist;
        attacking = false;
        agent.isStopped = false;
        timer = wanderTimer;
        playerInTerritory = false;
    }


    void Update()
    {
        health = hitBox.GetComponent<Health>().health;
        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        timer += Time.deltaTime;
        //playerInTerritory = territory;

        if (health <= 0)
        {
            Death();
            StartCoroutine("StartDeath");

        }
        if (isHit)
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Hit", true);
            StartCoroutine("GetHit");
            isHit = false;

        }

        if (!playerInTerritory)
        {
            agent.speed = 4;
            Wander();
        }

        else if (playerInTerritory)
        {
            agent.speed = 9;
            health = hitBox.GetComponent<Health>().health;

            if (health > 0)
            {
                if (!attacking)
                {
                    wepHitBox.SetActive(false);
                    Move();
                }
                
                    Attack();

                
            }
        }

    }

    void TargetPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSmoothing);
    }

    void Wander()
    {
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Move()
    {
        agent.SetDestination(player.transform.position);
        PlaySound(idle);
        TargetPlayer();

        if (distToPlayer <= attackDist)
        {
            Attack();
        }
        else if (distToPlayer > attackDist)
        {
            anim.SetBool("WillAttack", false);
            anim.SetBool("IsAtacking", false);

        }

    }

    void Attack()
    {

        //comboCounter += Time.deltaTime;
        attackNow += Time.deltaTime;

        if (distToPlayer <= attackDist)
        {

            anim.SetBool("IsAtacking", true);
            if (firstAttack)
            {
                PlayAttSound(attack1, 1f);
                attacking = true;
                attackNow = 0;
                agent.isStopped = true;
                wepHitBox.SetActive(true);
                anim.SetBool("RunAttack", true);
                StartCoroutine("StopRunAttack");


            }

            else if (!firstAttack)
            {
                PlayAttSound(attack2, 3f);
                attackNow = 0;
                attacking = true;
                wepHitBox.SetActive(true);
                anim.SetBool("RunAttackTwo", true);
                agent.isStopped = true;
                StartCoroutine("StopAttack");
            }

        }
        else
            attacking = false;
            anim.SetBool("IsAttacking", false);



    }

    IEnumerator GetHit()
    {

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);

    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(2f);
        agent.isStopped = false;
        attacking = false;
        anim.SetBool("RunAttackTwo", false);
        anim.SetBool("IsAtacking", false);
    }
    IEnumerator StopRunAttack()
    {
        yield return new WaitForSeconds(6.7f);
        agent.isStopped = false;
        attacking = false;
        firstAttack = false;
        anim.SetBool("IsAtacking", false);
        anim.SetBool("RunAttack", false);
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
        agent.isStopped = true;
        anim.SetBool("IsDead", true);

    }


    IEnumerator StartDeath()
    {
        if (dedPartInt == null)
        {
            PlaySound(dead);
            dedPartInt = Instantiate(deadPart, transform.position, Quaternion.Euler(new Vector3(-90, 0, 90))) as ParticleSystem;
        }
        yield return new WaitForSeconds(3);
        player.GetComponent<CharaterController>().xp += 50;
        Destroy(gameObject);
        
    }
    private void PlayAttSound(AudioClip clip, float delay)
    {
        stopSound += Time.deltaTime;
        audioSource.clip = clip;
        if (stopSound >= audioSource.clip.length + delay)
        {
            stopSound = 0;
            audioSource.Stop();
            audioSource.loop = false;
        }
        else if (!audioSource.isPlaying && stopSound <= audioSource.clip.length)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1);
            audioSource.Play();
            audioSource.loop = false;
        }


    }
    private void PlaySound(AudioClip clip)
    {
        stopSound += Time.deltaTime;
        audioSource.clip = clip;
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1);
        if (stopSound >= audioSource.clip.length)
        {
            stopSound = 0;
            audioSource.Stop();
            audioSource.loop = false;
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.loop = false;
        }
    }

}
