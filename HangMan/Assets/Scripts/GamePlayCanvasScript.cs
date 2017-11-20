using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayCanvasScript : MonoBehaviour {

	[SerializeField]
	private Image speakerButtonImage;
	[SerializeField]
	private Sprite normalSpeakerSprite, disabledSpeakerSprite;
	[SerializeField]
	private GameObject hintPanel;
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
			hintPanel.SetActive (true);
			isHintVisible = true;
		}
	}
	public void  OnClickedHintCloseButton()
	{
		if (isHintVisible) 
		{
			hintPanel.SetActive (false);
			isHintVisible = false;
		}
	}
}
