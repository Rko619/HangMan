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
	private GameObject hintPanel;
    [SerializeField]
    private GameObject hintButtonObj;
	private bool isSoundEnabled=true;
	private bool isHintVisible = false;


	void Update()
	{

	}
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

	public void OnClickedHintButton()
	{
		if (!isHintVisible) 
		{
            hintButtonObj.GetComponent<Image>().color = new Color(hintButtonObj.GetComponent<Image>().color.g, hintButtonObj.GetComponent<Image>().color.g, hintButtonObj.GetComponent<Image>().color.b, 50);
            hintButtonObj.GetComponent<Button>().interactable = false;
			hintPanel.SetActive (true);
			isHintVisible = true;
		}
		else
		{
			hintPanel.SetActive (false);
			isHintVisible = false;
		}
	}
	public void  OnClickedHintCloseButton()
	{
		if (isHintVisible) 
		{
			hintPanel.SetActive (false);
			isHintVisible = false;
            hintButtonObj.GetComponent<Image>().color = new Color(hintButtonObj.GetComponent<Image>().color.g, hintButtonObj.GetComponent<Image>().color.g, hintButtonObj.GetComponent<Image>().color.b, 255);
            hintButtonObj.GetComponent<Button>().interactable = true;
        }
	}
}
