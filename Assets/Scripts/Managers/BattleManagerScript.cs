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
	public PlayerBattleScript player;
	public List<GameObject> enemyList = new List<GameObject>();
	public EnemyBattleScript target;
    //public Button attackButton;
    public BattleStates currTurn;
	public Canvas battleCanvas;
	public Text playerManaCount;
	public Text lockedEnemyState;
	public Text lockedEnemyHealth;

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currTurn = BattleStates.PLAYER_TURN;

		for(int i = 0; i < enemies.Length; i++)
		{
			enemyList.Add(enemies[i]);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateBattleUI();

        if (currTurn != BattleStates.PLAYER_TURN)
        {
			for(int i = 0; i < enemyList.Count; i++)
			{
				player.health -= enemyList[i].GetComponent<EnemyBattleScript>().attack;
			}

			currTurn = BattleStates.PLAYER_TURN;
        }
        else if (currTurn == BattleStates.PLAYER_TURN)
        {

        }
    }

	void UpdateBattleUI()
	{
		playerManaCount.text = player.manaPoints.ToString();

		if (target != null) {
			
			lockedEnemyHealth.enabled = true;
			lockedEnemyState.text = "Enemy Locked On";
			lockedEnemyHealth.text = "EnemyHealth" + target.health.ToString ();
			//hpText.color = Color.Lerp (hpText.color, Color.red, Time.deltaTime * easeTime);
//			if (target.health <= 0) {
//				lockedEnemyState.text = "you win, enemy dead";
//			}
		} 
		else 
		{
			lockedEnemyState.text = "Please Select a Enemy";

			lockedEnemyHealth.enabled = false;
			lockedEnemyHealth.color = Color.Lerp(lockedEnemyHealth.color, Color.red, Time.deltaTime);

			if(enemyList.Count <= 0)
			{
				lockedEnemyState.text = "you win, enemy dead";
			}
		}
	}
}
