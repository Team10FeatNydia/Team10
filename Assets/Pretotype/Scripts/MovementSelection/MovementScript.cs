using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour 
{
	public static MovementScript instance;

	float distance;
	float moveSpeed = 20.0f;
	float attackRange = 1.0f;
	public EnemyScript target;

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

    public NavMeshAgent playerAgent;
    private RaycastHit interactionInfo;
    private GameObject interactedObject;

    private LayerMask floorLayer;

	void Awake()
	{
		instance = this;
	}

    // Use this for initialization
    void Start () 
	{
        playerAgent = GetComponent<NavMeshAgent>();
        floorLayer = LayerMask.GetMask("FloorLayer");
    }
	
	// Update is called once per frame
	void Update () 
	{

        Movement();
	}

	public void CheckAction()
	{
		distance = Vector3.Distance(transform.position, target.transform.position);

		if(distance < attackRange)
		{
			Attack();
		}
	}

	public void Attack()
	{
		transform.Rotate(0.5f, 0.5f, 0.5f);
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
