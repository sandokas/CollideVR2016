using UnityEngine;
using System.Collections;

public class BallShooter : MonoBehaviour {
	private Vector3 shooterOffset;

	public GameObject ballPrefab;
	// Use this for initialization
	void Start () {
		shooterOffset = new Vector3(0.0f, -0.4f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered){
			Debug.Log("Tap detected");
			Vector3 shooterOrigin = Camera.main.transform.position + shooterOffset;
			float playerDirectionX = Camera.main.transform.rotation.x;
			float playerDirectionY = Camera.main.transform.rotation.y;
			float playerDirectionYinAngles = Camera.main.transform.rotation.eulerAngles.y;

			GameObject ball = Instantiate(ballPrefab, shooterOrigin, Quaternion.identity) as GameObject;
			ball.transform.RotateAround(Camera.main.transform.position, Vector3.up, playerDirectionYinAngles);
			ball.GetComponent<Rigidbody>().velocity = new Vector3(playerDirectionY*10, playerDirectionX*5, 10);
		}

	}
}
