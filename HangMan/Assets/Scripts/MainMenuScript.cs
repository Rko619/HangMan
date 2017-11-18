﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    private string gamePlaySceneName;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnClickedPlayButton()
	{
        SceneLoader.instance.LoadScene(gamePlaySceneName);
	}
	public void OnClickedSettingsButton()
	{
        GameManager.instance.QuitGame();
    }
	public void OnClickedQuitButton()
	{
		GameManager.instance.QuitGame ();
	}
}
