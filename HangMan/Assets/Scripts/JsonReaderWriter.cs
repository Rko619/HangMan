using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public static class JsonReaderWriter  {

	// Use this for initialization
	private static string jsonString;
	private static JsonData wordsData;
	public static void InitializeJson ()
	{
		jsonString=File.ReadAllText (Application.dataPath + "/Resources/Sample.json");
	}
	
	// Update is called once per frame
	public static string[]  ReadFromJson (string category) 
	{
		InitializeJson();
		wordsData = JsonMapper.ToObject (jsonString);
		string[] wordArray=new string[wordsData[category].Count];
		for(int i=0;i<wordArray.Length;i++)
			{
				wordArray[i]=wordsData[category][i].ToString();
			}
		return(wordArray);
	

	}
	public static int GetNumberOfWords(string category)
	{
		InitializeJson();
		wordsData = JsonMapper.ToObject (jsonString);
		int wordCount=wordsData[category].Count;
		return(wordCount);
	}

	
}
