using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotherOrbitCamTest : MonoBehaviour {

    private float Y_ANGLE_MIN = -60f;
    private float Y_ANGLE_MAX = 80.0f;

    private float bowClampMin = -30f;

    public Transform camLook;
	public Transform bowAim;
    public Transform camTransform;

    public float smooth = 10f;
    private Camera cam;
	private GameObject player;
	private bool isBow = false;
    private Vector3 dollyDir;
	public GameObject bowReticle;
    public bool camColid;

    private float heightOffset = 0;
	private float maxDistance;
    private float minDistance;
    private float preDistance;
    private float distance = 13.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensitivityX;
    public float sensitivityY;
    public int invertValue = 1;
    public Toggle invertToggle;
	public Transform followMe;
    public float camerMoveSpeed = 10f;
    //public Transform followMe2;
    Vector3 dir;
    Quaternion rotation;
    float step;
    LayerMask layerMask = ~((1<<2)|(1 << 8) | (1 << 9) | (1 << 11)| (1 << 12));
	public bool ground;

    public float[] cullsDist = new float[32];

    public GameObject iGCont;

	public void Awake()
	{
		currentX = followMe.transform.eulerAngles.y - 20.0f;
		//currentX = followMe2.transform.eulerAngles.y;
		currentY = 20.0f;
		maxDistance = 12.0f;
        minDistance = 0f;
        dollyDir = transform.position.normalized;
        distance = transform.localPosition.magnitude;

	}
    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        iGCont = GameObject.FindGameObjectWithTag("UIController");
		player = GameObject.FindGameObjectWithTag ("PlayerParent");
		ground = player.GetComponent<CharaterController> ().grounded;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= (Input.GetAxis("Mouse Y") * sensitivityY) * invertValue;
        //Added 5-30-17 by Daniel to test controller stick cam movement. it works :)
        currentX += Input.GetAxis("xb_rightH") * (sensitivityX + 40.0f);
        currentY -= Input.GetAxis("xb_rightV") * (sensitivityY + 40.0f) * invertValue; ;
        //if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance < maxDistance) { distance += 0.5f; }
        //if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance > minDistance) { distance -= 0.5f; }
        //preDistance = distance;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        cam.layerCullSpherical = true;
        cam.layerCullDistances = cullsDist;

        sensitivityX = iGCont.GetComponent<InGameController>().xSensSlider.value;
        sensitivityY = iGCont.GetComponent<InGameController>().ySensSlider.value;
		isBow = player.GetComponent<CharaterController> ().BowActive;
        dir = new Vector3(0, 0, -distance);
        rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.rotation = rotation;
        step = camerMoveSpeed * Time.deltaTime;
        CheckCollision();

        CameraUpdater();
    }

    private void LateUpdate()
    {
        
        dir = new Vector3(0, 0, -distance);
        rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.rotation = rotation;
        step = camerMoveSpeed * Time.deltaTime;
        CheckCollision();
        
        CameraUpdater();
        

    }

    void CheckCollision()
    {
        Vector3 desiredCamPos = camTransform.TransformPoint(dir * maxDistance);
        RaycastHit hit;
        Debug.DrawLine(camLook.position, desiredCamPos, Color.cyan);
        if (Physics.Linecast(camLook.position, desiredCamPos, out hit,layerMask))
        {
            Debug.DrawRay(camLook.position, desiredCamPos, Color.red);
            camColid = true;
            distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        //camTransform.position = Vector3.MoveTowards(camTransform.position, (camLook.position + new Vector3(0, heightOffset, 0)) + rotation * dir, step);
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);

    }

    void CameraUpdater()
    {
        
        if (isBow == true)
        {
            Y_ANGLE_MIN = bowClampMin;
            camTransform.position = Vector3.MoveTowards(camTransform.position, (camLook.position + new Vector3(0, heightOffset, 0)) + rotation * dir, step);
            //camTransform.LookAt(camLook.position + new Vector3(0, heightOffset, 0));

			if (Input.GetAxis("Fire2") > 0.2f)
            {
                player.GetComponent<CharaterController>().aiming = true;
                GetComponent<RaycastShoot>().aiming = true;
                bowReticle.SetActive(true);
                //Vector3 dir = new Vector3 (0, 0, -distance);
                //Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
                dir = new Vector3(0, 0, -4.0f);
                //camTransform.position = bowAim.position + rotation * dir;
                //camTransform.LookAt(bowAim.position);
                camTransform.position = Vector3.MoveTowards(camTransform.position, bowAim.position + rotation * dir, step);
				if(player.GetComponent<CharaterController>().grounded == false)
				{
					player.GetComponent<CharaterController>().aiming = false;
					bowReticle.SetActive(false);
				}
				if(player.GetComponent<CharaterController>().grounded == true)
				{
					player.GetComponent<CharaterController>().aiming = true;
					bowReticle.SetActive(true);
				}

            }
            else
            {
                GetComponent<RaycastShoot>().aiming = false;
                player.GetComponent<CharaterController>().aiming = false;
                bowReticle.SetActive(false);
                //distance = preDistance;
            }
            //StartCoroutine (DestroyProjectile ());
        }
        else
        {
            Y_ANGLE_MIN = -60;
            camTransform.position = Vector3.MoveTowards(camTransform.position, (camLook.position + new Vector3(0, heightOffset, 0)) + rotation * dir, step);
            //camTransform.LookAt(camLook.position + new Vector3(0, heightOffset, 0));
            //distance = maxDistance;
        }
    }

    //Added 6-1-17 to allow y axis inversion
    public void InvertYAxis()
    {
        invertValue = invertValue * -1;
    }

	IEnumerator DestroyProjectile()
	{
		yield return new WaitForSeconds (3);
		GameObject arrow;
		arrow = GameObject.FindGameObjectWithTag ("projectile");
		Destroy (arrow);
	}
}
