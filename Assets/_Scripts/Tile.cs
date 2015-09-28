using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//[RequireComponent(typeof(BoxCollider2D))]

public class Tile: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{	
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	

	public void OnBeginDrag(PointerEventData eventData){
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = Input.mousePosition;
	}

	private void SetDraggedPosition(PointerEventData eventData){

	}

	public void OnEndDrag(PointerEventData eventData){
		
		//itemBeingDragged = null;
		//transform.position = startPosition;
	}



}
