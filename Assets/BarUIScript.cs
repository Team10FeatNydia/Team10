using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUIScript : MonoBehaviour {

    private Image bar;
    public PlayerStatusScript player;

	// Use this for initialization
	void Start () {
        bar = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        bar.fillAmount = ( player.health * 100f / player.maxHealth ) / 100;
	}
}
