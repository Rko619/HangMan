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
	private Color highlightColor,disabledColor,defaultColor;
    [SerializeField]
    private string correctButtonPressSoundName,wrongButtonPressSoundName;


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
        {
            AudioManager.instance.PlaySound(correctButtonPressSoundName,false);
            HighlightButton();
        }
        else
        {
            AudioManager.instance.PlaySound(wrongButtonPressSoundName,false);
            DisableButton();
        }
			
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
	public void ResetKeys()
	{
		GetComponent<Button> ().interactable = true;
		GetComponent<Button> ().image.color = defaultColor;
	}
}
