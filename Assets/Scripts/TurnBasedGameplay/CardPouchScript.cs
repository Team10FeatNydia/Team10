using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Animations;

public class CardPouchScript : MonoBehaviour, IPointerClickHandler {

	BattleManagerScript battleManager;
	public GameObject[] displayedCards = new GameObject[5];
	public List<CardScript> selectedCards = new List<CardScript>();
	public Sprite cardEye;
	public Sprite magicEye;
	public Sprite defaultEye;
	public Vector2 fingerStartPos = Vector2.zero;
	public float minSwipeDist  = 10.0f;
	public bool isSwipe = false;
	public bool opened = false;
	public int manaCheck;


	private float touch1;
	private Sprite currentEye;

	// Use this for initialization
	void Start () {
		battleManager = BattleManagerScript.Instance;
		GetComponent<Image>().sprite = defaultEye;
	}

	// Update is called once per frame
	void Update () {
		Swipe();

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

	void Swipe()
	{

		if (Input.touchCount > 0){

			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
				case TouchPhase.Began :
					isSwipe = true;
					fingerStartPos = touch.position;
					touch1 = touch.position.y;
					break;

				case TouchPhase.Canceled :
					isSwipe = false;
					break;

				case TouchPhase.Ended :

					float swipeDist = (touch.position - fingerStartPos).magnitude;

					if ( touch1 < 45) {


						Debug.Log( swipeDist);

						if (isSwipe &&  swipeDist > minSwipeDist){
							Vector2 direction = touch.position - fingerStartPos;
							Vector2 swipeType = Vector2.zero;


							if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
							{
								swipeType = Vector2.right * Mathf.Sign(direction.x);
							}
							else
							{
								swipeType = Vector2.up * Mathf.Sign(direction.y);
							}

							if(swipeType.x != 0.0f)
							{
								GetComponent<Image>().sprite = magicEye;

							}

							if(swipeType.y != 0.0f )
							{
								GetComponent<Image>().sprite = cardEye;     
							}

						}
					}

					break;
				}
			}
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

			}
		}
		else
		{
			opened = true;
			LayOutCards();
			GetComponent<Image>().sprite = cardEye;

		}

	}
}
