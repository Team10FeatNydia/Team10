using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : MonoBehaviour {

	public static TileMapManager instance;

	public Transform player;
	public bool canMove = false;

	public GameObject enemyPrefab;

	public Transform tilePrefab;
	public Vector2 mapSize;
	public TileScript[,] tileMap = new TileScript[10, 10];
	List<TileScript> tilePath;

	public TileScript selectedTile;
	public TileScript currTile;
	public TileScript node;

	public float timer;

	void Awake()
	{
		if(instance == null) instance = this;
	}

	// Use this for initialization
	void Start () {
		tilePath = new List<TileScript>();
		GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(selectedTile != null && !selectedTile.tileNeighbourCheck.isOccupied)
			{
				node = currTile;

				FindPath();
			}
		}

		if(canMove) MoveToTile();


	}

	public void GenerateMap()
	{
		for(int x = 0; x < mapSize.x; x++)
		{
			for(int y = 0; y < mapSize.y; y++)
			{
				Vector3 tilePosition = new Vector3(-mapSize.x/2 + 0.5f + x, 0, -mapSize.y/2 + 0.5f + y);
				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right*90)) as Transform;
				tileMap[y,x] = newTile.GetComponent<TileScript>();
				newTile.GetComponent<TileScript>().tilePos[0] = y;
				newTile.GetComponent<TileScript>().tilePos[1] = x;
				newTile.parent = transform;
			}
		}

		currTile = tileMap[0,0];
		tileMap[0,0].tileNeighbourCheck.isOccupied = true;
		player.position = currTile.transform.position + new Vector3(0, 0.5f, 0);

		GenerateEnemies();

	}

	void GenerateEnemies()
	{
		for(int i = 0; i < 5; i++)
		{
			bool exitLoop = false;

			while(!exitLoop)
			{
				int randX = Random.Range(0, (int)mapSize.x);
				int randY = Random.Range(0, (int)mapSize.y);

				if(!tileMap[randY, randX].tileNeighbourCheck.isOccupied)
				{
					GameObject newEnemy = Instantiate(enemyPrefab, tileMap[randY, randX].transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
					tileMap[randY, randX].tileNeighbourCheck.isOccupied = true;
					exitLoop = true;
				}
			}
		}
	}

	public void FindPath()
	{
		for(int a = 0; a < mapSize.x; a++)
		{
			for(int b = 0; b < mapSize.y; b++)
			{
				tileMap[b,a].ResetTile();
			}
		}

		tilePath = new List<TileScript>();

		int x = node.tilePos[1];
		int y = node.tilePos[0];
		int moveNum = 0;

		bool targetReached = false;

		tilePath.Add(tileMap[y,x]);

		while(!targetReached)
		{
			timer += Time.deltaTime;

			for(int i = 0; i < tilePath.Count; i++)
			{
				x = tilePath[i].tilePos[1];
				y = tilePath[i].tilePos[0];

				if(!tileMap[y,x].tileNeighbourCheck.allSidesChecked)
				{
					if(y < mapSize.y - 1)
					{
						//Check north side
						if(tileMap[y+1,x] != null)
						{
							if(!tileMap[y+1,x].tileNeighbourCheck.isOccupied)
							{
								if(tileMap[y,x].tileNeighbourCheck.isNorthConnectable && tileMap[y+1,x].tileNeighbourCheck.isSouthConnectable 
									&& tileMap[y+1,x] != node)
								{
									if(!tileMap[y+1,x].tileNeighbourCheck.isChecked)
									{
										tileMap[y+1,x].tileNeighbourCheck.prev = tileMap[y,x];
										tileMap[y+1,x].tileNeighbourCheck.moveStep = moveNum;
										tileMap[y+1,x].tileNeighbourCheck.isChecked = true;
										tilePath.Add(tileMap[y+1,x]);

										if(tileMap[y+1,x] == selectedTile)
										{
											targetReached = true;
											break;
										}
									}
								}
							}
						}
					}

					if(y > 0)
					{
						//Check south side
						if(tileMap[y-1,x] != null)
						{
							if(!tileMap[y-1,x].tileNeighbourCheck.isOccupied)
							{
								if(tileMap[y,x].tileNeighbourCheck.isSouthConnectable && tileMap[y-1,x].tileNeighbourCheck.isNorthConnectable 
									&& tileMap[y-1,x] != node)
								{
									if(!tileMap[y-1,x].tileNeighbourCheck.isChecked)
									{
										tileMap[y-1,x].tileNeighbourCheck.prev = tileMap[y,x];
										tileMap[y-1,x].tileNeighbourCheck.moveStep = moveNum;
										tileMap[y-1,x].tileNeighbourCheck.isChecked = true;
										tilePath.Add(tileMap[y-1,x]);

										if(tileMap[y-1,x] == selectedTile)
										{
											targetReached = true;
											break;
										}
									}
								}
							}
						}
					}

					if(x < mapSize.x - 1)
					{
						//Check east side
						if(tileMap[y,x+1] != null)
						{
							if(!tileMap[y,x+1].tileNeighbourCheck.isOccupied)
							{
								if(tileMap[y,x].tileNeighbourCheck.isEastConnectable && tileMap[y,x+1].tileNeighbourCheck.isWestConnectable 
									&& tileMap[y,x+1] != node)
								{
									if(!tileMap[y,x+1].tileNeighbourCheck.isChecked)
									{
										tileMap[y,x+1].tileNeighbourCheck.prev = tileMap[y,x];
										tileMap[y,x+1].tileNeighbourCheck.moveStep = moveNum;
										tileMap[y,x+1].tileNeighbourCheck.isChecked = true;
										tilePath.Add(tileMap[y,x+1]);

										if(tileMap[y,x+1] == selectedTile)
										{
											targetReached = true;
											break;
										}
									}
								}
							}
						}
					}

					if(x > 0)
					{
						//Check west side
						if(tileMap[y,x-1] != null)
						{
							if(!tileMap[y,x-1].tileNeighbourCheck.isOccupied)
							{
								if(tileMap[y,x].tileNeighbourCheck.isWestConnectable && tileMap[y,x-1].tileNeighbourCheck.isEastConnectable 
									&& tileMap[y,x-1] != node)
								{
									if(!tileMap[y,x-1].tileNeighbourCheck.isChecked)
									{
										tileMap[y,x-1].tileNeighbourCheck.prev = tileMap[y,x];
										tileMap[y,x-1].tileNeighbourCheck.moveStep = moveNum;
										tileMap[y,x-1].tileNeighbourCheck.isChecked = true;
										tilePath.Add(tileMap[y,x-1]);

										if(tileMap[y,x-1] == selectedTile)
										{
											targetReached = true;
											break;
										}
									}
								}
							}
						}
					}

					tileMap[y,x].tileNeighbourCheck.allSidesChecked = true;
				}
			}

			moveNum++;
		}

		tilePath = new List<TileScript>();

		bool exitLoop = false;
		tilePath.Add(selectedTile);
		TileScript tile = selectedTile.tileNeighbourCheck.prev;

		while(!exitLoop)
		{
			if(tile != node)
			{
				tile.GetComponent<MeshRenderer>().material.color = Color.cyan;
				tilePath.Add(tile);
				tile = tile.tileNeighbourCheck.prev;
			}
			else
			{
				canMove = true;
				exitLoop = true;
			}
		}

	}

	void MoveToTile()
	{
		if(tilePath.Count > 0)
		{
			player.position = Vector3.MoveTowards(player.position, tilePath[tilePath.Count - 1].transform.position + new Vector3(0, 0.5f, 0), 10f * Time.deltaTime);
			currTile = tilePath[tilePath.Count - 1];

			if(player.position == tilePath[tilePath.Count - 1].transform.position + new Vector3(0, 0.5f, 0))
			{
				tilePath.RemoveAt(tilePath.Count - 1);
			}
		}
		else
		{
			canMove = false;
			RecolourTile();
		}
	}

	public void RecolourTile()
	{
		for(int x = 0; x < mapSize.x; x++)
		{
			for(int y = 0; y < mapSize.y; y++)
			{
				tileMap[y,x].GetComponent<MeshRenderer>().material.color = Color.white;
			}
		}
	}
}
