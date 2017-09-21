using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharaterController : MonoBehaviour
{
	private GameObject music;
    public float almostGround = 3f;
    public float distToGround = 2f;
    public float distToWall = 2f;
    public bool BowActive;
    public bool clubActive;
    public bool hawkActive;
    public GameObject player;
    public GameObject obj;
    public Transform aimLook;
    public GameObject hitPart;
    public Animator bow;

    [Header("Weapons")]
    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject fireFlies;
    public int weaponCnt;

    [Header("Stats")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpSpeed = 10f;
    public float airSpeed = 0.07f;
    public float turnSmoothing = 5f;
    public float gravity = 100f;
    public float playerHealth = 100.0f;//Added 6-4-17 by Daniel
    public GameObject firCheckpoint;
	//public GameObject firCheckpointTemple;
    public bool firstCp = false;
	//public bool firstCpTemple = false;

    [Header("UI")]
    public GameObject GameOverText;
    public GameObject Background;
	public GameObject Background2;
    public GameObject restartButton;
    public GameObject restartButton2;
	public GameObject restartButton3;
    public GameObject quitButton;
    public GameObject cam;
    public Slider healthbar;
    public Slider xpBar;
    public GameObject award;
    public GameObject curPointAmount;
    public bool aiming;
    public Texture fullHealth;
    public Texture dying;
    public Texture dead;
    public Texture belowHalfHealth;
    public GameObject HUD;
	public GameObject UpMessage;
	public GameObject UpMessage2;
	public GameObject UpOverlay;
	public GameObject InGameCont;
	public GameObject fadeIn;
	public GameObject reticle;
    private GameObject intFireFlies;
	public GameObject hawkPicture;
	public GameObject clubPicture;
	public GameObject bowPicture;

    float currentSpeed = 0;

    public Animator anim;

    private bool isAttacking;

    LayerMask ignoreLayers;

    private float horizontal;
    private float vertical;
    private bool groundForward;
    private bool clearPath;
    public bool grounded;
    private bool canShoot;
    private bool canAttack;
    public bool isHit = false;
    private float attRate = 0;
    private float comboOne = 0;
    private float comboTwo = 0;
    private float comboThree = 0;
    private bool isDead = false;
    private float groundAngle;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private bool canRegen = false;

    //XP and upgrade points
    public int xp = 0;
	public int overflowXp = 0;
    public int upgradePoints;
    public int upgradePointTotal;
    public AudioClip step;
    public AudioSource walkAudio;
    public AudioSource audioSource;
    public AudioClip cautious;
    public AudioClip hit;
    public AudioClip hawkAtt;
    public AudioClip clubAtt;
    public AudioClip retreat;
    public AudioClip run;
	public GameObject retreatMessage;
	public AudioClip switchWeaponSound;
	public AudioClip upgradePointSound;
    private float retreaTime = 0;
    private GameObject spawn;
    private bool spawnPly = false;
    private bool tempLoad = false;
    private bool hint = true;
    private bool walking = false;

    Scene currentScene;
    private string scene;
    private float stopSound = 0;
	public bool bowDontRun;
	//public GameObject upgradePart;
	public GameObject upgradeLight;

    void Awake()
    {
        //hide cursor when playing game Eryc 06-09-17
        //cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam.GetComponent<RaycastShoot>().enabled = false;
        //weaponCnt = obj.GetComponent<AddWeapon> ().cnt;
		music = GameObject.FindGameObjectWithTag ("Music");
		Destroy (music);
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(hitPart);
    }

    void Start()
    {
        spawnPly = false;
       
        spawnPly = false;
        
        audioSource = gameObject.GetComponent<AudioSource>();
        aiming = false;
        canShoot = true;
        canAttack = true;
        currentScene = SceneManager.GetActiveScene();
        
        rb = gameObject.GetComponent<Rigidbody>();
        ignoreLayers = ~(1 << 3 | 1 << 8);



        if (scene != "Temple")
        {
            weapon0.SetActive(true);
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
			hawkPicture.SetActive (false);
			clubPicture.SetActive (false);
			bowPicture.SetActive (false);
        }

        if (scene == "Temple" || scene == "TheErycZone")
        {
            weaponCnt = 3;
            weapon0.SetActive(false);
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
			hawkActive = true;
			hawkPicture.SetActive (true);
			clubPicture.SetActive (false);
			bowPicture.SetActive (false);

        }

        //Debug.Log("Weapon-empty");
        Time.timeScale = 1.0f;
        //weaponCnt = 0;
        //		audio = GetComponent<AudioSource>();
    }

    void Update()
    {
		Debug.Log ("bowDont run: " + bowDontRun);
        hitPart = GameObject.FindGameObjectWithTag("PlayerHit");
		//upgradePart = GameObject.FindGameObjectWithTag("UpgradePart");
        fireFlies = GameObject.FindGameObjectWithTag("Hint");
        currentScene = SceneManager.GetActiveScene();
        scene = currentScene.name;
        if (Input.GetKeyDown(KeyCode.T) && hint)
        {
            fireFlies = Instantiate(fireFlies, transform.position + new Vector3(0,2.5f,0), transform.rotation) as GameObject;
            StartCoroutine("Hint");
            hint = false;
        }
        

        if (scene == "Temple" && !tempLoad)
        {
            spawn = GameObject.FindGameObjectWithTag("SpawnPoint");
            player.transform.position = spawn.transform.position;
            tempLoad = true;
        }

        if (!spawnPly)
        {
            spawn = GameObject.FindGameObjectWithTag("SpawnPoint");
            player.transform.position = spawn.transform.position;
            spawnPly = true;
        }
        

        retreaTime += Time.deltaTime;
        Debug.Log(weaponCnt);
        //firstCp = firCheckpoint.GetComponent<CheckpointSystem>().isFirst;
        if (scene != "TheErycZone" || scene != "Temple" || scene != "LoadingScene") 
        {
			
			//firstCp = firCheckpoint.GetComponent<CheckpointSystem> ().isFirst;
        }
		if(scene == "Temple")
		{
			firstCp = false;
			firCheckpoint = null;
		}
		if(scene == "MainMenu" || scene == "Credits")
		{
			Destroy (gameObject);
			Destroy (hitPart);
		}
        //weaponCnt = obj.GetComponent<AddWeapon>().cnt;
        grounded = IsGrounded();
        healthbar.value = playerHealth;
        xpBar.value = xp;
        if (playerHealth > 0)
        {
            isDead = false;
            anim.SetBool("Dead", false);
        }
        else if (playerHealth <= 0)
        {
            isDead = true;
            anim.SetBool("Dead", true);
        }
        if (playerHealth < 100)
        {
            RgainHealth();
        }
        if (playerHealth <= 30 && retreaTime <= retreat.length)
        {
			if(playerHealth > 0)
			{
				audioSource.pitch = 1;
				audioSource.volume = 1;
				PlaySound(retreat);
				retreatMessage.SetActive (true);
				StartCoroutine (messageEnd ());
			}
          
        }
        else if(playerHealth > 30 || retreaTime > 10)
        {
            retreaTime = 0;
        }
        if (isHit)
        {
            audioSource.pitch = 1;
            audioSource.volume = 1;
            PlaySound(hit);
            anim.SetBool("IsHit", true);
            StartCoroutine("GetHit");
            isHit = false;

        }
        if (BowActive)
        {
            
            anim.SetBool("BowOut", true);
            if (aiming)
            {
				//audioSource.PlayOneShot (bowDrawSound, 1f);
                bow.SetBool("Aiming", true);
                anim.SetBool("BowDrawn", true);
                transform.rotation = Quaternion.Euler(new Vector3(0, Camera.main.transform.eulerAngles.y + 25, 0));
				bowDontRun = true;
			
                //transform.LookAt(aimLook.position);
            }
            else
            {
                bow.SetBool("Aiming", false);
                anim.SetBool("BowDrawn", false);
				bowDontRun = false;
            }
        }
        else if (!BowActive)
        {
            anim.SetBool("BowOut", false);
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("StopAttack");
                StartCoroutine("StopAttack");
                isAttacking = false;
                canAttack = false;
            }


        }

        //6-26-17 Handles starting with no weapon and adds them to the weapon switch.
        if (weaponCnt == 0)
        {
            weapon0.SetActive(true);
            hawkActive = false;
			hawkPicture.SetActive (false);
			clubPicture.SetActive (false);
			bowPicture.SetActive (false);
            //Debug.Log("Weapon-empty");
        }
        else if (weaponCnt == 1)
        {
            weapon1.SetActive(true);
			BowActive = false;
			clubActive = false;
			hawkActive = true;
			hawkPicture.SetActive (true);
			clubPicture.SetActive (false);
			bowPicture.SetActive (false);
            //Debug.Log("Weapon-tomahawk");
        }
        else if (weaponCnt == 2)
        {
            //hawkActive = false;
            //clubActive = true;
			if (bowDontRun == false) 
			{
				if (Input.GetKeyDown(KeyCode.R) && !aiming)
				{

					Switch2Weapons();
				}
			}
          
        }
        else if (weaponCnt == 3)
        {
            //BowActive = true;
			if(bowDontRun == false)
			{
				if (Input.GetKeyDown(KeyCode.R) && !aiming)
				{

					Switch3Weapons();
				}
			}
        }
        if (weaponCnt > 3)
        {
            weaponCnt = 3;
        }

        if (playerHealth >= 50)
        {
            HUD.GetComponent<RawImage>().texture = fullHealth;
        }
        if (playerHealth <= 49 && playerHealth >= 30)
        {
			Background.SetActive(false);
			Background2.SetActive (false);

            HUD.GetComponent<RawImage>().texture = belowHalfHealth;
        }
        if (playerHealth <= 29 && playerHealth >= 1)
        {
			Background.SetActive(true);
			Background2.SetActive (false);
			HUD.GetComponent<RawImage>().texture = dying;
        }
		if (playerHealth <= 0 )//&& firstCp == false)
        {
			if(scene != "Temple")
			{
				StartCoroutine("DeadMessage");
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				HUD.GetComponent<RawImage>().texture = dead;
				GameOverText.SetActive(true);
				//Background.SetActive(true);
				Background.SetActive(false);
				Background2.SetActive(true);
				Background2.GetComponent<Image> ().CrossFadeAlpha (200,1700,false);
				restartButton.SetActive(true);
				quitButton.SetActive(true);
				cam.GetComponent<AnotherOrbitCamTest>().enabled = false;
				//StartCoroutine (StopDeathFadeIn ());
			}
			if(scene == "Temple")
			{
				StartCoroutine("DeadMessage");
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				HUD.GetComponent<RawImage>().texture = dead;
				GameOverText.SetActive(true);
				//Background.SetActive(true);
				Background.SetActive(false);
				Background2.GetComponent<Image> ().CrossFadeAlpha (200,1700,false);
				restartButton3.SetActive(true);
				quitButton.SetActive(true);
				cam.GetComponent<AnotherOrbitCamTest>().enabled = false;
				//StartCoroutine (StopDeathFadeIn ());
			}
          

        }
        grounded = IsGrounded();
		//Cheats disabled by Daniel Adams 7-17-17
       /* if (Input.GetKeyDown(KeyCode.N))
        {
            upgradePoints++;
			//upgradePointTotal++;
            if (upgradePoints > 9)
            {
                upgradePoints = 9;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerHealth -= 10;
        }
		if (Input.GetKeyDown(KeyCode.V))
		{
			playerHealth += 10;
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			xp += 50;
		}*/
    }
	IEnumerator StopDeathFadeIn()
	{
		yield return new WaitForSeconds (10f);
		Time.timeScale = 0f;
	}
	 
	IEnumerator messageEnd()
	{
		yield return new WaitForSeconds (2.7f);
		retreatMessage.SetActive (false);
	}

    IEnumerator DeadMessage()
    {
        yield return new WaitForSeconds(2);
     
    }

    IEnumerator Hint()
    {
        
        yield return new WaitForSeconds(20);
        hint = true;

    }

    void LateUpdate()
    {
        grounded = IsGrounded();
        /////////////////////////
        //added 6-23-17 by daniel to handle xp and points
        curPointAmount.GetComponent<Text>().text = "Current Points: " + upgradePoints;
        xpHandler();
        //Debug.Log ("Points: " + upgradePoints);
        //Debug.Log ("Current XP: " + xp);
    }

   



    void FixedUpdate()
    {
		if(bowDontRun == false)
		{
				Move();
		}
		if(bowDontRun == true)
		{
			anim.SetBool("IsWalk", false);
			anim.SetBool("IsRun", false);
		}
    }

    bool IsGrounded()
    {
        bool r = false;
        Vector3 origin = transform.position + (Vector3.up * 0.1f);

        RaycastHit hit = new RaycastHit();
        bool isHit = false;
        hit = FindGround(origin, ref hit, ref isHit);
        if (!isHit)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 newOrigin = origin;
                switch (i)
                {
                    case 0:
                        newOrigin += Vector3.forward / 3;
                        Debug.DrawRay(newOrigin, -Vector3.up * 0.1f, Color.red);
                        break;
                    case 1:
                        newOrigin -= Vector3.forward / 3;
                        Debug.DrawRay(newOrigin, -Vector3.up * 0.1f, Color.red);
                        break;
                    case 2:
                        newOrigin -= Vector3.right / 3;
                        Debug.DrawRay(newOrigin, -Vector3.up * 0.1f, Color.red);
                        break;
                    case 3:
                        newOrigin += Vector3.right / 3;
                        Debug.DrawRay(newOrigin, -Vector3.up * 0.1f, Color.red);
                        break;
                }
                FindGround(newOrigin, ref hit, ref isHit);
                if (isHit == true)
                    break;
            }
        }

        r = isHit;

        return r;
    }

    RaycastHit FindGround(Vector3 origin, ref RaycastHit hit, ref bool isHit)
    {
        Debug.DrawRay(origin, -Vector3.up * 0.1f, Color.red);
        if (Physics.Raycast(origin, -Vector3.up, out hit, distToGround, ignoreLayers))
        {
            isHit = true;
        }
        return hit;
    }

    void IsClear(Vector3 origin, Vector3 direction, float distance, ref bool isHit)
    {
        RaycastHit hit;
        Debug.DrawRay(origin, direction * distance, Color.yellow);
        if (Physics.Raycast(origin, direction, out hit, distance, ignoreLayers))
        {
            isHit = true;
        }
        else
        {
            isHit = false;
        }

        if (clearPath)
        {
            Vector3 incomingVect = hit.point - origin;
            Vector3 reflectVect = Vector3.Reflect(incomingVect, hit.normal);
            float angle = Vector3.Angle(incomingVect, reflectVect);
            if (angle < 70)
            {
                isHit = false;

            }
        }

        clearPath = isHit;

        if (groundForward)
        {
            if (horizontal != 0 || vertical != 0)
            {
                Vector3 p1 = transform.position;
                Vector3 p2 = hit.point;
                float diffY = p1.y - p2.y;
                groundAngle = diffY;
            }


            if (Mathf.Abs(groundAngle) > 0.3f)
            {
                rb.drag = 2;

            }


        }

    }

    void Move()
    {


        //get input values
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");
        horizontal = lh;
        vertical = lv;

        //update the players forces with input
        moveDirection.Set(lh, 0f, lv);
        //with the camera direction added as well
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;


        if (grounded)//if the player is on the ground
        {
			cam.GetComponent<RaycastShoot> ().enabled = true;
			//cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
            rb.mass = 10;
            rb.drag = 5;
            anim.SetBool("InAir", false);
            anim.SetBool("IsLand", true);
            Vector3 origin = transform.position + new Vector3(0, 2, 0);
            origin += Vector3.up * 0.75f;
            IsClear(origin, transform.forward, distToWall, ref clearPath);

            if (Input.GetMouseButton(0) || Input.GetAxis("xb_X") > 0.2f)
            {
                Debug.Log("Attacking");
                weapon1.GetComponent<Collider>().enabled = true;
                weapon2.GetComponent<Collider>().enabled = true;
                isAttacking = true;
                Attack();
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetAxis("xb_X") > 0.2f && canAttack)
            {
                Debug.Log("StopAttack");
                StartCoroutine("StopAttack");
                weapon1.GetComponent<Collider>().enabled = false;
                weapon2.GetComponent<Collider>().enabled = false;
                isAttacking = false;
                canAttack = false;
            }

            if (!clearPath)
            {
                origin += transform.forward * 1f;
                IsClear(origin, -Vector3.up, distToWall * 3, ref clearPath);
            }
            else if (Vector3.Angle(transform.forward, moveDirection) > 30)
            {
                clearPath = false;
            }
            if (clearPath)
            {
                if (Mathf.Abs(lv) + Mathf.Abs(lh) > 0)
                {

                    anim.SetBool("IsWalk", true);
                    
                    if (!walkAudio.isPlaying && walking)
                    {
                        walkAudio.Play();
                        walkAudio.pitch = Random.Range(0.8f, 1f);
                        walkAudio.volume = Random.Range(0.8f, 1f);
                    }
                    //StartCoroutine(WalkEffect());
                }
                currentSpeed = walkSpeed;

				if (Input.GetButton("Run") || Input.GetButton("ps4_R1"))
				{
                    walking = false;
                    walkAudio.clip = run;
                    if (!walkAudio.isPlaying && !walking)
                    {
                        walkAudio.Play();
                        walkAudio.pitch = Random.Range(0.8f, 1f);
                        walkAudio.volume = Random.Range(0.8f, 1f);
                    }
                    anim.SetBool("IsRun", true);
					currentSpeed = runSpeed;

				}
				else if (!Input.GetButton("Run") || !Input.GetButton("ps4_R1") && !isAttacking)
				{
                    walking = true;
                    walkAudio.clip = step;
                    anim.SetBool("IsRun", false);
				}
            }

            if (Input.GetButton("Jump") || Input.GetButton("xb_A"))
            {
                anim.SetBool("IsJump", true);
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            }
            if (Mathf.Abs(lv) + Mathf.Abs(lh) > 0)
            {
                


                if (!walkAudio.isPlaying && walking)
                {
                    walkAudio.Play();
                }

            }
        }
        //Start decressing drag so the player doesnt float
        //And lower the the speed of the player
        else if (!grounded)//if the player is off the ground
        {
			bowDontRun = false;
			cam.GetComponent<RaycastShoot> ().enabled = false;
			anim.SetBool("BowDrawn", false);
			//reticle.SetActive (false);
			//BowActive = false;
            rb.drag = 0.2f;
            anim.SetBool("IsLand", false);
            anim.SetBool("InAir", true);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun", false);
            currentSpeed = airSpeed;
            walkAudio.Stop ();
        }
        if (Mathf.Abs(lv) + Mathf.Abs(lh) == 0)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun", false);
            walkAudio.Stop();
        }



        //Add all forces to the player
        if (isDead)
        {
            rb.AddForce(moveDirection * 0);
        }
        else if (!isAttacking)
        {
            rb.AddForce(moveDirection.normalized * currentSpeed / Time.deltaTime);
        }
        else if (isAttacking && !BowActive)
        {
            rb.AddForce(moveDirection.normalized * 4 / Time.deltaTime);
        }



        //rotate the player in th correct directions
        if (lh != 0f || lv != 0f && GetComponent<LockOn>().lockOn == false)
        {
            Rotating();
        }
    }



    void Rotating()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), Time.fixedDeltaTime * turnSmoothing);
    }

    void Switch2Weapons()
    {
        if (weapon1.activeSelf)
        {
			audioSource.PlayOneShot (switchWeaponSound, 0.5f);
            weapon1.GetComponent<Weapon>().isClubActive = true;
            weapon1.GetComponent<Weapon>().isHawkActive = false;
            BowActive = false;
            clubActive = true;
            hawkActive = false;
            cam.GetComponent<RaycastShoot>().enabled = false;
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
			hawkPicture.SetActive (false);
			clubPicture.SetActive (true);
			bowPicture.SetActive (false);
            Debug.Log("Weapon2-club");
        }
        else if (weapon2.activeSelf)
        {
			audioSource.PlayOneShot (switchWeaponSound, 0.5f);
            weapon1.GetComponent<Weapon>().isClubActive = false;
            weapon1.GetComponent<Weapon>().isHawkActive = true;
            BowActive = false;
            clubActive = false;
            hawkActive = true;
            cam.GetComponent<RaycastShoot>().enabled = false;
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
			hawkPicture.SetActive (true);
			clubPicture.SetActive (false);
			bowPicture.SetActive (false);
            Debug.Log("Weapon3-tomahawk");
        }
    }
    void Switch3Weapons()
    {
        if (weapon1.activeSelf)
        {
			audioSource.PlayOneShot (switchWeaponSound, 0.5f);
            weapon1.GetComponent<Weapon>().isClubActive = true;
            weapon1.GetComponent<Weapon>().isHawkActive = false;
            weapon1.GetComponent<Weapon>().isBowActive = false;
            BowActive = false;
            clubActive = true;
            hawkActive = false;
            cam.GetComponent<RaycastShoot>().enabled = false;
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
			hawkPicture.SetActive (false);
			clubPicture.SetActive (true);
			bowPicture.SetActive (false);
            Debug.Log("Weapon2-club");
        }
        else if (weapon2.activeSelf)
        {
			audioSource.PlayOneShot (switchWeaponSound, 0.5f);
            weapon1.GetComponent<Weapon>().isClubActive = false;
            weapon1.GetComponent<Weapon>().isHawkActive = false;
            weapon1.GetComponent<Weapon>().isBowActive = true;
            BowActive = true;
            clubActive = false;
            hawkActive = false;
            cam.GetComponent<RaycastShoot>().enabled = true;
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
			hawkPicture.SetActive (false);
			clubPicture.SetActive (false);
			bowPicture.SetActive (true);
            Debug.Log("Weapon3-bow");
        }
        else if (weapon3.activeSelf)
        {
			audioSource.PlayOneShot (switchWeaponSound, 0.5f);
            weapon1.GetComponent<Weapon>().isClubActive = false;
            weapon1.GetComponent<Weapon>().isHawkActive = true;
            weapon1.GetComponent<Weapon>().isBowActive = false;
            BowActive = false;
            clubActive = false;
            hawkActive = true;
            cam.GetComponent<RaycastShoot>().enabled = false;
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
			hawkPicture.SetActive (true);
			clubPicture.SetActive (false);
			bowPicture.SetActive (false);
            Debug.Log("Weapon1-tomahawk");

        }
    }
    void Attack()
    {
        if (BowActive)
        {
            if (aiming && canShoot)
            {
				//audioSource.PlayOneShot (bowDrawSound, 1f);
				//PlaySound(bowDrawSound);
                anim.SetBool("BowShot", true);
                canShoot = false;
                StartCoroutine("ShootOver");
            }

        }
        else if (!BowActive)
        {
            if (Input.GetMouseButton(0) || Input.GetAxis("xb_X") < 0.2f)
            {

                anim.SetBool("IsAttack", true);
                if (clubActive)
                {
                    PlayAttSound(clubAtt, 0.5f);
                    anim.SetBool("ClubAttack", true);
                    anim.SetBool("HawkAttack", false);
                }
                if (hawkActive)
                {
                    PlayAttSound(hawkAtt, 0);
                    anim.SetBool("HawkAttack", true);
                    anim.SetBool("ClubAttack", false);
                }

            }



        }

    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(1f);
        weapon1.GetComponent<Collider>().enabled = false;
        weapon2.GetComponent<Collider>().enabled = false;
        canAttack = true;
        anim.SetBool("IsAttack", false);
    }

    IEnumerator ShootOver()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("BowShot", false);
        canShoot = true;

    }

    void xpHandler()
    {
        if (upgradePointTotal == 0 && xp >= 50)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position,catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			//upgradeLight.transform.position = new Vector3(,,1);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 50);
            StartCoroutine(awardPoint());
			StartCoroutine(pointMessageAway());
			//bowDontRun = true;
			//anim.SetBool("IsWalk", false);
			//anim.SetBool("IsRun", false);
        }
        else if (upgradePointTotal == 1 && xp >= 100)
        {
			//upgradePart = Instantiate (upgradePart,catcher.transform.position, catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 100);
            StartCoroutine(awardPoint());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 2 && xp >= 200)
        {
			//upgradePart = Instantiate (upgradePart,catcher.transform.position, catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 200);
            StartCoroutine(awardPoint());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 3 && xp >= 400)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position, catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 400);
            StartCoroutine(awardPoint2());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 4 && xp >= 500)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position, catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 500);
            StartCoroutine(awardPoint2());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 5 && xp >= 600)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position,catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 600);
            StartCoroutine(awardPoint2());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 6 && xp >= 700)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position, catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 700);
            StartCoroutine(awardPoint2());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 7 && xp >= 800)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position,catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 800);
            StartCoroutine(awardPoint2());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 8 && xp >= 900)
        {
			//upgradePart = Instantiate (upgradePart, catcher.transform.position,catcher.transform.rotation) as GameObject;
			upgradeLight.SetActive (true);
			//upgradeLight.transform.localScale = new Vector3(1.5f,2.5f,1.5f);
			audioSource.PlayOneShot (upgradePointSound, 0.8f);
            upgradePoints += 1;
            upgradePointTotal += 1;
            award.SetActive(true);
            curPointAmount.SetActive(true);
			overflowXp = (xp - 900);
            StartCoroutine(awardPoint2());
			StartCoroutine(pointMessageAway());
        }
        else if (upgradePointTotal == 9)
        {
            curPointAmount.GetComponent<Text>().text = "Current Points: 9 (MAX)";
        }
    }
	IEnumerator pointMessageAway()
	{
		yield return new WaitForSeconds(1f);
	
		award.SetActive(false);
		curPointAmount.SetActive(false);

		StartCoroutine (pointMessageAway2 ());

	}
	IEnumerator pointMessageAway2()
	{
		yield return new WaitForSeconds(.2f);
		//upgradeLight.transform.position -= new Vector3(1,1,1);
		//upgradeLight.transform.position = new Vector3(0,0,0);
		upgradeLight.SetActive (false);
	}
	IEnumerator awardPoint()
	{
		yield return new WaitForSeconds(0.5f);
		//upgradeLight.transform.localScale = new Vector3(0,0,0);
		xp = 0;
		xp =(xp + overflowXp);
		overflowXp = 0;
		xpBar.maxValue = (xpBar.maxValue * 2);
		if(upgradePointTotal == 1)
		{
			StartCoroutine (ExplainUpgrade ());
		}
	}
	IEnumerator awardPoint2()
	{
		yield return new WaitForSeconds(0.5f);
		//upgradeLight.transform.localScale = new Vector3(0,0,0);
		//award.SetActive(false);
		xp = 0;
		xp =(xp + overflowXp);
		overflowXp = 0;
		xpBar.maxValue = (xpBar.maxValue += 100);
		//curPointAmount.SetActive(false);
	}
	IEnumerator ExplainUpgrade()
	{
		yield return new WaitForSeconds (0);
		//Time.timeScale = 0.0f;
		walkSpeed = 4;
		runSpeed = 4;
		UpMessage.SetActive(true);
		UpOverlay.SetActive (true);
		StartCoroutine(ExplainUpgrade2());
		bowDontRun = true;
	}
	IEnumerator ExplainUpgrade2()
	{
		yield return new WaitForSeconds (3);
		UpMessage.SetActive(false);
		UpMessage2.SetActive(true);
		StartCoroutine(ExplainUpgrade3());
	}
	IEnumerator ExplainUpgrade3()
	{
		yield return new WaitForSeconds (3);
		bowDontRun = false;
		UpMessage2.SetActive(false);
		UpOverlay.SetActive (false);
		Time.timeScale = 0.0f;
		//player.GetComponent<CharaterController>().enabled = false;
		cam.GetComponent<AnotherOrbitCamTest> ().enabled = false;
		InGameCont.GetComponent<InGameController> ().openSkills ();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		walkSpeed = 10;
		runSpeed = 20;
	}


  
    private IEnumerator WalkEffect()
    {
        // Play the shooting sound effect
        walkAudio.Play();
  
        //Wait for .07 seconds
        yield return new WaitForSeconds(5);
    }
    private IEnumerator RegenHealth()
    {
        RgainHealth();
        yield return new WaitForSeconds(15);
        canRegen = true;
    }

    IEnumerator GetHit()
    {
        hitPart = Instantiate(hitPart, transform.position + new Vector3(0,2.5f,0), transform.rotation) as GameObject;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("IsHit", false);
    }

    void RgainHealth()
    {
        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
        playerHealth += Time.deltaTime / 3;
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
	public void restartLevel()
	{
		Time.timeScale = 1.0f;
		//int scene = SceneManager.GetActiveScene().buildIndex;
		//SceneManager.LoadScene(scene, LoadSceneMode.Single);
		fadeIn.SetActive(true);
		StartCoroutine(FadeIn());
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		HUD.GetComponent<RawImage>().texture = fullHealth;
		GameOverText.SetActive(false);
		Background.SetActive(false);
		restartButton.SetActive (false);
		restartButton2.SetActive(false);
		restartButton3.SetActive(false);
		quitButton.SetActive(false);
		cam.GetComponent<AnotherOrbitCamTest>().enabled = true;
		player.GetComponent<CharaterController> ().playerHealth = 100;
		spawn = GameObject.FindGameObjectWithTag("Checkpoint");
		player.transform.position = spawn.transform.position;
	}
	IEnumerator FadeIn()
	{
		yield return new WaitForSeconds (0.8f);
		fadeIn.SetActive (false);
	}
		
}
