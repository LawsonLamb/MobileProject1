using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	public Transform tile;
	private Sprite _sprite;
	private SpriteRenderer _sr;

	// Use this for initialization
	void Start () {
		tile = gameObject.transform.GetChild (0);
		_sr = gameObject.GetComponent<SpriteRenderer> ();
		//SET_SPRITE ();


	}
	
	// Update is called once per frame
	void Update () {
		//tile = gameObject.GetComponent<Tile> ();
		//SET_SPRITE ();
	
	}

	void SET_SPRITE(){
		
	

	}



}
