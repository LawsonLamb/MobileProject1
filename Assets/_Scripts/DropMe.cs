using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	public GridManager gridmanger;
	private Color normalColor;
	public Color highlightColor = Color.yellow;
	public int Column { get; set; }
	public int Row { get; set; }
	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;

	}
	 void Start(){
		gridmanger = gameObject.transform.parent.GetComponent<GridManager> ();
		int randomTile = Random.Range (0, gridmanger.sprites.Length);
		receivingImage.sprite = gridmanger.sprites [randomTile];
	
	}

	void Update(){

	}
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			Swap_Sprites(data,dropSprite);

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
		
		var dragMe = originalObj.GetComponent<DragMe>();
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
		//sets image to temp
		//then assigns the sprite from dropped object image
		//then we set the image of the droped sprite to the sprite that in the cell
		Sprite temp = receivingImage.sprite;
		receivingImage.sprite = sprite;
		srcImage.sprite = temp;
	
	}



}
