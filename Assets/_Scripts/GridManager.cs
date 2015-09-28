using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
				//RectTransform rectTrans = i.GetComponent<RectTransform> ();


				//rectTrans.offsetMin = new Vector2 (x, y);

			}

			}
	}






}
