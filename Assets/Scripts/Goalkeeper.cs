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

	private SpriteRenderer mySpriteRenderer;
	public float width = 10f;
	public float height = 5f;
	public float delayReflexes = 0.5f;
	private bool movingRight = true;
	private enum Side {Right, Left, Middle, NotPicked}
	private Side chosenSide = Side.NotPicked;
	public float speed = 15.0f;

	float xmin = -3f;
	float xmax = 3;

	// Use this for initialization
	void Start () {
		mySpriteRenderer = transform.Find ("GoalKeeper").gameObject.GetComponent<SpriteRenderer>();
		transform.FindChild ("GoalKeeper").transform.rotation = Quaternion.identity;
	}

	// Update is called once per frame
	void Update () {
		//Make Goalkeeper look at the Player all the time
		transform.LookAt (Camera.main.transform.position, Vector3.up);



		//If the Ball is on it's way I should be Jumping!
		if (ballPrefab.GetComponent<ShootMe> ().IsFlying) {
			StartCoroutine (JumpToBall (delayReflexes));
		} else if (ballPrefab.GetComponent<BallCollider> ().collided) {
			transform.FindChild ("GoalKeeper").transform.rotation = Quaternion.identity;
			chosenSide = Side.NotPicked;
			if (ballPrefab.GetComponent<BallCollider> ().isVictory) {
				mySpriteRenderer.sprite = gk_defeat;
			} else {
				mySpriteRenderer.sprite = gk_victory;
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
			mySpriteRenderer.sprite = gk_readyright;
		} else {
			mySpriteRenderer.sprite = gk_readyleft;
		}
	}
	IEnumerator JumpToBall(float waitForSeconds) {
		yield return new WaitForSeconds (waitForSeconds);
		if (!ballPrefab.GetComponent<BallCollider> ().collided) {
			float diffx = transform.position.x - ballPrefab.transform.position.x;
			//if (Mathf.Abs (diffx) > 1) {
			if (chosenSide == Side.NotPicked) {
				if (Mathf.Abs (diffx) < 1) {
					chosenSide = Side.Middle;
				} else if (diffx < 0) {
					chosenSide = Side.Right;
				} else {
					chosenSide = Side.Left;
				}
			}
			if (chosenSide == Side.Left) {
				if (xmax > transform.position.x) {
					transform.position += Vector3.left * speed * 2.5f * Time.deltaTime;
					//if (transform.eulerAngles.x <= -90) {
					transform.FindChild ("GoalKeeper").transform.Rotate (new Vector3 (0, 0, -90 * Time.deltaTime));
					mySpriteRenderer.sprite = gk_jumpleft;
				} else {
					transform.position = new Vector3 (xmax, transform.position.y, transform.position.z);
				}
			} else if (chosenSide == Side.Right) {
				if (xmin < transform.position.x) {
					transform.position += Vector3.right * speed * 2.5f * Time.deltaTime;
					//if (transform.eulerAngles.x >= 90) 
					transform.FindChild ("GoalKeeper").transform.Rotate (new Vector3 (0, 0, 90 * Time.deltaTime));
					mySpriteRenderer.sprite = gk_jumpright;
				} else {
					transform.position = new Vector3 (xmin, transform.position.y, transform.position.z);
				}
			} else {
				mySpriteRenderer.sprite = gk_leaningleft;
			}
		}
//		} else {
//			if (diffx > 0) {
//				transform.position += Vector3.left * speed * 1.5f * Time.deltaTime;
//				mySpriteRenderer.sprite = gk_readyleft;
//			} else {
//				transform.position += Vector3.left * speed * 1.5f * Time.deltaTime;
//				mySpriteRenderer.sprite = gk_readyright;
//			}
//		}
	}
}
