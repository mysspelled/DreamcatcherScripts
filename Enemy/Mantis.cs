using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mantis : MonoBehaviour {

    public Animator anim;
    public GameObject hitBox;
    public GameObject wepHitBox;
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;
    public ParticleSystem deadPart;
    public ParticleSystem dedPartInt;
    NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTimer = 20f;
    private float timer;
    public Territory territory;

    private AudioSource audioSource;

    private float attackNow = 0f;
    private float comboCounter = 0f;
    private bool target;
    private float meleeAttRate = 10f;
    private float rangeAttRate = 109f;
    private float attackDist = 8f;
    private float health = 100f;
    public float turnSmoothing;
    public bool intro;
    private bool attacking;
    private GameObject player;
    public float distToPlayer;
    private bool comboAttack;
    bool playerInTerritory;
    private bool firstAttack;
    private float stopSound;
    public int xp = 50;
    public bool isHit = false;
    Vector3 newPos;
    float distToPos;

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
            anim.SetBool("IsHit", true);
            StartCoroutine("GetHit");
            isHit = false;
        }

        if (!playerInTerritory)
        {
            anim.SetBool("Agresive", false);
            agent.speed = 4;
            agent.acceleration = 8;
            Wander();
        }

        else if (playerInTerritory)
        {
            agent.SetDestination(player.transform.position);
            comboCounter += Time.deltaTime;
            agent.speed = 9;
            agent.acceleration = 20;
            anim.SetBool("Agressive", true);
            anim.SetBool("Walk", true);
            
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
            anim.SetBool("Walk", true);
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        distToPos = Vector3.Distance(newPos, transform.position);
        if(distToPos < 2)
        {
            anim.SetBool("Walk", false);
        }
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
            audioSource.pitch = Random.Range(0.8f, 1);
            audioSource.Play();
            audioSource.loop = false;
        }


    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Move()
    {
        agent.SetDestination(player.transform.position);
        anim.SetBool("Walk", true);
        TargetPlayer();

        if (distToPlayer <= attackDist)
        {
            Attack();
        }
        else if (distToPlayer > attackDist)
        {
            anim.SetBool("Attack", false);
            

        }

    }

    void Attack()
    {
        anim.SetBool("Agresive", true);
        //comboCounter += Time.deltaTime;
        //attackNow += Time.deltaTime;

        if (distToPlayer <= attackDist)
        {

            //anim.SetBool("Atack", true);
            if (firstAttack)
            {
                attacking = true;
                firstAttack = false;
                attackNow = 0;
                agent.isStopped = true;
                wepHitBox.SetActive(true);
                PlayAttSound(attackSound1, 3f);
                StartCoroutine("StopRunAttack");


            }

            else if (!firstAttack)
            {
                //if(comboCounter >= 20)
                //{
                //    anim.SetBool("Combo", true);
                //    comboCounter = 0;
                //    StartCoroutine("StopComboAttack");
                //}
                PlayAttSound(attackSound2, 3f);
                attacking = true;
                wepHitBox.SetActive(true);
                anim.SetBool("Attack", true);
                anim.SetBool("LungeAttack", false);
                agent.isStopped = true;
                StartCoroutine("StopAttack");
                
            }
            

        }
        else
        {
            attacking = false;
            anim.SetBool("IsAttacking", false);
        }
            



    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        attacking = false;
        anim.SetBool("Attack", false);
        
    }
    IEnumerator StopComboAttack()
    {
        yield return new WaitForSeconds(4f);
        agent.isStopped = false;
        attacking = false;
        anim.SetBool("combo", false);
        anim.SetBool("Attack", false);
    }
    IEnumerator StopRunAttack()
    {
        yield return new WaitForSeconds(3f);
        agent.isStopped = false;
        attacking = false;
        firstAttack = false;
        anim.SetBool("Atack", false);
        anim.SetBool("LungeAttack", false);
    }
    IEnumerator GetHit()
    {

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("IsHit", false);

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
        anim.SetBool("Dead", true);

    }


    IEnumerator StartDeath()
    {
        if(dedPartInt == null)
        {
            dedPartInt = Instantiate(deadPart, transform.position, Quaternion.Euler(new Vector3(-90, 0, 90))) as ParticleSystem;
            PlayAttSound(attackSound3, 0);
        }
        
        yield return new WaitForSeconds(3);
        
        player.GetComponent<CharaterController>().xp += 50;
        Destroy(gameObject);
        
    }
    private void PlaySound(AudioClip clip)
    {
        stopSound += Time.deltaTime;
        //audioSource.clip = clip;
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
