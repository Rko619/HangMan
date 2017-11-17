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
	public TextObjects[] textObjectsRef;	


	private string correctWord;
	private int numberOfCharacters;
	[SerializeField]
	private GameObject TextObj;
	[SerializeField]
	private HangmanManager HangmanManager;
	private int wrongPressCount;
	[SerializeField]
	private GameManager GameManager;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SpawnBlankSpace(int characterLength)
	{
		OutputPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (characterLength * (TextObj.GetComponent<RectTransform> ().sizeDelta.x + OutputPanel.GetComponent<HorizontalLayoutGroup> ().spacing), OutputPanel.GetComponent<RectTransform> ().sizeDelta.y);
		textObjectsRef = new TextObjects[characterLength];

		for(int i=0;i<characterLength;i++)
        {
			GameObject currentTextObj=Instantiate (TextObj, OutputPanel.transform);
			//when starting just put as balnk 
			currentTextObj.GetComponent<Text> ().text ="__";
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
		if (CompareInputAndWord (keyValue))
        {
			bool isWordCompleted=UpdateLetterInOutputPanel (keyValue);
			keyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Highlight);

            if (isWordCompleted)
                GameManager.LevelCompleted();
        }
        else
        {
			wrongPressCount = wrongPressCount + 1;
			HangmanManager.EnablePartsOfHangplace (wrongPressCount);
			keyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Disable);

			if (wrongPressCount == 10)
			{
				DisplayCorrectWord();
				GameManager.GameOver ();
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
		string[] wordArray;
		string category="Animals";
		wordArray=new string[JsonReaderWriter.GetNumberOfWords(category)];
		wordArray=JsonReaderWriter.ReadFromJson(category);
		correctWord=wordArray[Random.Range(0,wordArray.Length-1)];
		correctWord=correctWord.ToUpper();
		int numberOfCharacters = CalculateNumberOfCharacters (correctWord);
		return(numberOfCharacters);
	}
	void DeletePreviousWord()
	{
		foreach(TextObjects t in textObjectsRef)
		{
			Destroy(t.Textobj,0f);
		}
	}
	public void ChangeWord()
	{
		SpawnBlankSpace(ChooseWord());
	}
	}
