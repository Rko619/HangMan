using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyboardScript : MonoBehaviour {

	public enum ButtonStates {Highlight,Disable};

	[SerializeField]
	private char inputKey;
	[SerializeField]
	private GameMode gameMode;
	[SerializeField]
	private Color fadeColor,defaultColor;
    [SerializeField]
    private string correctButtonPressSoundName,wrongButtonPressSoundName;


    void Start ()
    {
		Button Key = GetComponent<Button> ();
		Key.onClick.AddListener (SetInputKey);
	}
	
	public void SetInputKey()
	{
		gameMode.OnKeyPressed (this.gameObject, inputKey);
	}
	public void HighlightOrDisable(ButtonStates currentButtonState,GameObject correspondingCorrectCharObj)
	{
        if (currentButtonState == ButtonStates.Highlight)
        {
            AudioManager.instance.PlaySound(correctButtonPressSoundName,false);
			AnimateScaleDown();
            HighlightButton();
        }
        else
        {
            AudioManager.instance.PlaySound(wrongButtonPressSoundName,false);
			AnimateScaleDown();
            DisableButton();
        }
			
	}
	void HighlightButton()
	{
		GetComponent<Button> ().interactable = false;
		//GetComponent<Button> ().image.color = fadeColor;
	}
	void DisableButton()
	{
		GetComponent<Button> ().interactable = false;
		//GetComponent<Button> ().image.color = fadeColor;
	}

	public void AnimateScaleDown()
	{
		iTween.ScaleTo(gameObject,new Vector3(0,0,0),2f);
		GetComponent<Button> ().interactable = false;

	}
	public void AnimateScaleUp()
	{
		iTween.ScaleTo(gameObject,new Vector3(1,1,1),2f);
		GetComponent<Button> ().interactable = true;

	}

	public void ResetKeys()
	{
		AnimateScaleUp();
		GetComponent<Button> ().interactable = true;
		GetComponent<Button> ().image.color = defaultColor;
	}
 
}
