using UnityEngine;
using System.Collections;

public class enemySpowner : MonoBehaviour {

	public static enemySpowner enemySp;

	public GameObject enemy;
	public float maxPos;
	public float delayTimer;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delayTimer;

	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (timer <= 0) {
			Vector3 enemyPos = new Vector3 (Random.Range (-2.50f,2.50f),transform.position.y,transform.position.z);
			
			Instantiate (enemy,enemyPos, transform.rotation);

			timer = delayTimer;
		}

	}
}
