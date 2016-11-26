using UnityEngine;
using System.Collections;

public class CrowdSpawner : MonoBehaviour {
	public GameObject crowdPrefab;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform){
			GameObject crowd = Instantiate(crowdPrefab, child.transform.position, Quaternion.identity) as GameObject;
			crowd.transform.parent = child;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
