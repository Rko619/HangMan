using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {



    [SerializeField]
    private Image speakerButtonImage;
    [SerializeField]
    private Sprite normalSpeakerSprite, disabledSpeakerSprite;
    [SerializeField]
    private GameObject hintPanel;
    [SerializeField]
    private GameObject hintButtonObj;
    private bool isSoundEnabled = true;
    private bool isHintVisible = false;
    [SerializeField]
    private Text highScoreText;
	[SerializeField]
	private string gamePlayScene;





	void Start()
	{
		highScoreText.text = "HIGHSCORE : " + GameManager.instance.GetHighScore ().ToString ();

	}
    void OnEnable()
    {
		if (GameManager.instance&&AudioManager.instance)
		{
			highScoreText.text = "HIGHSCORE : " + GameManager.instance.GetHighScore ().ToString ();
            UpdateSoundIconStatus();
        }
    }
	public void OnClickedPlayButton()
	{
		SceneLoader.instance.LoadScene (gamePlayScene);
		//GameManager.instance.StartGame ();
	}
	public void OnClickedSettingsButton()
	{
        GameManager.instance.QuitGame();
    }
	public void OnClickedQuitButton()
	{
		GameManager.instance.QuitGame ();
	}

    public void OnClickedSpeakerButton()
    {
        if (isSoundEnabled)
        {
            AudioManager.instance.MuteSounds();
            speakerButtonImage.sprite = disabledSpeakerSprite;
            isSoundEnabled = false;
        }
        else
        {
            AudioManager.instance.UnmuteSounds();
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
            hintPanel.SetActive(true);
            isHintVisible = true;
        }
        else
        {
            hintPanel.SetActive(false);
            isHintVisible = false;
        }
    }
    public void OnClickedHintCloseButton()
    {
        if (isHintVisible)
        {
            hintPanel.SetActive(false);
            isHintVisible = false;
            hintButtonObj.GetComponent<Image>().color = new Color(hintButtonObj.GetComponent<Image>().color.g, hintButtonObj.GetComponent<Image>().color.g, hintButtonObj.GetComponent<Image>().color.b, 255);
            hintButtonObj.GetComponent<Button>().interactable = true;
        }
    }

    void UpdateSoundIconStatus()
    {
        if(AudioManager.instance.isAudioMuted)
        {
            speakerButtonImage.sprite = disabledSpeakerSprite;
            isSoundEnabled = false;
        }
        else
        {
            speakerButtonImage.sprite = normalSpeakerSprite;
            isSoundEnabled = true;
        }
    }


}
