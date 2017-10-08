using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPouchScript : MonoBehaviour, IPointerClickHandler {

	BattleManagerScript battleManager;
	public GameObject[] displayedCards = new GameObject[5];
	public List<CardScript> selectedCards = new List<CardScript>();
	public bool opened = false;
	public int manaCheck;

	// Use this for initialization
	void Start () {
		battleManager = BattleManagerScript.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Attack()
	{
		for(int i = 0; i < selectedCards.Count; i++)
		{
			if(selectedCards[i].myCard.cardType == CardType.ATTACK)
			{
				battleManager.target.health -= selectedCards[i].myCard.cardEffect;
			}
			else if(selectedCards[i].myCard.cardType == CardType.HEAL)
			{
				battleManager.player.health += selectedCards[i].myCard.cardEffect;
			}

			battleManager.player.manaPoints -= selectedCards[i].myCard.manaCost;
		}

		Debug.Log("Attack");

		//battleManager.target.health -= battleManager.player.attack;
		battleManager.target.GetComponent<MeshRenderer>().material.color = Color.red;

		battleManager.target.CheckHealth();

		battleManager.target = null;
		battleManager.currTurn = BattleStates.ENEMY_TURN;

		for(int i = 0; i < displayedCards.Length; i++)
		{
			Destroy(displayedCards[i].gameObject);
		}
	}

	void LayOutCards()
	{
		manaCheck = battleManager.player.manaPoints;
		selectedCards.Clear();

		for(int i = 0; i < CardManagerScript.Instance.cardList.Count; i++)
		{
			CardDescription tempCard;
			tempCard = CardManagerScript.Instance.cardList[i];
			tempCard.isSpawned = false;
			CardManagerScript.Instance.cardList[i] = tempCard;
		}

		for(int i = 0; i < 5; i++)
		{
			bool exitLoop = false;
			CardDescription tempCard;

			int randNum;

			do
			{
				randNum = Random.Range(0, CardManagerScript.Instance.cardList.Count);

				if(!CardManagerScript.Instance.cardList[randNum].isSpawned)
				{
					tempCard = CardManagerScript.Instance.cardList[randNum];
					tempCard.isSpawned = true;
					CardManagerScript.Instance.cardList[randNum] = tempCard;
					exitLoop = true;
				}
			}while(!exitLoop);

			GameObject newCard = Instantiate(CardManagerScript.Instance.cardPrefab, this.transform) as GameObject;

			CardScript cardScript = newCard.GetComponent<CardScript>();
			cardScript.myCard = CardManagerScript.Instance.cardList[randNum];
			cardScript.cardPouch = this;

			newCard.GetComponent<RectTransform>().localPosition = new Vector3(-135f*i - 150f, 0f, 0f);
			newCard.transform.SetParent(this.transform.parent, true);

			displayedCards[i] = newCard;
			displayedCards[i].GetComponent<CardScript>().UpdateStats();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Click");

		if(opened)
		{
			if(battleManager.target != null && selectedCards.Count > 0)
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
			LayOutCards();
		}

	}
}
