﻿using UnityEngine;
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
	public float width = 1.78f;
	public float height = 2.92f;
	public float delayReflexes = 0.5f;
	private bool movingRight = true;
	private enum Side {Right, Left, Middle, NotPicked}
	private Side chosenSide = Side.NotPicked;
	public float speed = 15.0f;

	float xmin = -3f;
	float xmax = 3f;
	float xforIdlemin = -1.5f;
	float xforIdlemax = 1.5f;

	// Use this for initialization
	void Start () {
		mySpriteRenderer = transform.Find ("GoalKeeper").gameObject.GetComponent<SpriteRenderer>();
		transform.FindChild ("GoalKeeper").transform.localRotation = Quaternion.identity;
		transform.FindChild ("GoalKeeper").transform.localPosition = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		//Make Goalkeeper look at the Player all the time
		transform.LookAt (Camera.main.transform.position, Vector3.up);



		//If the Ball is on it's way I should be Jumping!
		if (ballPrefab.GetComponent<ShootMe> ().IsFlying) {
			StartCoroutine (JumpToBall (delayReflexes));
		} else if (ballPrefab.GetComponent<BallCollider> ().collided) {
			transform.FindChild ("GoalKeeper").transform.localRotation = Quaternion.identity;
			transform.FindChild ("GoalKeeper").transform.localPosition = Vector3.zero;
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
		if (leftEdgeOfFormation < xforIdlemin)
		{
			movingRight = true;
		}
		else if (rightEdgeOfFormation > xforIdlemax)
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
			float diffx = ballPrefab.transform.position.x - transform.position.x;
			//if (Mathf.Abs (diffx) > 1) {
			if (chosenSide == Side.NotPicked) {
				if (Mathf.Abs (diffx) < 0.7f) {
					chosenSide = Side.Middle;
				} else if (diffx < 0) {
					chosenSide = Side.Left;
				} else {
					chosenSide = Side.Right;
				}
			}
			if (chosenSide == Side.Left) {
				if (xmin < transform.position.x) {
					transform.position += Vector3.left * speed * 3f * Time.deltaTime;
					//if (transform.eulerAngles.x <= -90) {
					transform.FindChild ("GoalKeeper").transform.Rotate (new Vector3 (0, 0, -90 * Time.deltaTime));
					mySpriteRenderer.sprite = gk_jumpright;
				} else {
					transform.position = new Vector3 (xmin, transform.position.y, transform.position.z);
					transform.FindChild ("GoalKeeper").transform.localRotation = Quaternion.identity;
					transform.FindChild ("GoalKeeper").transform.localPosition = Vector3.zero;
				}
			} else if (chosenSide == Side.Right) {
				if (xmax > transform.position.x) {
					transform.position += Vector3.right * speed * 3f * Time.deltaTime;
					//if (transform.eulerAngles.x >= 90) 
					transform.FindChild ("GoalKeeper").transform.Rotate (new Vector3 (0, 0, 90 * Time.deltaTime));
					mySpriteRenderer.sprite = gk_jumpleft;
				} else {
					transform.position = new Vector3 (xmax, transform.position.y, transform.position.z);
					transform.FindChild ("GoalKeeper").transform.localRotation = Quaternion.identity;
					transform.FindChild ("GoalKeeper").transform.localPosition = Vector3.zero;
				}
			} else {
                if (diffx < 0) {
                    if (xmin < transform.position.x)
                        transform.position += Vector3.left * speed * 1.5f * Time.deltaTime;
                } else
                {
                    if (xmax > transform.position.x)
                        transform.position += Vector3.right * speed * 1.5f * Time.deltaTime;
                }
                mySpriteRenderer.sprite = gk_leaningleft;
			}
		}
	}
}
