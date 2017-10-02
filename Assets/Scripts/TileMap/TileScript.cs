using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct neighboursCheck
{
	public bool isNorthConnectable;
	public bool isSouthConnectable;
	public bool isEastConnectable;
	public bool isWestConnectable;

	public bool allSidesChecked;
	public bool isOccupied;
	public bool isChecked;
	public int moveStep;

	public TileScript next;
	public TileScript prev;
}

public class TileScript : MonoBehaviour {

	public neighboursCheck tileNeighbourCheck;
	public int[] tilePos = new int[2];

	// Use this for initialization
	void Start () {
		ResetTile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(TileMapManager.instance.selectedTile != null) TileMapManager.instance.selectedTile.GetComponent<MeshRenderer>().material.color = Color.white;
			GetComponent<MeshRenderer>().material.color = Color.red;
			TileMapManager.instance.selectedTile = this;
		}
	}

	public void ResetTile()
	{
		tileNeighbourCheck.isEastConnectable = true;
		tileNeighbourCheck.isWestConnectable = true;
		tileNeighbourCheck.isNorthConnectable = true;
		tileNeighbourCheck.isSouthConnectable = true;
		//tileNeighbourCheck.isOccupied = false;
		tileNeighbourCheck.isChecked = false;
		tileNeighbourCheck.allSidesChecked = false;
		tileNeighbourCheck.moveStep = 0;
		tileNeighbourCheck.prev = null;
	}
}
