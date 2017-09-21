using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weryote : MonoBehaviour {

    public List<Transform> pathNodes;
    public GameObject wepHitBox;
    public GameObject hitBox;
    //public GameObject headHitBox;
    public SphereCollider territory;
    public float health = 100f;
    public float turnSmoothing = 1f;
    public int xp = 60;
    public AudioClip roar;
    public AudioClip att1;
    public AudioClip att2;
    public AudioClip walk;
    public AudioClip dead;
    public ParticleSystem deadPart;
    private ParticleSystem dedPartInt;
    



    private GameObject player;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private Animator anim;
    private float attackRate = 3f;
    private float comboRate = 10f;
    private float lungeDist = 15f;
    private float slashDist = 5f;
    private float stayTime = 5f;
    public float timmer = 0f;
    private float attackDist;
    private float distToPlayer;
    private float maxDist = 150;
    public float distToPath;
    public float numOfNodes;
    public int i = 0;
    private bool move;
    private bool firstSight;
    private bool attacking;
    private bool playerInTerritory;
    public bool isHit = false;
    private float stopSound = 0;
    private float comboCounter = 0;
    public float dist = 0;
    



    // Use this for initialization
    void Start () {
    
        player = GameObject.FindGameObjectWithTag("PlayerParent");
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        firstSight = true;
        numOfNodes = pathNodes.Count;
        agent.speed = 8;
        move = false;
        anim.SetBool("IsWalking", true);
        agent.destination = pathNodes[i].position;

    }
	
	// Update is called once per frame
	void Update () {
        //numOfNodes = pathNodes.Count;
        comboCounter += Time.deltaTime;
        health = hitBox.GetComponent<Health>().health;
        
        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        dist = Vector3.Distance(pathNodes[i].localPosition, transform.localPosition);
        distToPath = Vector3.Distance(pathNodes[i].localPosition, transform.localPosition);
        //agent.destination = pathNodes[i].position;
        

        if (health <= 0)
        {
            Death();
            StartCoroutine("StartDeath");      
        }

        if (isHit)
        {

            anim.SetBool("Hit", true);
            StartCoroutine("GetHit");
            isHit = false;

        }
        if (firstSight)
        {
            attackDist = lungeDist;
        }
        else if (!firstSight)
        {
            attackDist = slashDist;
        }

        if (playerInTerritory)
        {
            if (health > 0)
            {
                if (!attacking)
                {
                    TargetPlayer();
                    wepHitBox.SetActive(false);
                    MoveToPlayer();
                }

                Attack();
            }
        }
        else if (!playerInTerritory)
        {

            if (dist < 1f)
            {
                timmer += Time.deltaTime;
                if (timmer > stayTime)
                {
                    i++;
                    agent.isStopped = false;
                    anim.SetBool("IsWalking", true);
                    if (i >= numOfNodes - 1)
                    {
                        i = 0;
                    }

                    agent.destination = pathNodes[i].position;
                    timmer = 0;
                }
                anim.SetBool("IsWalking", false);
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
            }
            else if (dist > 2f)
            {
                //agent.destination = pathNodes[i].position;
                //agent.SetDestination(pathNodes[i].position);
                anim.SetBool("IsWalking", true);
                agent.isStopped = false;
                anim.SetBool("IsWalking", true);
                agent.destination = pathNodes[i].position;
            }
        }
        

    }

    void Patrol()
    {
        if(dist < 1f)
        {
            timmer += Time.deltaTime;
            if (timmer > stayTime)
            {
                i++;
                agent.isStopped = false;
                anim.SetBool("IsWalking", true);
                if (i >= numOfNodes - 1)
                {
                    i = 0;
                }
                
                agent.destination = pathNodes[i].position;
                timmer = 0;
            }
            anim.SetBool("IsWalking", false);
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
        else if (dist > 2f)
        {
            //agent.destination = pathNodes[i].position;
            //agent.SetDestination(pathNodes[i].position);
            anim.SetBool("IsWalking", true);
            agent.isStopped = false;
            anim.SetBool("IsWalking", true);
            agent.destination = pathNodes[i].position;
        }

        //dist = Vector3.Distance(pathNodes[i].position, transform.position);
        /*if (dist<1)
        {
            
            timmer += Time.deltaTime;
            if (timmer > stayTime)
            {
                if (i == numOfNodes - 1)
                {
                    i = 0;
                }
                i++;
                agent.destination = pathNodes[i].localPosition;
                timmer = 0;
                //StartCoroutine("WaitTime");
                move = true;
            }
            Debug.Log("Waiting");
            anim.SetBool("IsWalking", false);
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

        }
        else if(dist >2f)
        {
            //agent.destination = pathNodes[i].position;
            //agent.SetDestination(pathNodes[i].position);
            anim.SetBool("IsWalking", true);
            agent.isStopped = false;
            anim.SetBool("IsWalking", true);
            //agent.destination = pathNodes[i].position;
        }
        //agent.SetDestination(pathNodes[i].position);
        Debug.Log("Moving");*/

    }

    void GotoNextPoint()
    {
        agent.isStopped = false;
        anim.SetBool("IsWalking", true);
        // Returns if no points have been set up
        
        // Set the agent to go to the currently selected destination.
        if (i == numOfNodes - 1)
        {
            i = 0;
        }
        i++;
        agent.destination = pathNodes[i].position;

        
    }

    void MoveToPlayer()
    {
        anim.SetBool("IsSleeping", false);
        agent.SetDestination(player.transform.position);
        
        agent.isStopped = false;
        //agent.stoppingDistance = 5;
        

        if (distToPlayer <= attackDist)
        {
            Attack();
        }
        else if (distToPlayer > attackDist)
        {
            agent.acceleration = 10;
            TargetPlayer();
            anim.SetBool("IsWalking", true);
            anim.SetBool("WillAttack", false);
            anim.SetBool("IsAtacking", false);

        }

    }

    void Attack()
    {

        //comboCounter += Time.deltaTime;
        attackRate += Time.deltaTime;

        if (distToPlayer <= attackDist )
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("Atacking", true);
            if (firstSight)
            {
                
                attacking = true;
                attackRate = 0;
                agent.isStopped = true;
                wepHitBox.SetActive(true);
                anim.SetBool("Attacking", true);
                anim.SetBool("LungeAttack", true);
                PlayAttSound(roar,0);
                StartCoroutine("StopAttack");


            }

            else if (distToPlayer <=slashDist && attackRate >= 3)
            {
                agent.SetDestination(player.transform.position);
                PlayAttSound(att1,3);
                attackRate = 0;
                agent.stoppingDistance = slashDist - 0.5f;
                wepHitBox.SetActive(true);
                anim.SetBool("Attacking", true);
                anim.SetBool("AttackOne", true);
                agent.isStopped = true;
                StartCoroutine("StopRunAttack");
                attacking = true;
                if (comboCounter >= 30)
                {
                    PlayAttSound(att2,0);
                    comboCounter = 0;
                    wepHitBox.SetActive(true);
                    anim.SetBool("AttackTwo", attacking);
                    anim.SetBool("CombOne", attacking);
                    
                }
            }

        }
        else
        {
            attacking = false;
            anim.SetBool("Attacking", false);
        }
       
    }

    void TargetPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSmoothing);
    }
    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(3f);
        agent.isStopped = false;
        attacking = false;
        firstSight = false;
        anim.SetBool("LungeAttack", false);
        anim.SetBool("Atacking", false);
    }
    IEnumerator StopRunAttack()
    {
        
        yield return new WaitForSeconds(3f);
        
        agent.isStopped = false;
        attacking = false;
        anim.SetBool("Atacking", false);
        anim.SetBool("AttackOne", false);
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(Random.Range(5,20));
        if (i == numOfNodes-1)
        {
            i = 0;
        }
        else
        {
            i++;
            agent.destination = pathNodes[i].position;
            //agent.SetDestination(pathNodes[i].position);
            anim.SetBool("IsWalking", true);
            agent.isStopped = false;
            move = false;
            Debug.Log("Go");
        }
        
        
        
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
            Debug.Log("GetInMyBelly");
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
        yield return new WaitForSeconds(2.7f);
        player.GetComponent<CharaterController>().xp += xp;
        Destroy(gameObject);
        
    }
    IEnumerator GetHit()
    {

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
  
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

    private void PlaySound(AudioClip clip)
    {
        stopSound += Time.deltaTime;
        audioSource.clip = clip;
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
