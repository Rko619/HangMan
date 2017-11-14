using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour {

	// Use this for initialization
	public GameObject MenuPanel;
	public GameObject KeysPanel;
	public GameObject HangPlace;
	public GameObject OutputPanel;
	void Start () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartGame()
	{
		MenuPanel.SetActive (false);
	}
	public void QuitGame()
	{
		Application.Quit ();
	}
	public void GameOver()
	{
		Debug.Log ("GameOver");
	}

	public void LevelCompleted()
	{
		
	}
}
