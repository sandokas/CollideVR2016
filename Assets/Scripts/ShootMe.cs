using UnityEngine; 
using System.Collections; 

public class ShootMe : MonoBehaviour { 
	public AudioClip shootSound;
	public AudioClip stepSound1;
	public AudioClip stepSound2;
	public AudioClip stepSound3;
	public AudioClip stepSound4;
	//bool stepsound1played = false;
	//bool stepsound2played = false;

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
        // (not used currently)
        //GvrController.TouchDown
        // Due to GvrViewer.Instance.Triggered not working in my mobile

        if ((Input.touchCount > 0)) {
            foreach (Touch t in Input.touches) { 
                if (t.phase == TouchPhase.Began) { 
			        isPressed = true;
                    Debug.Log("Input.touchCount "+ Input.touchCount);
                }
            }
        }
        
        if (GvrViewer.Instance.Triggered)
        {
            isPressed = true;
            Debug.Log("GvrViewer.Instance.Triggered");
        }
        
        /*
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            Debug.Log("Input.GetMouseButtonDown(0)");
        }
        */
        
		if (isPressed && lerpAmt < 1 && !isFlying)                // and lerpAmt is not already at max 
		{ 
			//if(!stepsound1played){
				//Debug.Log("Step 1");

			switch (Random.Range (1, 4)) {
			case 1:
				AudioSource.PlayClipAtPoint (stepSound1, Camera.main.transform.position);
				break;
			case 2:
				AudioSource.PlayClipAtPoint (stepSound2, Camera.main.transform.position);
				break;
			case 3:
				AudioSource.PlayClipAtPoint (stepSound3, Camera.main.transform.position);
				break;
			case 4:
				AudioSource.PlayClipAtPoint (stepSound4, Camera.main.transform.position);
				break;
			default:
				break;

			}
				//stepsound1played = true;
			//}
			isBuildUp = true;
			lerpAmt += Time.deltaTime / timeToBuildUp; 
			//power = Mathf.Lerp ( powermin, powermax, lerpAmt*speed ); 
			if (power< powermax)
				power += powermin;
			timeSinceLastPress = 0;
			pointingTo = new Vector3 (cam.transform.forward.x,cam.transform.forward.y,cam.transform.forward.z);
		} 

		if (!isPressed && isBuildUp &&!isFlying) {
			/*if(!stepsound2played){
				Debug.Log("Step 2");
				AudioSource.PlayClipAtPoint(stepSound2,Camera.main.transform.position);
				stepsound2played = true;
			}*/
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
				GetComponent<BallCollider>().Defeat();
			}
		}
		isPressed = false;
	} 
	private void Shoot() {
		Debug.Log("Shoot!");
		AudioSource.PlayClipAtPoint(shootSound,Camera.main.transform.position);
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
