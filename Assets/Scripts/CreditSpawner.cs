using UnityEngine;
using System.Collections;

public class CreditSpawner : MonoBehaviour {
	public Sprite credits_1;
	public Sprite credits_2;
	public Sprite credits_3;
	public Sprite credits_4;
	public Sprite credits_5;
	public Sprite credits_6;
	public Sprite credits_7;
	public Sprite credits_8;
	public Sprite credits_9;
	int sprite_index = 0;

	// Use this for initialization
	void Start () {
		ChooseSprite();

		InvokeRepeating("ChooseSprite", 2.5f, 2.5f);

	}



	void ChooseSprite(){
		switch(sprite_index){
		case 0: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_1;
			break;
		case 1: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_2;
			break;
		case 2: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_3;
			break;
		case 3: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_4;
			break;
		case 4: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_5;
			break;
		case 5: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_6;
			break;
		case 6: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_7;
			break;
		case 7: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_8;
			break;
		case 8: 
			gameObject.GetComponent<SpriteRenderer>().sprite = credits_9;
			break;
		default:
			break;
		}

		if(sprite_index < 8) {
			sprite_index++;
		}
		else {
			sprite_index = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
