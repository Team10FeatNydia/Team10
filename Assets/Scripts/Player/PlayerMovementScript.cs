using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour 
{
	[HideInInspector]
	public PlayerManager self;

	[Header("Movement")]
	public float distance;
	// Time to move from point A to point B
	public float lerpTime = 5.0f;
	private float currLerpTime = 0.0f;
	private Vector3 startPos;
	private Vector3 endPos;

	[Header("BooleanSettings")]
	public bool isTap = false;

	#region Movement
	// Use this for initialization
	void Start () 
	{
		startPos = self.transform.position;
		endPos = self.transform.position + Vector3.forward * distance;
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
			self.transform.position = Vector3.Lerp (startPos, endPos, percentage);
		}
	}
	#endregion Movement

	#region ChangeScenePlayer
	public void ArenaScene()
	{
		SceneManager.LoadScene(2);
	}
	#endregion ChangeScenePlayer
}