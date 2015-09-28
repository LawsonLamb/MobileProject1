using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System;
//[RequireComponent(typeof(BoxCollider2D))]

public class Tile:MonoBehaviour {
	
	[SerializeField] private Color color;

	private Vector3 screenPoint;
	private Vector3 offset;
	public string type{ get; set; }

	void Awake ()
	{
		//sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
		//scale = transform.localScale.x;
		//gridManager = GetComponent<GridManager>();
	}
	void Update () {


	}
	public void Assign(int row, int column)
	{

		//Column = column;
		//Row = row;
	
	}

	public void Move (Vector2 destination)
	{
		gameObject.transform.position = destination; // provisional

	}

	/*
	void OnMouseEnter(){
		scale = 1.5f;
		transform.localScale = new Vector3 (scale, scale, transform.localScale.z);

	}
	void OnMouseExit(){
		scale = 1.0f;
		transform.localScale = new Vector3 (scale, scale, transform.localScale.z);
	}
	
	*/	

	// Use this for initialization

	void OnMouseDown() {
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


	void OnMouseDrag()

	{
		
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		gameObject.transform.position = curPosition;

	}
	// Update is called once per frame
	/*
	public static void SwapColumnRow(Tile a, Tile b)
	{
		int temp = a.Row;
		a.Row = b.Row;
		b.Row = temp;

		temp = a.Column;
		a.Column = b.Column;
		b.Column = temp;
	}
	*/






}
