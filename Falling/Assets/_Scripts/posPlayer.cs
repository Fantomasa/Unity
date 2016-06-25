using UnityEngine;
using System.Collections;

public class posPlayer : MonoBehaviour {

	float maxPos;
	Vector3 postion;

	// Use this for initialization
	void Start () {
		postion = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Die") {
			Destroy(this.gameObject);
		}
	}
}
