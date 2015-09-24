using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	public Tile tile;
	private Sprite _sprite;
	private SpriteRenderer _sr;

	// Use this for initialization
	void Start () {
	tile = gameObject.GetComponent<Tile> ();
		_sr = gameObject.GetComponent<SpriteRenderer> ();
		SET_SPRITE ();


	}
	
	// Update is called once per frame
	void Update () {
		
		SET_SPRITE ();
	
	}

	void SET_SPRITE(){
		_sprite = tile.sprite;
		_sr.sprite = _sprite;
	

	}



}
