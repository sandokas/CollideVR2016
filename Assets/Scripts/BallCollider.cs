using UnityEngine;
using System.Collections;

public class BallCollider : MonoBehaviour {
	public Score scorePrefab;
	bool collided = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator RespawnBall(float time){
		GetComponent<ShootMe>().Reset();
		yield return new WaitForSeconds(time);
		transform.position = GetComponent<ShootMe>().defaultPosition;
		transform.rotation = Quaternion.identity;
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
		collided = false;
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.CompareTag("GoalTrigger") && collided == false){
			collided = true;
			Debug.Log("Goal!");
			scorePrefab.playerScore += 1;
			StartCoroutine(RespawnBall(3));
			return;
		}
	}

	void OnCollisionEnter(Collision collision){
		if((collision.gameObject.CompareTag("StadiumTrigger") || collision.gameObject.CompareTag("GoalkeeperTrigger")) 
		   && collided == false){
			collided = true;
			Debug.Log("Out!");
			scorePrefab.goalkeeperScore += 1;
			StartCoroutine(RespawnBall(3));
			return;
		}
	}
}
