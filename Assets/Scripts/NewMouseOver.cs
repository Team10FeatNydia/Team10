using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseOver : MonoBehaviour {

	public float scale;
	public float minScale;
	public float maxScale;
	public bool scaleUp;
	public float scaleSpeed;


	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		previewObject ();
	
	}

	void OnMouseOver ()
	{
		scaleUp = true;

		Debug.Log ("dfsjgnijidf");
	}

	void OnMouseExit ()
	{
		scaleUp = false;
	}

	void previewObject ()
	{
		if (scaleUp) {
			scale += scaleSpeed * Time.deltaTime;
			if (scale > maxScale) 
			{
				scale = maxScale;
			}
			transform.localScale = new Vector3 (scale, scale, scale);
		}
		else
		{
			scale -= scaleSpeed * Time.deltaTime;;
			if (scale < minScale) 
			{
				scale = minScale;
			}
			transform.localScale = new Vector3 (scale, scale, scale);
		}



	
	}
}
