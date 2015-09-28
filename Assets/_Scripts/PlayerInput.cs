using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	private GridManager gridManager;
	public LayerMask Tiles;
	private GameObject activeTile;

	void Awake ()
	{
		gridManager = GetComponent<GridManager> ();
	}

	void Update ()

	{ 
		/*
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{	/*
			if (activeTile == null)
				SelectTile ();
			else 
				//AttemptMove ();
			
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit) {

				hit.collider.gameObject.GetComponent<Tile> ().Scale_UP ();
			
			}
		} 

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit) {

			activeTile = hit.collider.gameObject;
			//activeTile.GetComponent<Tile> ().Scale_UP ();

		} 

		else {
			
	

	}
	*/
	}

	void SelectTile ()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


		if (hit) {
			
			activeTile = hit.collider.gameObject;
			//activeTile.GetComponent<Tile> ().Scale_Anim ();
		}

	}


		
}
