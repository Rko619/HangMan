using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {


	public void OnClickedPlayButton()
	{
		GameManager.instance.StartGame ();
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
