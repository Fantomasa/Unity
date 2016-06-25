using UnityEngine;
using System.Collections;

public class rocketMove : MonoBehaviour {

	public float rocketSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(1,0,0)* rocketSpeed*Time.deltaTime);
	}
}
