using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
	private bool isTap = false;
	private float startYPos;
	private Vector3 endPoint;
	public float duration = 5.0f;
	public GameObject player;

	void Start ()
	{
		startYPos = player.transform.position.y;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hit;
			Ray ray;

			ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);

			if (Physics.Raycast(ray,out hit))
			{
				isTap = true;
				endPoint = hit.point;
				endPoint.y = startYPos;
			}

		}

		if (isTap && !Mathf.Approximately (gameObject.transform.position.magnitude, endPoint.magnitude))
		{ 
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance (gameObject.transform.position, endPoint))));
		}
		else if (isTap && Mathf.Approximately (gameObject.transform.position.magnitude, endPoint.magnitude)) 
		{
			isTap = false;
		}
	}

	public void ArenaScene()
	{
		SceneManager.LoadScene(2);
	}
}
