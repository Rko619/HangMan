using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class GamePlayCanvasScript : MonoBehaviour {

	[SerializeField]
	private Image speakerButtonImage;
	[SerializeField]
	private Sprite normalSpeakerSprite, disabledSpeakerSprite;
	[SerializeField]
	private GameObject yesnoPanel;
    [SerializeField]
	private GameObject backButtonObj;
	private bool isSoundEnabled=true;


	public void OnClickedSpeakerButton()
	{
		if (isSoundEnabled)
		{
			AudioManager.instance.StopSound ();
			speakerButtonImage.sprite = disabledSpeakerSprite;
			isSoundEnabled = false;
		}
		else 
		{
			AudioManager.instance.PlayStoppedSounds ();
			speakerButtonImage.sprite = normalSpeakerSprite;
			isSoundEnabled = true;
		}
	}

	public void OnClickedBackButton()
	{
        backButtonObj.GetComponent<Image>().color = new Color(backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.b, 50);
        backButtonObj.GetComponent<Button>().interactable = false;
		yesnoPanel.SetActive (true);
	}
	public void  OnClickedYesButton()
	{
		backButtonObj.GetComponent<Button>().interactable = true;
		yesnoPanel.SetActive (false);
		GameManager.instance.OnClickedMainMenuButton ();

	}
	public void  OnClickedNoButton()
	{
		yesnoPanel.SetActive (false);
		backButtonObj.GetComponent<Image>().color = new Color(backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.b, 255);
		backButtonObj.GetComponent<Button>().interactable = true;
	}
}
