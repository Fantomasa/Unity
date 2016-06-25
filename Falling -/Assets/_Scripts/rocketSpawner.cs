using UnityEngine;
using System.Collections;

public class rocketSpawner : MonoBehaviour {

	public GameObject rocket;
	public float maxPosR;

	float delayTimer = 2.5f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Vector3 rocketPos = new Vector3 (transform.position.x, Random.Range (-4f, 4f), transform.position.z);
		
			Instantiate (rocket, rocketPos, transform.rotation);

			timer = delayTimer;
		}
	}
}
