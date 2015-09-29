using UnityEngine;
using System.Collections;

public class UI_MANAGER : MonoBehaviour {

	public string GameScene;
	public string HighScoreScene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadGameScene(){

		Application.LoadLevel(GameScene);
	}
	public void LoadHighScoreScene(){
		Application.LoadLevel(HighScoreScene);
	}
}
