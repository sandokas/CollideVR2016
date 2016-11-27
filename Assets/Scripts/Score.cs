using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public float playerScore = 0f;
	public float goalkeeperScore = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TextMesh scoreText = gameObject.GetComponent<TextMesh>();
        scoreText.text = "Home " + playerScore.ToString() + "-" + goalkeeperScore.ToString() + " Away";

    }
}
