using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadSceneScript : MonoBehaviour {

	public string MainMenuSceneName;
	// Use this for initialization
	void Start ()
	{
		SceneLoader.instance.LoadScene (MainMenuSceneName,false);
	}
	
	
}
