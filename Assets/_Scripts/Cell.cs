using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
public enum Sprite_Type{
	Red,
	Blue,
	Green,
	Yellow,
	Purple,

}
public class Cell : MonoBehaviour,IDropHandler {

	//public SpriteRenderer _sr;
	public int Column { get; set; }
	public int Row { get; set; }
	//public int TileType { get; set; }
	//public string Type { get; set; }
	//public bool IsMoving;
	public Sprite sprite;
	public Image image;
	public GridManager gridmanger;

	void Start () {

		//	_sr= gameObject.transform.FindChild ("Sprite").GetComponent<SpriteRenderer> () as SpriteRenderer;
		image = gameObject.transform.FindChild ("Sprite").GetComponent<Image> ();
		gridmanger = gameObject.transform.parent.GetComponent<GridManager> ();
		int randomTile = Random.Range (0, gridmanger.sprites.Length);
		image.sprite = gridmanger.sprites [randomTile];

	//	_sr.sprite = sprite;

	}
	public Transform item {
		get{
			if (transform.childCount > 0) {
				return transform.GetChild (0);
			}
			return null;
				}

	}

	void Update () {
		image = gameObject.transform.FindChild ("Sprite").GetComponent<Image> ();
	
	}

	public void OnDrop(PointerEventData eventData){
		/*
		if (!item) {
			Tile.itemBeingDragged.transform.SetParent (transform);
		}
		*/

		Sprite DropSrite = GetDropSprite (eventData);
		//Swap_Sprites (eventData, DropSrite);
			
		
	}
	/*
	public bool IsSameType(Cell otherShape)
	{
		if (otherShape == null || !(otherShape is Cell))
			throw new ArgumentException("otherShape");

		return string.Compare(this.Type, (otherShape as Cell).Type) == 0;
	}

*/
	/// <param name="column"></param>
	public void Assign(string type, int row, int column)
	{

		if (string.IsNullOrEmpty(type))
			throw new ArgumentException("type");

		Column = column;
		Row = row;
		//Type = type;
	}

	public static void SwapColumnRow(Cell a, Cell b)
	{
		int temp = a.Row;
		a.Row = b.Row;
		b.Row = temp;

		temp = a.Column;
		a.Column = b.Column;
		b.Column = temp;
	}

	private Sprite GetDropSprite(PointerEventData eventData){
		var originalObj = eventData.pointerDrag;
		if (originalObj == null)
			return null;

		var cell = originalObj.GetComponent<Cell>();
		if (cell == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Cell> ().image;
		if (srcImage == null)
			return null;
		Sprite temp = image.sprite;
		print (temp.name);
		image.sprite = sprite;
		print (image.sprite.name);
		srcImage.sprite = temp;
		print (temp.name);
		print (srcImage.sprite.name);
		return srcImage.sprite;
	}

	private void Swap_Sprites(PointerEventData eventData,Sprite sprite){
		var originalObj = eventData.pointerDrag;
		var srcImage = originalObj.GetComponent<Cell> ().image;
		//sets image to temp
		//then assigns the sprite from dropped object image
		//then we set the image of the droped sprite to the sprite that in the cell
		Sprite temp = image.sprite;
		image.sprite = sprite;
		srcImage.sprite = temp;



	




	}

	}

