using UnityEngine;
using System.Collections;

public class buttonMovement : touchManager {

	public enum type{LeftButton, RightButton, JumpButton};
	public type buttonType;

	public float jumpHeight = 0.0f;
	public float moveSpeed = 0.0f;

	public GameObject player = null ;
	Rigidbody2D rb = null;

	public GUITexture buttonTexture = null;

	// Use this for initialization
	void Start () {
		rb = player.GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
	
		TouchInput (buttonTexture);
	}
	/*
	void OnFirstTouchBegan()
	{
		switch(buttonType)
		{
		case type.JumpButton:
			rb.AddForce(Vector2.up * jumpHeight,ForceMode2D.Impulse);
			break;
		}
	} */
	/*
	void OnSecondTouchBegan()
	{
		switch(buttonType)
		{
		case type.JumpButton:
			rb.AddForce(Vector2.up * jumpHeight,ForceMode2D.Impulse);
			break;
		}
	}
	*/
	void OnFirstTouch()
	{
		switch(buttonType)
		{
		case type.LeftButton:
			player.transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
			break;
		case type.RightButton:
			player.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
			break;
		}
	}
	void OnSecondTouch()
	{
		switch(buttonType)
		{
		case type.LeftButton:
			player.transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
			break;
		case type.RightButton:
			player.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
			break;
		}
	}


}
