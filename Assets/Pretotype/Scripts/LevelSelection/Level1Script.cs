using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        SoundManagerScript.Instance.PlayBGM(AudioClipID.BGM_LEVEL_1);
    }
}
