using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    OVERWORLD,
    BATTLE,
}

    public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript Instance;
    public GameStates curState = GameStates.OVERWORLD;
    Camera playerBattleCamera;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerBattleCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        playerBattleCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (curState == GameStates.OVERWORLD)
        {
            Camera.main.gameObject.SetActive(true);
            playerBattleCamera.gameObject.SetActive(false);
        }
        else if (curState == GameStates.BATTLE)
        {
            playerBattleCamera.gameObject.SetActive(true);
            BattleCanvasScript.Instance.gameObject.SetActive(true);
        }
    }
}
