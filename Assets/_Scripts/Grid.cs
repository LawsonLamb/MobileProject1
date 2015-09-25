using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Grid : MonoBehaviour {
	

	public int HEIGHT=4;
	public int WIDTH=4;
	public int[,] grid;
	public GameObject gridImage;
//	public GameObject swap1;
	//public GameObject swap2;
	public GameObject[] tiles = new GameObject[5];

	GameObject parent;
	// Use this for initialization
	void Start () {
		//Create_Grid ();
		Spawn_Tiles ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[ContextMenu("Create Grid")]
	void Create_Grid(){
		grid= new int[WIDTH,HEIGHT];
		for (int i = 0; i < WIDTH; i++) {
			for(int j =0;j<HEIGHT;j++){
				//int randomTile = Random.Range (0, tiles.Length);
				//grid[i,j] = randomTile;
			
				Instantiate (gridImage, new Vector2 (i,j), Quaternion.identity);
				//print (i + " , " + j);
			}

		}


	}
	void Spawn_Tiles(){
		
			grid = new int[WIDTH, HEIGHT];
	
			
		for (int i = 0; i < WIDTH; i++) {
			for(int j =0;j<HEIGHT;j++){
				int randomTile = Random.Range (0, tiles.Length);
				grid[i,j] = randomTile;
				  Instantiate (tiles[randomTile], new Vector2 (i,j), Quaternion.identity);
				tiles[randomTile].name = "("+i+" , "+j+")";
				//print (i + " , " + j);
			}

		}

	}

}
