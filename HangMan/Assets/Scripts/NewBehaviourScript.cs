using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	private string jstring;
	private JsonData itemData;
	void Start () {
		jstring=File.ReadAllText (Application.dataPath + "/Resources/Sample.json");
		Debug.Log(jstring);
		itemData = JsonMapper.ToObject (jstring);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
