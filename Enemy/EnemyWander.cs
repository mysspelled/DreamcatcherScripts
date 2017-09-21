using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : MonoBehaviour {

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private float distToPlayer;

    public SphereCollider territory;
    public Transform player;
    bool playerInTerritory;

    public GameObject enemy;
    public Animator anim;

    public float turnSmoothing = 2;
    public float speed = 3f;
    public float attackOneRange = 2f;
    public int attackOneDamage = 10;
    public float timeBetweenAttacks;
    private bool IsAttacking;


    // Use this for initialization
    void Start()
    {

        playerInTerritory = false;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        agent.stoppingDistance = attackOneRange;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(player.position, transform.position);
        timer += Time.deltaTime;

        if (playerInTerritory == true)
        {
            if(distToPlayer > attackOneRange)
                MoveToPlayer();
            if (distToPlayer <= attackOneRange)
                Attack();
            
        }
        else if (timer >= wanderTimer && playerInTerritory == false)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
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
    public void MoveToPlayer()
    {
        anim.Play("Walk_2");
        //rotate to look at player
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        //move towards player
        agent.destination = player.position;
        
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
    void Attack()
    {

        anim.Play("Attack");

    }
}

