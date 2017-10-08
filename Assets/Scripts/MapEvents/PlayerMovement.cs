using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
	public GameObject player;

	private Vector3 startPos;
	private Vector3 endPos;
	public float distance;

	// Time to move from point A to point B
	public float lerpTime = 5.0f;
	private float currLerpTime = 0.0f;

	public bool isTap = false;

	// Use this for initialization
	void Start () 
	{
		startPos = player.transform.position;
		endPos = player.transform.position + Vector3.forward * distance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount == 1)
		{
			isTap = true;
		}

		if (isTap == true) 
		{
			currLerpTime += Time.deltaTime;
			if (currLerpTime >= lerpTime) 
			{
				currLerpTime = lerpTime;
			}

			float percentage = currLerpTime / lerpTime;
			player.transform.position = Vector3.Lerp (startPos, endPos, percentage);
		}
	}

	public void ArenaScene()
	{
		SceneManager.LoadScene(2);
	}
}
