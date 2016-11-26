using UnityEngine;
using System.Collections;

public class BallCollider : MonoBehaviour {
	public Score scorePrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.CompareTag("GoalTrigger")){
			Debug.Log("Goal!");
			scorePrefab.playerScore += 1;
			return;
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag("StadiumTrigger") || collision.gameObject.CompareTag("GoalkeeperTrigger")){
			Debug.Log("Out!");
			scorePrefab.goalkeeperScore += 1;
			return;
		}
	}
}
