using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour {

	public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
	public float fireRate = 1.2f;                                      // Number in seconds which controls how often the player can fire
	public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
	public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
	public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

	private Camera fpsCam;                                              // Holds a reference to the first person camera
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
	//private AudioSource gunAudio;                                       // Reference to the audio source which will play our shooting sound effect
	private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
	private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing
	public Transform followMe;
	public float enemyHealth;
	//public GameObject enemy;
	private GameObject closest;
	private GameObject temp;
	public GameObject projectile;
    Rigidbody instantiatedProjectile;
    public Animator anim;
    public Animator bow;

    public float speed = 120;
    public bool aiming;
    private bool canShoot = true;
    private bool canSpawn = true;
	public AudioClip pickupSound;
	public AudioClip bowDrawSound;
	public AudioSource audio;

	//public Transform camLook;


	void Start () 
	{
        
		audio = GetComponent<AudioSource>();
        aiming = false;
		// Get and store a reference to our LineRenderer component
		laserLine = GetComponent<LineRenderer>();

		// Get and store a reference to our AudioSource component
		//gunAudio = GetComponent<AudioSource>();
		//enemy = GameObject.FindGameObjectWithTag("Enemy");


		//enemyHealth = enemy.GetComponent<Health> ().health;

		// Get and store a reference to our Camera by searching this GameObject and its parents
		fpsCam = GetComponentInParent<Camera>();
	}


	void Update () 
	{
        projectile = GameObject.Find("Arrow");
        
        if(aiming && canSpawn)
        {
            laserLine.SetPosition(0, gunEnd.position);
			audio.PlayOneShot (bowDrawSound, 1f);
            instantiatedProjectile = Instantiate(projectile.GetComponent<Rigidbody>(), gunEnd.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)))as Rigidbody;
            canSpawn = false;
        }
        if(aiming && canShoot)
        {
            instantiatedProjectile.transform.position = gunEnd.position;
            instantiatedProjectile.transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
        }

		// Check if the player has pressed the fire button and if enough time has elapsed since they last fired
		if (Input.GetButtonDown("xb_R2") ||Input.GetMouseButtonDown(0) && canShoot && aiming) 
		{
			Debug.Log ("shooting");
            // Update the time when our player can fire next
			audio.PlayOneShot (pickupSound, 1f);
            bow.SetBool("Fire", true);
            anim.SetBool("BowShot", true);
            // Start our ShotEffect coroutine to turn our laser line on and off


            // Set the start position for our visual effect for our laser to the position of gunEnd


            instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, speed));

            StartCoroutine("ShootOver");
            canShoot = false;

            //StartCoroutine (DestroyArrow(instantiatedProjectile));
			// Check if our raycast has hit anything
			
		}
	}

	

    IEnumerator ShootOver()
    {
        yield return new WaitForSeconds(0.8f);
        bow.SetBool("Fire", false);
        anim.SetBool("BowShot", false);
        canShoot = true;
        canSpawn = true;

    }

    /*private IEnumerator DestroyArrow(Rigidbody go)
	{
		//Wait for .07 seconds
		yield return new WaitForSeconds(2);
		Destroy (go);
	}*/
}