using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class tightPatrolAI : MonoBehaviour 
{
	public List<Transform> target;
	private float dist;
	public int i = 0;
	private float waitTime;
	private float walkTime;
	public float wanderTimer;
    public float turnSmoothing;
    private GameObject closest;
	private GameObject lastLocation;
	private float randomTimer;
    private float distToPlayer;
    private float attackDist = 2;
    private float lungeRange = 4;
    private bool attacking = false;
    public Animator anim;
    float attackNow = 0;
    float comboCounter = 0;
    public GameObject wepHitBox;
    float combatDist;

    NavMeshAgent agent;
	Transform pathStop;
	Transform self;
	public SphereCollider territory;
	bool playerInTerritory = false;
	public Transform player;
    private bool firstAttack;
    private bool comboAttack;
    private float health = 100;
    private bool firstSight;


    void Start ()
	{
        anim = GetComponent<Animator>();
        firstSight = true;
		//playerInTerritory = false;
		waitTime = 0.0f;
		walkTime = 0.0f;
		agent = GetComponent<NavMeshAgent>();
		self = GetComponent<Transform>();
        attacking = false;
        //agent.SetDestination(target[i].position);
        randomTimer = Random.Range(10.0f,60.0f);
	}

	// Update is called once per frame
	void Update () 
	{
        if (firstSight)
        {
            combatDist = lungeRange;
        }
        else if (!firstSight)
        {
            combatDist = attackDist;
        }
        
        
        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log ("timer2: " + walkTime);
        walkTime += Time.deltaTime;
        health = GetComponent<Health>().health;
        if (health <= 0)
        {
            anim.SetBool("IsDead", true);
            
        }
        if (playerInTerritory == true) 
		{
            if (health > 0)
            {
                if (!attacking)
                {
                    wepHitBox.SetActive(false);
                    MoveToPlayer();
                }
                Attack();
            }
            walkTime = 0.0f;
		} 
		else if (walkTime >= 60.0f && playerInTerritory == false) 
		{
            anim.SetBool("IsWalking:", true);
            waitTime += Time.deltaTime;
			lastLocation = FindClosestLocation ();
			transform.position = lastLocation.transform.position;
			resetTimers();
			//Debug.Log ("timer: " + waitTime);
		}
        
		else
		{
			tightPath ();
		}
	}

	public void MoveToPlayer()
	{
        anim.SetBool("IsWalking:", true);
        //Attack();
        //rotate to look at player
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;
        //move towards player
        agent.destination = player.position;
        lastLocation = FindClosestLocation ();
		//Debug.Log (lastLocation);
		dist = Vector3.Distance(transform.position, lastLocation.transform.position);

        if(distToPlayer <= combatDist)
        {
            Attack();
            
        }
        else if(distToPlayer > combatDist)
        {
            attacking = false;
            anim.SetBool("Attacking", false);
            agent.stoppingDistance = 0;
        }

		if(dist > 60)
		{
			agent.SetDestination(lastLocation.transform.position);
			playerInTerritory = false;
			tightPath ();
		}

	}
    void Attack()
    {

        //comboCounter += Time.deltaTime;
        attackNow += Time.deltaTime;

        if (distToPlayer <= combatDist)
        {

            anim.SetBool("Atacking", true);
            if (firstSight)
            {
                attacking = true;
                attackNow = 0;
                agent.isStopped = true;
                wepHitBox.SetActive(true);
                anim.SetBool("LungeAttack", true);
                StartCoroutine("StopLungeAttack");


            }

            else if (!firstSight)
            {
                anim.SetBool("Atacking", true);
                attackNow = 0;
                attacking = true;
                wepHitBox.SetActive(true);
                anim.SetBool("AttackOne", true);
                agent.isStopped = true;
                StartCoroutine("StopAttack");
            }

        }
        else
        attacking = false;
        anim.SetBool("Attacking", false);



    }
    void OnTriggerEnter(Collider other)
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

	public void tightPath()
	{
		
		dist = Vector3.Distance(target[i].position, transform.position);
		//pathStop = Random.Range (0, 5);

		if(dist < 2)
		{
			i++;
		}
		if (i ==5)
		{
			i = 0;
		}
        
        agent.SetDestination(target[i].position);
	}

	GameObject FindClosestLocation()
	{
        
        GameObject[] enemyTarget = GameObject.FindGameObjectsWithTag("AiLocation");
		Vector3 position = transform.position;
		float distance = Mathf.Infinity;
		//float distance = 50.0f;

		foreach (GameObject go in enemyTarget) 
		{
			if(go == this.gameObject)       // Here, you check if the game object is not this game object
			{
				continue;
			}
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) 
			{
				closest = go;
				distance = curDistance;
				//Debug.Log(closest.name);
			}
		}
		return closest;
	}

	public float _waitTime()
	{
		
		float time = randomTimer;
		//Debug.Log (time);
		return randomTimer;
	}
    

	public void resetTimers()
	{
		if(waitTime>= _waitTime())
		{
			Start ();
			//timer = 0.0f;
			//timer2 = 0.0f;
			tightPath ();
		}
	}
    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(1.5f);
        agent.isStopped = false;
        attacking = false;
        anim.SetBool("AttackOne", false);
        anim.SetBool("Atacking", false);
    }
    IEnumerator StopLungeAttack()
    {
        yield return new WaitForSeconds(6f);
        agent.isStopped = false;
        attacking = false;
        firstSight = false;
        anim.SetBool("Atacking", false);
        anim.SetBool("LungeAttack", false);
    }

}
