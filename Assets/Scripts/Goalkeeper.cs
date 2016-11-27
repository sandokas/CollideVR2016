using UnityEngine;
using System.Collections;

public class Goalkeeper : MonoBehaviour {
	public GameObject ballPrefab;
	public Sprite gk_readyleft;
	public Sprite gk_readyright;
	public Sprite gk_jumpleft;
	public Sprite gk_jumpright;
	public Sprite gk_leaningleft;
	public Sprite gk_leaningright;
	public Sprite gk_victory;
	public Sprite gk_defeat;
	public enum Gk_state {Victory, Defeat, Idle, Jumping};
	public Gk_state current_state;


	public float width = 10f;
	public float height = 5f;
	public float delayReflexes = 0.5f;
	private bool movingRight = true;

	public float speed = 15.0f;

	float xmin = -3f;
	float xmax = 3;

	// Use this for initialization
	void Start () {
		current_state = Gk_state.Idle;
	}

	// Update is called once per frame
	void Update () {
		//Make Goalkeeper look at the Player all the time
		transform.LookAt (Camera.main.transform.position, Vector3.up);

		//If the Ball is on it's way I should be Jumping!
		if (ballPrefab.GetComponent<ShootMe> ().IsFlying) {
			StartCoroutine (JumpToBall (delayReflexes));
		} else if (ballPrefab.GetComponent<BallCollider> ().collided) {
			if (ballPrefab.GetComponent<BallCollider> ().isVictory) {
				gameObject.GetComponent<SpriteRenderer>().sprite = gk_defeat;
			} else {
				gameObject.GetComponent<SpriteRenderer>().sprite = gk_victory;
			}
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
		//Sprites
		if (transform.position.x > 0) {
			gameObject.GetComponent<SpriteRenderer>().sprite = gk_readyright;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = gk_readyleft;
		}
	}
	IEnumerator JumpToBall(float waitForSeconds) {
		yield return new WaitForSeconds (waitForSeconds);
		if (transform.position.x - ballPrefab.transform.position.x < 0 ) {
			if (xmax > transform.position.x) {
				transform.position += Vector3.right * speed * 2 * Time.deltaTime;
				gameObject.GetComponent<SpriteRenderer>().sprite = gk_jumpleft;
			}
		} else {
			if (xmin < transform.position.x) {
				transform.position += Vector3.left * speed * 2 * Time.deltaTime;
				gameObject.GetComponent<SpriteRenderer>().sprite = gk_jumpright;
			}
		}
	}
}
