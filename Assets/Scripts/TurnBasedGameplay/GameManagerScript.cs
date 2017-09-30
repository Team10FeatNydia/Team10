using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    OVERWORLD,
    BATTLE,
    GAMEOVER
}

    public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript Instance;
    public GameStates curState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        curState = GameStates.BATTLE;
    }

    // Update is called once per frame
    void Update () {

    }
}
