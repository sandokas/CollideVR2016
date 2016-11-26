using UnityEngine;
using System.Collections;

public class Crowd : MonoBehaviour {
	public Sprite crowd0;
	public Sprite crowd1;
	public Sprite crowd2;
	public Sprite crowd3;
	public Sprite crowd4;
	public Sprite crowd5;
	public Sprite crowd6;
	public Sprite crowd7;

	// Use this for initialization
	void Start () {
		//choose randomly one of the crowd sprites
		int rand = Random.Range(0, 7);
		switch(rand){
		case 0: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd0;
			break;
		case 1: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd1;
			break;
		case 2: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd2;
			break;
		case 3: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd3;
			break;
		case 4: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd4;
			break;
		case 5: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd5;
			break;
		case 6: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd6;
			break;
		case 7: 
			gameObject.GetComponent<SpriteRenderer>().sprite = crowd7;
			break;
		default:
			break;
		}

		//make crowd look at the center of the pitch
		transform.LookAt(new Vector3(0,0,-11), Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
