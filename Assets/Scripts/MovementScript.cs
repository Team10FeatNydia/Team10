using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour {

    public NavMeshAgent playerAgent;
    private RaycastHit interactionInfo;
    private GameObject interactedObject;

    private LayerMask floorLayer;

    // Use this for initialization
    void Start () {
        playerAgent = GetComponent<NavMeshAgent>();
        floorLayer = LayerMask.GetMask("FloorLayer");
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {

        if (Input.touchCount == 1)
        {
            Ray ray;

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out interactionInfo, Mathf.Infinity, floorLayer))
                    {
                        interactedObject = interactionInfo.transform.gameObject;
                        playerAgent.destination = interactionInfo.point;
                    }
                }
                
            }
        }
    }
}
