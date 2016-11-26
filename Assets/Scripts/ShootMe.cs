using UnityEngine; 
using System.Collections; 

public class ShootMe : MonoBehaviour { 
	private Camera cam; 
	private float power = 0; 
	public float speed = 500; 
	public float powermax = 5000; 
	public float powermin = 100; 
	public float timeToMove = 60f;       // time to move (in seconds) 
	private float lerpAmt = 0.0f;  // current lerp 't' amount (private, 0-1) 
	public float timeToWaitBeforeRelease = 1f;
	private float timeSinceLastPress = 0f;
	private bool isBuildUp = false;
	void Start() { 
		cam = GameObject.Find("Main Camera").GetComponent<Camera>(); 
	} 
	void  Update() 
	{ 

		if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && lerpAmt < timeToMove )                // and lerpAmt is not already at max 
		{ 
			isBuildUp = true;
			lerpAmt += Time.deltaTime / timeToMove; 
			power = Mathf.Lerp ( powermin, powermax, lerpAmt*speed ); 
		} 
		if (GvrViewer.Instance.VRModeEnabled && !GvrViewer.Instance.Triggered && isBuildUp) {
			timeSinceLastPress += Time.deltaTime;
		}
		if ( GvrViewer.Instance.VRModeEnabled && !GvrViewer.Instance.Triggered && isBuildUp && timeSinceLastPress > timeToWaitBeforeRelease) 
		{ 
			Shoot ();
			isBuildUp = false;
			timeSinceLastPress = 0f;
		} 

	} 
	private void Shoot() {
		Rigidbody rb =  GetComponent<Rigidbody>(); 
		Vector3 force = cam.transform.forward; 
		rb.AddForce (force*power);
	}
} 
