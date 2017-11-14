using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyboardScript : MonoBehaviour {

	// Use this for initialization
	public enum ButtonStates {Highlight,Disable};

	[SerializeField]
	private char inputKey;
	[SerializeField]
	private GameMode gameMode;
	[SerializeField]
	private Color highlightColor,disabledColor; 

	void Start () {
		Button Key = GetComponent<Button> ();
		Key.onClick.AddListener (SetInputKey);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
	public void SetInputKey()
	{
		gameMode.OnKeyPressed (this.gameObject, inputKey);
	}
	public void HighlightOrDisable(ButtonStates currentButtonState)
	{
		if (currentButtonState == ButtonStates.Highlight)
			HighlightButton ();
		else
			DisableButton ();
			
	}
	void HighlightButton()
	{
		GetComponent<Button> ().interactable = false;
		GetComponent<Button> ().image.color = highlightColor;
	}
	void DisableButton()
	{
		GetComponent<Button> ().interactable = false;
		GetComponent<Button> ().image.color = disabledColor;
	}
}
