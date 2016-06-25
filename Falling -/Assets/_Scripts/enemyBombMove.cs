using UnityEngine;
using System.Collections;

public class enemyBombMove : MonoBehaviour {

	public float speedBomb = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(-1,0,0)* speedBomb*Time.deltaTime);	
	}
}
