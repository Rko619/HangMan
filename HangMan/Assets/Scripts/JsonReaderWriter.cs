using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public static class JsonReaderWriter  {

	// Use this for initialization
	private static string jsonString;
	private static JsonData wordsData;
	static void InitializeJson ()
	{
		jsonString=File.ReadAllText (Application.dataPath + "/Resources/Sample.json");
	}
	
	// Update is called once per frame
	static void Read () 
	{
		wordsData = JsonMapper.ToObject (jsonString);
		int i=wordsData["Animals"].Count;
		Debug.Log(i);

	}
}
