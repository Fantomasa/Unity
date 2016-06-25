using UnityEngine;
using System.Collections;

public class skyMove : MonoBehaviour {

	public float speed = 5f;
	Vector2 offset;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		offset = new Vector2 (0, Time.time * speed);
		GetComponent<Renderer> ().material.mainTextureOffset = offset;

	}
}
