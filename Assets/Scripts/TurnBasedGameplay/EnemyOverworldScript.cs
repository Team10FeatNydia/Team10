using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOverworldScript : MonoBehaviour {

    Vector3 touchPosWorld;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && GameManagerScript.Instance.curState == GameStates.OVERWORLD)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    PlayerCanvasScript.Instance.gameObject.SetActive(true);
                }
                else
                {
                    PlayerCanvasScript.Instance.gameObject.SetActive(false);
                }
            }
        }
    }
}
