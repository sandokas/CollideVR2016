using UnityEngine;
using System.Collections;

public class Goalkeeper : MonoBehaviour {
	public GameObject ballPrefab;

	public float width = 10f;
	public float height = 5f;
	public float delayReflexes = 0.5f;
	private bool movingRight = true;
	
	public float speed = 15.0f;
	
	float xmin = -3f;
	float xmax = 3;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Make Goalkeeper look at the Player all the time
		transform.LookAt(Camera.main.transform.position, Vector3.up);

		if (ballPrefab.GetComponent<ShootMe> ().IsFlying) {
			StartCoroutine (JumpToBall(delayReflexes));
		} else {
			IdleMovement ();
		}

	}
	void IdleMovement () {
		//Handle Goalkeeper movement
		if (movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		if (leftEdgeOfFormation < xmin)
		{
			movingRight = true;
		}
		else if (rightEdgeOfFormation > xmax)
		{
			movingRight = false;
		}
	}
	IEnumerator JumpToBall(float waitForSeconds) {
		yield return new WaitForSeconds (waitForSeconds);
		if (transform.position.x - ballPrefab.transform.position.x < 0 ) {
			if (xmax > transform.position.x)
				transform.position += Vector3.right * speed * 2 * Time.deltaTime;
		} else {
			if (xmin < transform.position.x)
				transform.position += Vector3.left * speed * 2 *Time.deltaTime;
		}
	}
}
