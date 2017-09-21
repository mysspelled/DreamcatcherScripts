using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharContMoveTEST : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    //public Transform[] enemy;
    //private bool lockOn = false;



    private float hb;
    [Header("UI")]
    public GameObject GameOverText;
    public GameObject Background;
    public GameObject restartButton;
    public GameObject quitButton;
    private GameObject cam;
    public Slider healthbar;
    CharacterController controller;
    //public GameObject[] gOver;

	public bool BowActive = false;
    public GameObject player;

    // Update is called once per frame
    [Header("Movement Values")]
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float airSpeed = 3.0F;
    public float gravity = 20.0F;
    public float sprintSpeed = 16.0f;
    public float turnSmoothing = 15f;
    public float playerHealth = 100.0f;//Added 6-4-17 by Daniel
    public float distanceToGround = 1.0f;
   // public Transform rayOrigin;
 //   public Ray groundRay;

    public Animator anim;

    private bool isAttacking;
    private Vector3 moveDirection = Vector3.zero;



    // Use this for initialization
    void Start()
	{
		Debug.Log ("Weapon1-melee");
        //Cursor.visible = false;
        weapon1.SetActive(true);
        weapon2.SetActive(false);
        weapon3.SetActive(false);


    }

    void Update()
    {
        //Debug.DrawRay(groundRay.origin, groundRay.direction, Color.red);
       // groundRay.origin = rayOrigin.position;
       // groundRay.direction = Vector3.down;
        //RaycastHit grounded;



        //gOver = GameObject.FindGameObjectsWithTag("GameOver");
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchWeapons();
        }
        healthbar.value = playerHealth;

        if (playerHealth <= 0)
        {
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            /*foreach (GameObject item in gOver) 
			{
				if(item == this.gameObject)
				{
					item.gameObject.SetActive (true);
				}
			}*/
            GameOverText.SetActive(true);
            Background.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            //player.GetComponent<CharContMoveTEST>().enabled = false;
            cam.GetComponent<AnotherOrbitCamTest>().enabled = false;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetAxis("xb_X") > 0.0f)
        {
            if (isAttacking == false)
            {
                Attack();
            }
        }

    }


    void Awake()
    {
        //controller = GetComponent<CharacterController>();
        //hide cursor when playing game Eryc 06-09-17
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
		cam.GetComponent<RaycastShoot> ().enabled = false;
    }

    //Edited 5-30-17-Attack no longer loops
    void FixedUpdate()
    {
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");
        //playerHealth--;
        //Debug.Log(playerHealth);
        //Added6-14-17 to handle player death

        //Edited 5-30-17 by Daniel to allow right trigger controller support
        isAttacking = false;
        anim.SetBool("IsAttackTwo", false);

        //transform.LookAt (GetClosestEnemy(enemy));

        if (controller.isGrounded)
        {
            Move(lh, lv);
        }
        else if (!controller.isGrounded)
        {
            AirMove(lh, lv);
        }

        moveDirection.y -= gravity;
        controller.Move(moveDirection * Time.deltaTime);

        if (lh != 0f || lv != 0f)
        {
            Rotating();
        }
    }
    //function to trigger attack animation transition. 5/26/2017
    void Attack()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRun", false);
        anim.SetBool("IsAttackTwo", true);
        isAttacking = true;
        Debug.Log("Attacking");
    }

    //Editted 06-16-2017 by Eryc
    //Moved some of the function around and smoothed out transition from on the ground and off the ground
    void Move(float lh, float lv)
    {

        if (Mathf.Abs(lh) > 0 || Mathf.Abs(lv) > 0)
        {
            anim.SetBool("IsWalking", true);

        }
        else if (Mathf.Abs(lh) == 0 || Mathf.Abs(lv) == 0)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRun", false);
        }

        moveDirection = new Vector3(lh, 0, lv);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.x *= speed;
        moveDirection.z *= speed;


        //transform.Rotate(0, moveDirection.y, 0);
        //Edited 5-30-17- ps4 R1 will activate run
        if (Input.GetButton("Run") || Input.GetButton("ps4_R1"))
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRun", true);
            moveDirection = moveDirection.normalized * sprintSpeed;
        }
        else if (!Input.GetButton("Run") || !Input.GetButton("ps4_R1"))
        {
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsRun", false);
            if (Input.GetButton("Run") || Input.GetButton("ps4_R1"))
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRun", true);
                moveDirection.x = moveDirection.normalized.x * sprintSpeed;
                moveDirection.z = moveDirection.normalized.z * sprintSpeed;
            }

        }
        if (Input.GetButton("Jump") || Input.GetButton("xb_A"))
        {
            moveDirection.y = jumpSpeed;

            anim.SetBool("IsJump", true);
        }
        else
        {
            anim.SetBool("IsJump", false);
        }

    }

    //Added 6-16-17 takes in horizontal and vertical movement and applies it to the player while in the air
    //This is reduced by a small amount because the player cant fly
    void AirMove(float lh, float lv)
    {
        moveDirection.x = lh;
        moveDirection.z = lv;
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.x = moveDirection.normalized.x * airSpeed;
        moveDirection.z = moveDirection.normalized.z * airSpeed;

        //controller.Move(moveDirection * Time.deltaTime);

        if (lh != 0f || lv != 0f)
        {
            Rotating();
        }
    }

    void Rotating()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), Time.fixedDeltaTime * turnSmoothing);
    }

    //added 6-8-17 by Daniel
    /*Transform GetClosestEnemy (Transform[] enemies)
	{
		Transform bestTarget = null;
		//float closestDistanceSqr = Mathf.Infinity;
		float closestDistanceSqr = 100.0f;// this effects the how close an enemy has to get to become locked on
		Vector3 currentPosition = transform.position;
		foreach(Transform potentialTarget in enemies)
		{
			Vector3 directionToTarget = potentialTarget.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}

		return bestTarget;
	}*/

    void SwitchWeapons()
	{ 
        if (weapon1.activeSelf)
        {
			BowActive = false;
			cam.GetComponent<RaycastShoot> ().enabled = true;
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
			Debug.Log ("Weapon2-Bow");
        }
        else if (weapon2.activeSelf)
        {
			BowActive = true;
			cam.GetComponent<RaycastShoot> ().enabled = false;
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
			Debug.Log ("Weapon3-??");
        }
		else if(weapon3.activeSelf)
        {
			BowActive = false;
			cam.GetComponent<RaycastShoot> ().enabled = false;
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
			//Debug.Log ("Weapon1");
		
        }

    }
}