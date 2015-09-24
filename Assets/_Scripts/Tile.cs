using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(BoxCollider2D))]
public class Tile:MonoBehaviour {
	//private GridManager gridManager;
	//public float scale;
	public Sprite sprite;


	void Awake ()
	{
		//sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
		//scale = transform.localScale.x;
		//gridManager = GetComponent<GridManager>();
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
	void Start () {
		//scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {

	
	}

}
