using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMode : MonoBehaviour {

	// Use this for initialization
	public GameObject OutputPanel;

	[System.Serializable]
	public struct TextObjects
	{
	public GameObject Textobj;
	public char letter;
	}

    [Header("Refrences for GameManger")]
    public GameObject gameStatePrefab;
    public GameMode gameModeScript;
    public GameObject gamePlayCanvas;
    public int totalNumberOfWordsFound;
	public int wordFoundCount;
    public GameObject hangPlace;
	public GameObject mainMenuCanvas;
	public Text highScoreText;
	public string correctWord;
	public bool isInGame;





    private TextObjects[] textObjectsRef;
	private int numberOfCharacters;
    [Header("Refrrences for GameMode")]
    [SerializeField]
	private GameObject textObj;
	[SerializeField]
	private HangmanManager hangmanManager;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private GameManager gameManager;
	private int wrongPressCount;
	private List<GameObject> pressedKeys=new List<GameObject>();
	private bool isWordLoadedFromDB;
	private string[] wordArray;
	[SerializeField]
	private float timeForOneChar;


	void Start ()
	{
		gameManager = GameManager.instance;
		gameManager.gameModeScript = this;
		gameManager.InitializeRefrrences ();
		StartGame ();
	}
	public void StartGame()
	{
		IEnumerator i = StartWithDelay();
		StartCoroutine(i);
	}

	IEnumerator StartWithDelay()
	{
		gamePlayCanvas.GetComponent<GamePlayCanvasScript> ().DisplayHighScore (gameManager.GetHighScore ());
		gamePlayCanvas.GetComponent<GamePlayCanvasScript> ().EnableableSettingPanel ();

		yield return new WaitForSeconds (3f);

		int numberOfCharactersInCurrentWord=ChooseWord ();
		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().ResetAnswerPos();
		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().ResetTime();
		
		yield return (SpawnBlankSpace(numberOfCharactersInCurrentWord));

		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().AnimateKeysScaleUp();
		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().DisplayTime();
		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().DisplayHint(GameManager.instance.GetHintValue());

		yield return new WaitForSeconds (2f);

		TimeManagerScript.instance.StartTimer(CalculateTimeNeeded(numberOfCharactersInCurrentWord));

		UpdateScore ();
		isInGame=true;
	}

	public IEnumerator SpawnBlankSpace(int characterLength)
	{	//dynamic size for x and fixed value for y
		//Debug.Log ("SpawnBlank Called");
		OutputPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (characterLength * (textObj.GetComponent<RectTransform> ().sizeDelta.x + OutputPanel.GetComponent<HorizontalLayoutGroup> ().spacing), OutputPanel.GetComponent<RectTransform> ().sizeDelta.y);
		textObjectsRef = new TextObjects[characterLength];

		for(int i=0;i<characterLength;i++)
        {
			yield return new WaitForSeconds(.1f);
			GameObject currentTextObj=Instantiate (textObj, OutputPanel.transform);
			//when starting just put as balnk 
			currentTextObj.GetComponent<TextObjScript>().SetDashSprite();
			//store textobj in struct array for future use
			textObjectsRef[i].Textobj=currentTextObj;
			textObjectsRef [i].letter = GetCharAtIndex (correctWord, i);
			currentTextObj.GetComponent<TextObjScript> ().correctLetter = textObjectsRef [i].letter;
		}
		yield return new WaitForSeconds(0f);
	}
	
	public int CalculateNumberOfCharacters(string word)
	{
		string s = word;
		return(s.Length);
	}
	public char GetCharAtIndex(string inputWord,int inputIndex)
	{
		char[] cArray=inputWord.ToCharArray ();
		return(cArray[inputIndex]);
	}
	bool CompareInputAndWord(char keyVal)
	{
		bool bContainsChar = false;

		foreach (char c in correctWord.ToCharArray())
        {
			if (keyVal == c)
            {
				bContainsChar = true;
				return bContainsChar;
			}
            else
				bContainsChar = false;  
		}
		return bContainsChar;
	}
	bool UpdateLetterInOutputPanel(char letterToBeUpdated)
	{
        bool isWordCompleted = false;

		foreach (TextObjects t in textObjectsRef)
        {
			if (t.letter == letterToBeUpdated)
			{
				t.Textobj.GetComponent<TextObjScript> ().DisplayCorrectLetter ();
				t.Textobj.GetComponent<TextObjScript> ().isCorrectLetterUpdated=true;
			}
		}
        foreach (TextObjects t in textObjectsRef)
        {
            if (t.Textobj.GetComponent<TextObjScript>().isCorrectLetterUpdated)
            {
                isWordCompleted = true;
            }
            else
                return (false);
        }
        return(isWordCompleted);
    }
	public void OnKeyPressed(GameObject keyRef,char keyValue)
	{
		pressedKeys.Add (keyRef);
		if (CompareInputAndWord (keyValue))
        {
			bool isWordCompleted=UpdateLetterInOutputPanel (keyValue);
			keyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Highlight,GetCorrespondingAlphabetObj(keyValue));

			if (isWordCompleted) 
			{
				CompletedChallenge();
			}
        }
        else
        {
			wrongPressCount = wrongPressCount + 1;
			hangmanManager.EnablePartsOfHangplace (wrongPressCount);
			keyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Disable,null);

			if (wrongPressCount == hangmanManager.hangPlaceParts.Length)
			{
				ChallengeNotCompleted();
			}
				
        }
	}
	void DisplayCorrectWord()
	{
		foreach (TextObjects t in textObjectsRef)
        {
            if (!t.Textobj.GetComponent<TextObjScript>().isCorrectLetterUpdated)
            {
                t.Textobj.GetComponent<TextObjScript>().DisplayCorrectLetter();
            }
		}
	
	}

	int  ChooseWord()
	{
		//Debug.Log ("ChooseWord Called");
        if (!isWordLoadedFromDB)
		{
         wordArray = DBReaderWriter.ReadFromDB();
		 isWordLoadedFromDB=true;
		}
		else
		{
            List<string> tempWordList = new List<string>();
			//Removing a word after it is used
			for(int i=0;i<wordArray.Length;i++)
			{
				if(wordArray[i]==correctWord)
				{
                    //wordArray[i].Remove(0,i);
				}
                else
                {
                    tempWordList.Add(wordArray[i]);
                }
			}
            wordArray = new string[0];
            wordArray = tempWordList.ToArray();
		}
		correctWord=wordArray[Random.Range(0,wordArray.Length-1)];
		correctWord=correctWord.ToUpper();
		int numberOfCharacters = CalculateNumberOfCharacters (correctWord);
		return(numberOfCharacters);
	}
	public void DeletePreviousWord()
	{
		foreach(TextObjects t in textObjectsRef)
		{
			Destroy(t.Textobj,0f);
		}
	}
	public void ResetPressedKeys()
	{
		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().AnimateKeysScaleUp();
		pressedKeys.Clear ();
		wrongPressCount = 0;
	}

	public void ChangeWord()
	{
        SpawnBlankSpace(ChooseWord());
		totalNumberOfWordsFound = totalNumberOfWordsFound + 1;
        UpdateScore();
    }
	void UpdateScore()
	{
		string tnw = totalNumberOfWordsFound.ToString ();
		string wfc = wordFoundCount.ToString ();
		scoreText.text = "SURVIVED : " + wfc;
	}

    GameObject GetCorrespondingAlphabetObj(char keyVal)
    {
        foreach (TextObjects t in textObjectsRef)
        {
            if(t.letter==keyVal)
            {
               return t.Textobj;
            }
        }

        return null;
    }
	public void RevealChar()
	{
		string newWord="";
		foreach (TextObjects t in textObjectsRef)
        {
			if(!t.Textobj.GetComponent<TextObjScript>().isCorrectLetterUpdated)
			{
				newWord+=t.letter;
			}
		}
		int length=newWord.Length;
		char[] c=newWord.ToCharArray();
		int n=Random.Range(0,length-1);
		char cr=c[n];
		//finding keyboard letter gameobject corresponding car
		GameObject k=GameObject.FindGameObjectWithTag(cr.ToString());
		OnKeyPressed(k,cr);
	}
	float CalculateTimeNeeded(int numberOfCharactersInWord)
	{
		float totalTime = timeForOneChar * (float)numberOfCharactersInWord;
		return(totalTime);	
	}

	void CompletedChallenge()
	{
		isInGame=false;

		gamePlayCanvas.GetComponent<GamePlayCanvasScript> ().EnableableSettingPanel ();


		wordFoundCount = wordFoundCount + 1;

		if(!gamePlayCanvas.GetComponent<GamePlayCanvasScript>().isTimedOut)
		{
			GameManager.instance.UpdateHintCount(GameManager.instance.GetHintValue()+1);
		}

		TimeManagerScript.instance.StopTimer();

		UpdateScore();

		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().AnimateKeysScaleDown();	

		IEnumerator i=gamePlayCanvas.GetComponent<GamePlayCanvasScript>().ShowUserCorrectWord();		
		StartCoroutine(i);

		gameManager.Invoke("LevelCompleted",3f);
	}

	void ChallengeNotCompleted()
	{
		isInGame=false;

		gamePlayCanvas.GetComponent<GamePlayCanvasScript> ().DisableSettingPanel ();

		TimeManagerScript.instance.StopTimer();

		DisplayCorrectWord();

		gamePlayCanvas.GetComponent<GamePlayCanvasScript>().AnimateKeysScaleDown();

		IEnumerator i=gamePlayCanvas.GetComponent<GamePlayCanvasScript>().ShowUserCorrectWord();		
		StartCoroutine(i);

		gameManager.Invoke("GameOver",3f);
	}

}
