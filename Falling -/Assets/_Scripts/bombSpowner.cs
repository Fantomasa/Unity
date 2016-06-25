using UnityEngine;
using System.Collections;

public class bombSpowner : MonoBehaviour {

	public GameObject bomb;
	public float maxPosB;

	float delayTimer=2.5f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Vector3 bombPos = new Vector3 (transform.position.x, Random.Range (-4f, 4f), transform.position.z);
			Instantiate (bomb, bombPos, transform.rotation);
			timer = delayTimer;
		}
	}
}
