using UnityEngine;
using System.Collections;

public class BallCollider : MonoBehaviour {
	public Score scorePrefab;

	public AudioClip goalSound;
	public AudioClip failSound;
    public AudioClip catchSound;
	public bool gk_touchedIt = false;
	private float timeTouchedIt = 0f;
	public bool isGame = true;
	public bool isVictory = true;

	public bool collided = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gk_touchedIt) {
			timeTouchedIt += Time.deltaTime;
			if (timeTouchedIt > 1f) {
                if (!collided)
				    Defeat ();
				timeTouchedIt = 0f;
				gk_touchedIt = false;
			}
		}
		if (transform.position.z >= -4f && !collided) {
			Defeat ();
		}
	}

	private IEnumerator RespawnBall(float time){
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
			Victory ();
		}
	}

	void OnCollisionEnter(Collision collision){
		if((collision.gameObject.CompareTag("StadiumTrigger") || collision.gameObject.CompareTag("GoalkeeperTrigger")) 
		   && collided == false){
			gk_touchedIt = true;
            AudioSource.PlayClipAtPoint(catchSound, Camera.main.transform.position);
        }
	}
	public void Defeat () {
		collided = true;
		isVictory = false;
		AudioSource.PlayClipAtPoint (failSound, Camera.main.transform.position);
		scorePrefab.goalkeeperScore += 1;
		StartCoroutine (RespawnBall (3));
	}
	public void Victory() {
		collided = true;
		isVictory = true;
		AudioSource.PlayClipAtPoint(goalSound,Camera.main.transform.position);
		scorePrefab.playerScore += 1;
		StartCoroutine(RespawnBall(3));
	}
}
