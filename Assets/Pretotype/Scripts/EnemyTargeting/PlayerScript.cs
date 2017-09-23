using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public static PlayerScript instance;

	float distance;
	float moveSpeed = 20.0f;
	float attackRange = 1.0f;
	public bool swiped;
	public EnemyScript target;
	public GameObject trail;

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		swiped = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

		Swipe();

		if(target != null) CheckAction();



		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
		}
		else if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
		}

	}

	public void CheckAction()
	{
		distance = Vector3.Distance(transform.position, target.transform.position);

		if(swiped)
		{
			MoveTowards();
		}

		if(distance < attackRange)
		{
			Attack();
		}
	}

	public void Attack()
	{
		transform.Rotate(0.5f, 0.5f, 0.5f);
	}

	public void MoveTowards()
	{
		if(distance > attackRange)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		}
		else
		{
			swiped = false;
		}
	}

	public void Swipe()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);

				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//normalize the 2d vector
				currentSwipe.Normalize();

				//swipe upwards
				if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					Debug.Log("up swipe");
					swiped = true;
				}
				//swipe down
				if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					Debug.Log("down swipe");
					swiped = true;
				}
				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					Debug.Log("left swipe");
					swiped = true;
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					Debug.Log("right swipe");
					swiped = true;
				}
			}
		}
	}
}
