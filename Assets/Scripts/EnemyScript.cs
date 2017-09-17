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
			PlayerScript.instance.target = this;
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
		if(PlayerScript.instance.target != null)
		{
			PlayerScript.instance.target.myMat.color = Color.red;
			PlayerScript.instance.target.targeted = false;
			PlayerScript.instance.target = null;
		}
	}

}
