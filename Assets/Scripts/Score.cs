using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public float score = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TextMesh scoreText = gameObject.GetComponent<TextMesh>();
        scoreText.text = score.ToString();

    }
}
