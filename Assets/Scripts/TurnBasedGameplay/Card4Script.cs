using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card4Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Card4Button() {
        GameManagerScript.Instance.curState = GameStates.OVERWORLD;
        AttackButtonScript.Instance.button.interactable = false;
    }
}
