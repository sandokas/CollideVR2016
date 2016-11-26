using UnityEngine; 
using System.Collections; 

public class ShootMe : MonoBehaviour { 
	//fix ball
	public float speed = 5; 
	public float powermax = 5000; 
	public float powermin = 100; 
	public float timeToBuildUp = 5f;       // time to move (in seconds) 
	private float lerpAmt = 0.0f;  // current lerp 't' amount (private, 0-1) 
	public float timeLagMax = 1f;
	public float y_emphasis = 2f;

	public float bananapower = 50f;
	public float timeToReset = 5f;
	public Vector3 defaultPosition = new Vector3(0f,1f,-18f);
	public float fakegravity = 0.0f;
	public float fakeCannonKickEffect = 0.0f;

	//internal
	private Camera cam; 
	private Vector3 pointingTo;
	private Vector3 leaningTo;
	private float power = 0; 
	private float timeSinceLastPress = 0f;
	private float timeFlying = 0f;
	private bool isBuildUp = false;
	private bool isFlying = false;
	private bool isPressed = false;

	public bool IsFlying {
		get {
			return isFlying;
		}
	}

	public void Reset() {
		isFlying = false;
		timeFlying = 0f;
		power = 0f;
		lerpAmt = 0f;
	}

	void Start() { 
		cam = GameObject.Find("Main Camera").GetComponent<Camera>(); 
	} 
	void  Update() 
	{ 
		if (GvrController.TouchDown || (Input.touchCount > 0) || GvrViewer.Instance.Triggered || Input.GetMouseButtonDown(0)) {
			isPressed = true;
		}
		if (isPressed && lerpAmt < 1 && !isFlying)                // and lerpAmt is not already at max 
		{ 
			isBuildUp = true;
			lerpAmt += Time.deltaTime / timeToBuildUp; 
			//power = Mathf.Lerp ( powermin, powermax, lerpAmt*speed ); 
			if (power< powermax)
				power += powermin;
			timeSinceLastPress = 0;
			pointingTo = new Vector3 (cam.transform.forward.x,cam.transform.forward.y,cam.transform.forward.z);
		} 

		if (!isPressed && isBuildUp &&!isFlying) {
			timeSinceLastPress += Time.deltaTime;
		}
		if (!isPressed && isBuildUp && timeSinceLastPress > timeLagMax && !isFlying) 
		{ 
			Shoot ();
			leaningTo = new Vector3 (cam.transform.forward.x,cam.transform.forward.y,cam.transform.forward.z);
			isBuildUp = false;
			isFlying = true;
			timeSinceLastPress = 0f;
		} 

		if (isFlying) {
			if ( timeFlying < timeToReset)  //
			{
				Banana ();
				timeFlying += Time.deltaTime;
			} else {
				//Nao e preciso fazer reset porque vai ser chamado na Coroutine
				StartCoroutine(GetComponent<BallCollider>().RespawnBall(3));
			}
		}
		isPressed = false;
	} 
	private void Shoot() {
		Rigidbody rb =  GetComponent<Rigidbody>(); 
		Vector3 force = new Vector3 (pointingTo.x * power, pointingTo.y * y_emphasis * power, pointingTo.z * power); 
		rb.AddForce (force);
	}
	private void Banana() {
		Rigidbody rb =  GetComponent<Rigidbody>(); 
		Vector3 force = new Vector3 (leaningTo.x * bananapower, fakegravity, fakeCannonKickEffect); 
		rb.AddRelativeForce (force);
	}
} 
