using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStates
{
	PLAYER_TURN,
	ENEMY_TURN
}

public class BattleManagerScript : MonoBehaviour 
{
	#region Singleton
	private static BattleManagerScript mInstance;

	public static BattleManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject temp = GameObject.FindGameObjectWithTag("BattleManager");

				if(temp == null)
				{
					Instantiate(ManagerControllerScript.Instance.battleManagerPrefab, Vector3.zero, Quaternion.identity);
				}
				mInstance = temp.GetComponent<BattleManagerScript>();
			}
			return mInstance;
		}
	}

	public static bool CheckInstanceExit()
	{
		return mInstance;
	}
	#endregion Singleton

	public PlayerStatusScript player;
	public List<EnemyStatusScript> enemyList = new List<EnemyStatusScript>();
	public EnemyStatusScript target;
    //public Button attackButton;
    public BattleStates currTurn;
	public Canvas battleCanvas;
	public Text playerManaCount;
	public Text lockedEnemyState;
	public Text lockedEnemyHealth;

    void Awake() 
	{
		if(BattleManagerScript.CheckInstanceExit())
		{
			Destroy(this.gameObject);
		}
    }

	// Use this for initialization
	void Start () 
	{
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currTurn = BattleStates.PLAYER_TURN;

		for(int i = 0; i < enemies.Length; i++)
		{
			enemyList.Add(enemies[i].GetComponent<EnemyStatusScript>());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(PauseMenuManagerScript.Instance.paused) return;

		UpdateBattleUI();

        if (currTurn != BattleStates.PLAYER_TURN)
        {
			for(int i = 0; i < enemyList.Count; i++)
			{
				enemyList[i].Attack();
			}

			currTurn = BattleStates.PLAYER_TURN;
        }
        else if (currTurn == BattleStates.PLAYER_TURN)
        {

        }
    }

	void UpdateBattleUI()
	{
		playerManaCount.text = player.localPlayerData.manaPoints.ToString();

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
