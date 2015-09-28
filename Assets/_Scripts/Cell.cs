using UnityEngine;
using System.Collections;
using System;
public enum Sprite_Type{
	Red,
	Blue,
	Green,
	Yellow,
	Purple,

}
public class Cell : MonoBehaviour {

	public SpriteRenderer _sr;
	public int Column { get; set; }
	public int Row { get; set; }
	public int TileType { get; set; }
	public string Type { get; set; }
	public bool IsMoving;
	public Sprite sprite;

	void Start () {

		_sr= gameObject.transform.FindChild ("Sprite").GetComponent<SpriteRenderer> () as SpriteRenderer;

		_sr.sprite = sprite;

	}
	

	void Update () {
		
	
	}
	public bool IsSameType(Cell otherShape)
	{
		if (otherShape == null || !(otherShape is Cell))
			throw new ArgumentException("otherShape");

		return string.Compare(this.Type, (otherShape as Cell).Type) == 0;
	}
	/// <param name="column"></param>
	public void Assign(string type, int row, int column)
	{

		if (string.IsNullOrEmpty(type))
			throw new ArgumentException("type");

		Column = column;
		Row = row;
		Type = type;
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


}
