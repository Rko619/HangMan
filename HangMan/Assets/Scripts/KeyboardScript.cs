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
            AnimatedTowardsOutputPanel(correspondingCorrectCharObj);
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
		GetComponent<Button> ().image.color = fadeColor;
	}
	void DisableButton()
	{
		GetComponent<Button> ().interactable = false;
		GetComponent<Button> ().image.color = fadeColor;
	}
	public void ResetKeys()
	{
		GetComponent<Button> ().interactable = true;
		GetComponent<Button> ().image.color = defaultColor;
	}
    IEnumerator AnimatedTowardsOutputPanel(GameObject alphabetObj)
    {
        Debug.Log("animating ");
        yield return new WaitForSeconds(0f);
    }
}
