using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class GamePlayCanvasScript : MonoBehaviour {

	public bool isTimedOut;

	[SerializeField]
	private Image speakerButtonImage;
	[SerializeField]
	private Sprite normalSpeakerSprite, disabledSpeakerSprite;
	[SerializeField]
	private GameObject yesnoPanel;
    [SerializeField]
	private GameObject backButtonObj;
	[SerializeField]
	private Text timerText;
	[SerializeField]
	private Text highScoreText;
	[SerializeField]
	private Text hintText;
	[SerializeField]
	private Button hintButton;
	[SerializeField]
	private Color normalTimeColor;
	[SerializeField]
	private GameObject[] keys;
	[SerializeField]
	private Transform wordNormalPos,wordEndPos,answerTransform;
	[SerializeField]
	private GameObject settingsPanel;
	private bool isSoundEnabled=true;


    void OnEnable()
    {
        if (GameManager.instance && AudioManager.instance)
        {
            UpdateSoundIconStatus();
        }
    }

	void Start()
	{
		hintButton.onClick.AddListener (OnClickedHintButton);

		GameManager.instance.onHighScoreChanged += DisplayHighScore;

		GameManager.instance.onHintValueChanged += DisplayHint;
	}

    public void OnClickedSpeakerButton()
	{
		if (isSoundEnabled)
		{
			AudioManager.instance.MuteSounds ();
			speakerButtonImage.sprite = disabledSpeakerSprite;
			isSoundEnabled = false;
		}
		else 
		{
			AudioManager.instance.UnmuteSounds ();
			speakerButtonImage.sprite = normalSpeakerSprite;
			isSoundEnabled = true;
		}
	}

	public void OnClickedBackButton()
	{
		//pause a game using this line
		GameManager.instance.PauseGame();

        backButtonObj.GetComponent<Image>().color = new Color(backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.b, 50);
        backButtonObj.GetComponent<Button>().interactable = false;
		yesnoPanel.SetActive (true);
	}
	public void  OnClickedYesButton()
	{
		GameManager.instance.OnClickedMainMenuButton ();
	}
	public void  OnClickedNoButton()
	{
		//UnPause a Game using this line
		GameManager.instance.UnPauseGame();

		yesnoPanel.SetActive (false);
		backButtonObj.GetComponent<Image>().color = new Color(backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.g, backButtonObj.GetComponent<Image>().color.b, 255);
		backButtonObj.GetComponent<Button>().interactable = true;
	}

    void UpdateSoundIconStatus()
    {
        if (AudioManager.instance.isAudioMuted)
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

	public void DisplayTime()
	{
		TimeManagerScript.instance.OnTimerUpdated += UpdateTimeInfo;
		TimeManagerScript.instance.OnTimerFinished += onTimedOut;
		timerText.color =normalTimeColor;
		isTimedOut = false;
	}

	void UpdateTimeInfo()
	{
		timerText.text = TimeManagerScript.instance.currentTime.ToString();
	}

	void OnClickedHintButton()
	{
		//Reduce hint value by 1 when hint is clicked
		if(GameManager.instance.GetHintValue()>0&&(GameManager.instance.gameModeScript.isInGame))
		{
		GameManager.instance.gameModeScript.RevealChar();
		GameManager.instance.UpdateHintCount(GameManager.instance.GetHintValue()-1);
		}
	}

	void onTimedOut()
	{
		//timerText.text = "00:00";
		isTimedOut = true;
		timerText.color = normalTimeColor;
	}

	public void DisplayHint(int hintValue)
	{
		hintText.text=GameManager.instance.GetHintValue().ToString();
	}

	public void DisplayHighScore(int _updatedScore)
	{
		highScoreText.text = "HIGHSCORE "+_updatedScore.ToString ();
	}

	public void AnimateKeysScaleDown()
	{
		foreach(GameObject g in keys)
		{
			g.GetComponent<KeyboardScript>().AnimateScaleDown();
		}
	}

	public void AnimateKeysScaleUp()
	{
		foreach(GameObject g in keys)
		{
			g.GetComponent<KeyboardScript>().AnimateScaleUp();
		}
	}

	public IEnumerator ShowUserCorrectWord()
	{
		 float elapsedTime =0;
		 float time=3f;
     	 Vector3 startingPos = transform.position;
    	 while (elapsedTime < time)
     	{
         answerTransform.position = Vector3.Slerp(answerTransform.position, wordEndPos.position, (elapsedTime / time));
         elapsedTime += Time.deltaTime;
         yield return null;
		 }

		yield return new WaitForSeconds(0f);
	}

	public void ResetAnswerPos()
	{
		answerTransform.position=wordNormalPos.position;
	}

	public void ResetTime()
	{
		timerText.text = "00:00";
	}

	public void DisableSettingPanel()
	{
		settingsPanel.SetActive (false);
	}

	public void EnableableSettingPanel()
	{
		settingsPanel.SetActive (true);
	}
	void OnDisable()
	{
        //GameManager.instance.gameModeScript.CancelInvoke();
    }

}
