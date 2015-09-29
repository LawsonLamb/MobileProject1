using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
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
	{	
		Grid = new GameObject[GridWidth, GridHeight];
		
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

	public void GetMatches(GameObject go){

			List<GameObject> h_matches =Check_Cols(go);
			List<GameObject> verticalMatches = Check_ROWs(go);
				
		foreach (GameObject h in h_matches){
			print ("MATCHED: "+ h.name);
			h.GetComponent<Cell>().Remove_Tile();

		}
		foreach(GameObject v in verticalMatches){
			print ("MATCHED: "+ v.name);
			v.GetComponent<Cell>().Remove_Tile();
		}


	
	}
	[ContextMenu("Check Matches")]
	public void CheckEntireGridForMatches(){

		for (int x = 0; x < GridWidth; x++) {
			for (int y = 0; y < GridHeight; y++){


					GetMatches(Grid[x,y]);
					
			}
		}
	}

	void Update(){



	}

	private List<GameObject> Check_Cols(GameObject go){

	List<GameObject> matches = new List<GameObject>();
		matches.Add (go);
		var cell = go.GetComponent<Cell>();

		if (cell.Column!= 0){
			for (int column = cell.Column - 1; column >= 0; column--)
			{
				if (Grid[cell.Row, column].GetComponent<Cell>().type == cell.type)
				{
					matches.Add(Grid[cell.Row, column]);
					print ("match col: "+ Grid[cell.Row, column].name);
					Grid[cell.Row, column].GetComponent<Cell>().containerImage.color = Color.green;
				}
				else
					break;
			}

		}

		if (cell.Column != GridHeight - 1){
			for (int column = cell.Column + 1; column < GridHeight; column++){
			if (Grid[cell.Row, column].GetComponent<Cell>().type ==  cell.type)
			{
				matches.Add(Grid[cell.Row, column]);
				print ("match col : "+ Grid[cell.Row, column].name);
				Grid[cell.Row, column].GetComponent<Cell>().containerImage.color = Color.green;
			}
			else
				break;
			}
		}

		if (matches.Count <= 3)
			matches.Clear();

		return matches;
	}

	private List<GameObject> Check_ROWs(GameObject go){
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var cell = go.GetComponent<Cell>();
		if (cell.Row != 0){
			for (int row = cell.Row - 1; row >= 0; row--)
		{
			if (Grid[row, cell.Column] != null &&( Grid[row, cell.Column].GetComponent<Cell>().type== cell.type))
			{
				matches.Add(Grid[row, cell.Column]);
					print ("match row : "+ Grid[row, cell.Column].name);
					Grid[row, cell.Column].GetComponent<Cell>().containerImage.color = Color.green;
			}
			else
				break;
		}
		}
		
		//check top
		if (cell.Row != GridWidth - 1){
			for (int row = cell.Row + 1; row < GridWidth; row++)
		{
			if (Grid[row, cell.Column] != null &&  (Grid[row, cell.Column].GetComponent<Cell>().type== cell.type))
			{
				matches.Add(Grid[row, cell.Column]);
					print ("match row : "+ Grid[row, cell.Column].name);
					Grid[row, cell.Column].GetComponent<Cell>().containerImage.color = Color.green;
			}
			else{
				break;
				}
		}
		}

		if (matches.Count <= 3)
			matches.Clear();

		return matches;
		
	}
	/*
	private IEnumerable<GameObject> GetEntireRow(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		int row = go.GetComponent<Cell>().Row;
		for (int column = 0; column < GridHeight; column++)
		{
			matches.Add(Grid[row, column]);
		}
		return matches;
	}
	
	private IEnumerable<GameObject> GetEntireColumn(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		int column = go.GetComponent<Cell>().Column;
		for (int row = 0; row < GridWidth; row++)
		{
			matches.Add(Grid[row, column]);
		}
		return matches;
	}
*/

	
}
