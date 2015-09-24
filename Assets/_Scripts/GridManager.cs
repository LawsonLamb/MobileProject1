using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	public int GridWidth;
	public int GridHeight;
	public int[,] Grid;
	//public GameObject Background;

	public GameObject[] TilePrefabs;
	[ContextMenu("Make Grid")]
	void Awake ()
	{	CreateGrid ();
		//CreateGridBackground ();

	}

	void CreateGrid ()
	{
		Grid = new int[GridWidth, GridHeight];

		for (int x = 0; x < GridWidth; x++)
		{
			for (int y= 0; y < GridHeight; y++)
			{
				int randomTile = Random.Range (0, TilePrefabs.Length);
				Grid[x,y] = randomTile;
				Instantiate (TilePrefabs[randomTile], new Vector2 (x,y), Quaternion.identity);
			}
		}
	}
	/*
	void CreateGridBackground(){
		Grid = new int[GridWidth, GridHeight];

		for (int x = 0; x < GridWidth; x++)
		{
			for (int y= 0; y < GridHeight; y++)
			{
				
			
			
				Instantiate (Background, new Vector2 (x,y), Quaternion.identity);
			}
		}

	}
	*/
	public void CheckMatches()
	{
		List<Vector2> matchPositions = new List<Vector2>();

		int currentTile = 0;
		int lastTile = 0;
		int secondToLastTile = 0;

		for (int x = 0; x < GridWidth; x++)
		{
			for (int y = 0; y < GridHeight; y++)
			{
				currentTile = Grid[x,y];

				if (currentTile == lastTile && lastTile == secondToLastTile)
						matchPositions.Add (new Vector2 (x, y));

						secondToLastTile = lastTile;
						lastTile = currentTile;
						
					}
						// Same for horizontal
						}

					if (matchPositions.Count > 0)
						{
							List<GameObject> tilesToDestroy = new List<GameObject>();
							foreach (Vector2 tilePosition in matchPositions)
							{
								// Raycast
							}
							// Destroy ( set to 0, send message oder call method?)
						}

					//Try it! Match 3s will now vanish... but there's still something missing of course: The tiles above moving down and new tiles being created. Let's do that!
	}
						void ReplaceTiles ()
						{
							for (int x = 0;x < GridWidth; x++)
							{
								int missingTileCount = 0;
								for (int y = 0; y < GridHeight; y++)
								{ 
									if (Grid[x,y] == -1)
										missingTileCount++;
								}
								for (int i = 0; i < missingTileCount; i++)
								{
									int randomTileID = Random.Range (0, TilePrefabs.Length);
									Instantiate (TilePrefabs[randomTileID], new Vector2 (x, GridHeight + i), Quaternion.identity);
								}
							}
					}
}
