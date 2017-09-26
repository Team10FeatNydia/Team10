using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldScript : MonoBehaviour {

    public static PlayerOverworldScript Instance;

    public Vector3 playerPos;

    void Awake()
    {
        Instance = this;    
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerPos = this.gameObject.transform.position;
	}
}
