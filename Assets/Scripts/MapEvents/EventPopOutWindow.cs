using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPopOutWindow : MonoBehaviour 
{
	public GameObject eventCanvasObj;
	private Canvas eventCanvas;

	void Start ()
	{
		eventCanvas = eventCanvasObj.GetComponent<Canvas> ();
		eventCanvas.enabled = false;
		GameObject.Find ("Player").GetComponent<PlayerMovementScript> ().enabled = true;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			eventCanvas.enabled = true;
			GameObject.Find ("Player").GetComponent<PlayerMovementScript> ().enabled = false;
		}
	}

	void OnTriggerExit (Collider other)
	{
		eventCanvas.enabled = false;
		GameObject.Find ("Player").GetComponent<PlayerMovementScript> ().enabled = true;
	}
}
