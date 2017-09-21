using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShadowKing : MonoBehaviour {

    public Animator anim;
    public GameObject hitBox;
    public GameObject wepHitBox;
    NavMeshAgent agent;

    private AudioSource audioSource;

    public AudioClip crushYou;
    public AudioClip heayae;
    public AudioClip rah;
    public AudioClip nowPowa;
    public AudioClip dead;


    float attackNow = 0;
    float comboCounter = 0;
    private bool target;
    private float meleeAttRate = 10;
    private float rangeAttRate = 109;
    private float attackDist = 10;
    private float health = 200;
    public float turnSmoothing;
    public bool intro;
    private bool attacking;
    private GameObject player;
    public float distToPlayer;
    private bool comboAttack;
    private bool canAtt = false;
    float stopSound = 0;
    float stopAttSound = 0;
	public Slider ShadowHealthSlider;
	public GameObject ShadowHealth;
	public GameObject ShadowHUD;
	public Texture fullHealth;
	public Texture halfHealth;
	public Texture lowHealth;

    

    void Start () {
        
		//ShadowHealth = GameObject.Find ("ShadowManHealthBar");
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        intro = false;
        turnSmoothing = 0.2f;
        agent.stoppingDistance = attackDist;
        attacking = false;
        agent.isStopped = true;
        agent.speed = 8;
    }
	
	
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        health = hitBox.GetComponent<Health>().health;

        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (health == 100) 
		{
			ShadowHUD.GetComponent<RawImage> ().texture = fullHealth;
		}
		if(health <= 50 && health >= 20)
		{
			ShadowHUD.GetComponent<RawImage> ().texture = halfHealth;
		}
		if(health <= 19 && health >=1)
		{
			ShadowHUD.GetComponent<RawImage> ().texture = lowHealth;
		}
        if(health <= 0)
        {
			ShadowHealthSlider.value = -1;
			ShadowHealth.SetActive (false);
			ShadowHUD.SetActive (false);
            anim.Play("dead");
            StartCoroutine("GoToCredits");
            PlaySound(dead);
        }
        
        if (canAtt)
        {
			GameObject.Find ("Player 1 1").GetComponent<CharaterController> ().walkSpeed = 10;
			GameObject.Find ("Player 1 1").GetComponent<CharaterController> ().runSpeed = 20;
            anim.SetBool("IdleGround", true);
            if (health > 0)
            {
                if (!attacking)
                {
                    TargetPlayer();
                    wepHitBox.SetActive(false);
                    Move();
                }
                Attack();
            }
            //intro = false;
        }

        else if (intro == true)
        {
			GameObject.Find ("Player 1 1").GetComponent<CharaterController> ().walkSpeed = 7;
			GameObject.Find ("Player 1 1").GetComponent<CharaterController> ().runSpeed = 7;
            StartCoroutine("IntroOver");
            anim.SetBool("Intro", true);
			ShadowHealth.SetActive (true);
			ShadowHUD.SetActive (true);
            
        }

	}
    IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Credits");
        Destroy(gameObject);
    }
    void TargetPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSmoothing);
    }

    void Move()
    {
        agent.SetDestination(player.transform.position);

        if(distToPlayer <= attackDist)
        {
            Attack();
        }
        else if(distToPlayer > attackDist)
        {
            TargetPlayer();
            anim.SetBool("WillAttack", false);
            anim.SetBool("IsAttackTwo", false);
            anim.SetBool("CombOne", false);
        }
        
    }

    void Attack()
    {
        
        comboCounter += Time.deltaTime;
        attackNow += Time.deltaTime;
        stopAttSound += Time.deltaTime;
        wepHitBox.SetActive(true);

        if (attackNow >= meleeAttRate)
        {
            attackNow = 0;
            attacking = true;
            
            anim.SetBool("WillAttack", attacking);
            
            
            if (!audioSource.isPlaying)
            {
                Debug.Log("HEAYA");
                PlayAttSound(heayae);
            }


            if (comboCounter >= 30)
            {
                comboCounter = 0;
                wepHitBox.SetActive(true);
                anim.SetBool("IsAttackTwo", attacking);
                anim.SetBool("CombOne", attacking);

                PlayAttSound(rah);
                
                //audioSource.clip = Resources.Load("RAH") as AudioClip;
                //audioSource.Play();
            }

            agent.isStopped = true;
            StartCoroutine("StopAttack");
        }


        
    }

    private void PlayAttSound(AudioClip clip)
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
            audioSource.Play(2);
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

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(3);
        TargetPlayer();
        agent.isStopped = false;
        attacking = false;
        stopAttSound = 0;
    }
    IEnumerator IntroOver()
    {
        yield return new WaitForSeconds(13);
		intro = false;
        canAtt = true;
        agent.isStopped = false;

    }

}
