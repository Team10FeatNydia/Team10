using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	Material myMat;
	public bool targeted;

	// Use this for initialization
	void Start () {
		targeted = false;
		myMat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		//myMat.color = Color.black;
		if(!targeted)
		{
			LockOff();
			LockOn();
			MovementScript.instance.target = this;
		}
		else
		{
			LockOff();
		}
	}

	void LockOn()
	{
		targeted = true;
		myMat.color = Color.black;
	}

	void LockOff()
	{
		if(MovementScript.instance.target != null)
		{
			MovementScript.instance.target.myMat.color = Color.red;
			MovementScript.instance.target.targeted = false;
			MovementScript.instance.target = null;
		}
	}

}
