using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPouchScript : MonoBehaviour, IPointerClickHandler {

	BattleManagerScript battleManager;
	public bool opened = false;

	// Use this for initialization
	void Start () {
		battleManager = BattleManagerScript.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Attack()
	{
		Debug.Log("Attack");

		battleManager.target.health -= battleManager.player.attack;
		battleManager.target.GetComponent<MeshRenderer>().material.color = Color.red;
		battleManager.target = null;
		battleManager.currTurn = BattleStates.ENEMY_TURN;
	}

	void LayOutCards()
	{
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Click");

		if(opened)
		{
			if(battleManager.target != null)
			{
				Attack();
				opened = false;
				GetComponent<Image>().color = Color.white;
			}
		}
		else
		{
			opened = true;
			GetComponent<Image>().color = Color.blue;
		}

	}


}
