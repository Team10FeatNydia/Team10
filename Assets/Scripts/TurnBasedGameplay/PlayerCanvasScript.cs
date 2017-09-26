using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasScript : MonoBehaviour {

    public static PlayerCanvasScript Instance;
	// Use this for initialization
	void Awake () {
        Instance = this;
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManagerScript.Instance.curState != GameStates.OVERWORLD)
        {
            this.gameObject.SetActive(false);
        }
	}
}
