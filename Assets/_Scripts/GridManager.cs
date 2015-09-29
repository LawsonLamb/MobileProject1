using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class GridManager : MonoBehaviour {

	public int GridWidth;
	public int GridHeight;
	public GameObject[,] Grid;
	public Sprite[] sprites = new Sprite[5];
	public GameObject prefab;
	//public Canvas canvas;
	public GameObject panel;




	[ContextMenu("Make Grid")]
	void Start ()
	{	Grid = new GameObject[GridWidth, GridHeight];
		
		//RectTransform canvasRect_Trans = canvas.GetComponent<RectTransform> ();

		for (int x = 0; x < GridWidth; x++) {
			for (int y = 0; y < GridHeight; y++) {
				Grid[x,y] = Instantiate (prefab) as GameObject;
				Grid[x,y].transform.SetParent (panel.transform, false);
				Grid[x,y].name = "" + x + "," + y;
				Grid[x,y].GetComponent<Cell>().Assign(x,y);

				//RectTransform rectTrans = i.GetComponent<RectTransform> ();


				//rectTrans.offsetMin = new Vector2 (x, y);

			}

			}

	}
	[ContextMenu("New Grid")]
	void Respawn_Tiles(){

		
		for (int x = 0; x < GridWidth; x++) {
			for (int y = 0; y < GridHeight; y++) {
				Transform tile = Grid[x,y].transform.FindChild("Sprite");
				int randomTile = Random.Range (0,sprites.Length);
				tile.GetComponent<Image>().sprite = sprites [randomTile];

			}

	}

	

}

	void CheckMatches(){

		
		for (int x = 0; x < GridWidth; x++) {
			for (int y = 0; y < GridHeight; y++) {



			}
		}
	


	}

	private void Check_Cols(GameObject go){

	List<GameObject> matches = new List<GameObject>();

		var cell = go.GetComponent<Cell>();

		if (cell.Column!= 0){
			for (int column = cell.Column - 1; column >= 0; column--)
			{
				if (Grid[cell.Row, column].GetComponent<Cell>().type == cell.type)
				{
					matches.Add(Grid[cell.Row, column]);
				}
				else
					break;
			}

		}

		if (cell.Column != GridWidth - 1)
			for (int column = cell.Column + 1; column < GridWidth; column++)
		{
			if (Grid[cell.Row, column].GetComponent<Cell>().type ==  cell.type)
			{
				matches.Add(Grid[cell.Row, column]);
			}
			else
				break;
		}


	}




}
