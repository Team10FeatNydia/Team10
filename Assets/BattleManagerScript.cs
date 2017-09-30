using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStates
{
PLAYER_TURN,
ENEMY_TURN
}

public class BattleManagerScript : MonoBehaviour {

    public static BattleManagerScript Instance;
    public Button attackButton;
    public BattleStates currTurn;

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        currTurn = BattleStates.PLAYER_TURN;
	}
	
	// Update is called once per frame
	void Update () {
        if (currTurn != BattleStates.PLAYER_TURN)
        {
            attackButton.interactable = false;
        }
        else if (currTurn == BattleStates.PLAYER_TURN)
        {
            attackButton.interactable = true;
        }
    }
}
