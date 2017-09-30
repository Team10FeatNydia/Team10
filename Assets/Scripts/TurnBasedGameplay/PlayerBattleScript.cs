using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleScript : MonoBehaviour {

    //Values added in the Inspector.
    public int health;
    public int attack;
    public EnemyBattleScript target;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack() {
        if (target.targeted)
        {
            target.health -= attack;
            BattleManagerScript.Instance.currTurn = BattleStates.ENEMY_TURN;
        }
    }
}
