using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMode : MonoBehaviour {

	// Use this for initialization
	public GameObject OutputPanel;

	[System.Serializable]
	public struct TextObjects{
	public GameObject Textobj;
	public char letter;
	}

    [Header("Refrrences for GameManger")]
    public GameObject gameStatePrefab;
    public GameMode gameModeScript;
    public GameObject gamePlayCanvasPrefab;
    public int totalNumberOfWords;
	public int wordFoundCount;
    public GameObject hangPlace;


    private TextObjects[] textObjectsRef;
    private string correctWord;
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
    private string currentHint;

	void Start () 
	{
		
	}
	void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.gameModeScript = this;
    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SpawnBlankSpace(int characterLength)
	{
		OutputPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (characterLength * (textObj.GetComponent<RectTransform> ().sizeDelta.x + OutputPanel.GetComponent<HorizontalLayoutGroup> ().spacing), OutputPanel.GetComponent<RectTransform> ().sizeDelta.y);
		textObjectsRef = new TextObjects[characterLength];

		for(int i=0;i<characterLength;i++)
        {
			GameObject currentTextObj=Instantiate (textObj, OutputPanel.transform);
			//when starting just put as balnk 
			currentTextObj.GetComponent<TextObjScript>().SetDashSprite();
			//store textobj in struct array for future use
			textObjectsRef[i].Textobj=currentTextObj;
			textObjectsRef [i].letter = GetCharAtIndex (correctWord, i);
			currentTextObj.GetComponent<TextObjScript> ().correctLetter = textObjectsRef [i].letter;
		}
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
				t.Textobj.GetComponent<TextObjScript> ().DisplayCorrectLetter ();
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
			keyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Highlight);

			if (isWordCompleted) 
			{
				wordFoundCount = wordFoundCount + 1;
                UpdateScore();
                gameManager.LevelCompleted ();
			}
        }
        else
        {
			wrongPressCount = wrongPressCount + 1;
			hangmanManager.EnablePartsOfHangplace (wrongPressCount);
			keyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Disable);

			if (wrongPressCount == 10)
			{
				DisplayCorrectWord();
				gameManager.GameOver ();
			}
				
        }
	}
	void DisplayCorrectWord()
	{
		foreach (TextObjects t in textObjectsRef)
        {
			if(!t.Textobj.GetComponent<TextObjScript>().isCorrectLetterUpdated)
				t.Textobj.GetComponent<TextObjScript>().DisplayCorrectLetter();
		}
	
	}

	int  ChooseWord()
	{
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
		foreach (GameObject g in pressedKeys) 
		{
			g.GetComponent<KeyboardScript> ().ResetKeys ();
		}
		pressedKeys.Clear ();
		wrongPressCount = 0;
	}

	public void ChangeWord()
	{
        SpawnBlankSpace(ChooseWord());
		totalNumberOfWords = totalNumberOfWords + 1;
        UpdateScore();
    }
	void UpdateScore()
	{
		string tnw = totalNumberOfWords.ToString ();
		string wfc = wordFoundCount.ToString ();
		scoreText.text ="Words Found = "+wfc +" | "+"Total Words = "+ tnw ;
	}
	}
