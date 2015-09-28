using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Grid : MonoBehaviour {
	

	public int HEIGHT=4;
	public int WIDTH=4;
	public int[,] grid;
	public GameObject Cell_Prefab;
	private GameObject gridImage;
//	public GameObject swap1;
	//public GameObject swap2;
	[SerializeField]
	//public  Color [] tile_colors = new Color[5];
	//public GameObject [] tiles = new GameObject[5];
	public Sprite[] sprites = new Sprite[5];
	// Use this for initialization
	void Start () {
		Spawn_Tiles ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}




	void Spawn_Tiles(){
		
			grid = new int[WIDTH, HEIGHT];
	
			
		for (int x = 0; x < WIDTH; x++) {
			for(int y =0;y<HEIGHT;y++){
					

		
				int randomTile = Random.Range (0, sprites.Length);
					grid[x,y] = randomTile;

				var go = Instantiate (Cell_Prefab, new Vector2 (x, y), Quaternion.identity) as GameObject;
				go.GetComponent<Cell> ().sprite = sprites [randomTile];
					go.name = "("+x+" , "+y+")";
				//print (i + " , " + j);
			}

		}

	}
	/*
	private void InitCell(Cell cell)
	{
		int colorIndex = Random.Range(0, tile_colors.Length);

		cell.TileType = colorIndex; // TileColorExtensions.SelectRandom();
		cell.Color = tile_colors[colorIndex];
		cell.IsMoving = false;
	}
*/

}
