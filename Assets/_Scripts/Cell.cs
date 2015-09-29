using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
public enum Sprite_Type{
	Red =0,
	Blue=1,
	Green =2 ,
	Yellow = 3,
	Purple = 4,

}
public class Cell : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler{
	public int type;
	public int Type{get{return type;} set{ type = value;}}
	//public SpriteRenderer _sr;
	public int Column;
	public int Row;
	//public int TileType { get; set; }
	public Image containerImage;
	public Image receivingImage;
	public GridManager gridmanger;
	private Color normalColor;
	public Color highlightColor = Color.yellow;


	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
		
	}
	void Start(){
		gridmanger = gameObject.transform.parent.GetComponent<GridManager> ();
		int randomTile = Random.Range (0, gridmanger.sprites.Length);
		receivingImage.sprite = gridmanger.sprites [randomTile];
		type  = randomTile;
		
	}
	
	void Update(){
		
	}
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null){
			Swap_Sprites(data,dropSprite);
			gridmanger.GetMatches(gameObject);

		}
		//receivingImage.overrideSprite = dropSprite;
	}
	
	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			containerImage.color = highlightColor;
	}
	
	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<Tile>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}
	private void Swap_Sprites(PointerEventData eventData,Sprite sprite){
		var originalObj = eventData.pointerDrag;
		var srcImage = originalObj.GetComponent<Image>();
		var cell =  originalObj.GetComponent<Cell>();

		//sets image to temp
		//then assigns the sprite from dropped object image
		//then we set the image of the droped sprite to the sprite that in the cell
		/*
		int tempTYPE = this.Type;
		int tempType2 = (int)cell.Type;
		this.Type = tempType2;
		cell.Type = tempTYPE;
		*/


		Sprite temp = receivingImage.sprite;
		receivingImage.sprite = sprite;
		srcImage.sprite = temp;
		
	}
	[ContextMenu("Check")]
	 void Check_Vertical(){

		int temp1 = Row+1;
		int temp2 = Row-1;
		print ("check vert on : "+" ( "+  gameObject.name+") " + "row+1: "+ temp1+"  row-1: "+ temp2);
	

		if(gridmanger.Grid[Row+1,Column].GetComponent<Cell>().receivingImage.Equals(this.receivingImage)){

			print (gameObject.name+ " : " + "vert_match" + Row+1);
		}

		if(gridmanger.Grid[Row+1,Column].GetComponent<Cell>().type ==type){

			print (gameObject.name+ " : " + "vert_match" + Row+1);

		}

		
		if(gridmanger.Grid[Row-1,Column].GetComponent<Cell>().receivingImage ==receivingImage){
			int temp =  Row-1;
			print (gameObject.name+ " : " + "vert_match" + temp.ToString());
			
		}

	}
	public void Assign(int row,int col){
		Row = row;
		Column = col;

	}

	[ContextMenu("PrintGrid")]
	void printGridTEST(){

		
		for (int x = 0; x < gridmanger.GridWidth ; x++) {
			for (int y = 0; y < gridmanger.GridHeight; y++) {
				string temp = gridmanger.Grid[x,y].gameObject.name;
				print (temp);
			}
		}


	}
	public void  Remove_Tile(){

		print ("add points");
		int randomTile = Random.Range (0, gridmanger.sprites.Length);
		receivingImage.sprite = gridmanger.sprites [randomTile];
		type  = randomTile;
	}

}

