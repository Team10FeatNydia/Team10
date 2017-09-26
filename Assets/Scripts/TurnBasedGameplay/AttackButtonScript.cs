using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonScript : MonoBehaviour {
    public static AttackButtonScript Instance;
    public Button button;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}

    void Start() {
        button = this.gameObject.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackButton() {
        GameManagerScript.Instance.curState = GameStates.BATTLE;
    }
}
