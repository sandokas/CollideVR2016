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

	//internal
	private Camera cam; 
	private float power = 0; 
	private float timeSinceLastPress = 0f;
	private bool isBuildUp = false;

	void Start() { 
		cam = GameObject.Find("Main Camera").GetComponent<Camera>(); 
	} 
	void  Update() 
	{ 

		if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && lerpAmt < 1 )                // and lerpAmt is not already at max 
		{ 
			isBuildUp = true;
			lerpAmt += Time.deltaTime / timeToBuildUp; 
			power = Mathf.Lerp ( powermin, powermax, lerpAmt*speed ); 
			timeSinceLastPress = 0;
		} 
		if (GvrViewer.Instance.VRModeEnabled && !GvrViewer.Instance.Triggered && isBuildUp) {
			timeSinceLastPress += Time.deltaTime;
		}
		if ( GvrViewer.Instance.VRModeEnabled && !GvrViewer.Instance.Triggered && isBuildUp && timeSinceLastPress > timeLagMax) 
		{ 
			Shoot ();
			isBuildUp = false;
			timeSinceLastPress = 0f;
		} 

	} 
	private void Shoot() {
		Rigidbody rb =  GetComponent<Rigidbody>(); 
		Vector3 force = new Vector3 (cam.transform.forward.x * power, cam.transform.forward.y * y_emphasis * power, cam.transform.forward.z * power); 
		rb.AddForce (force);
	}
} 
