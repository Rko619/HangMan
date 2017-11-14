using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextObjScript : MonoBehaviour {

	// Use this for initialization
	public char correctLetter;

	[SerializeField]
	private Color correctTextColor;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void DisplayCorrectLetter()
	{
		GetComponent<Text> ().text = correctLetter.ToString ();
		GetComponent<Text> ().color = correctTextColor;
	}
}
