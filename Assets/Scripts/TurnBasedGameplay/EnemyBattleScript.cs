using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleScript : MonoBehaviour {

    public int health;
    public int attack;
    float posX;
    float posY;
    public bool targeted;
    public PlayerBattleScript player;

    // Use this for initialization
    void Start()
    {
        targeted = false;
        posX = this.gameObject.transform.position.x;
        posY = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    targeted = true;
                }
            }
        }

        if (targeted)
        {

        }

        if (BattleManagerScript.Instance.currTurn == BattleStates.ENEMY_TURN)
        {
            player.health -= attack;
            BattleManagerScript.Instance.currTurn = BattleStates.PLAYER_TURN;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
 
}
