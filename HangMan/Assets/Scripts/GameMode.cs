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

	void Start () {
		correctWord = "RAGHUL";
		numberOfCharacters = CalculateNumberOfCharacters (correctWord);
		CreateBlankSpace (numberOfCharacters);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateBlankSpace(int characterLength)
	{
		OutputPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (characterLength * (TextObj.GetComponent<RectTransform> ().sizeDelta.x + OutputPanel.GetComponent<HorizontalLayoutGroup> ().spacing), OutputPanel.GetComponent<RectTransform> ().sizeDelta.y);
		textObjectsRef = new TextObjects[characterLength];
		for(int i=0;i<characterLength;i++) {
			GameObject currentTextObj=Instantiate (TextObj, OutputPanel.transform);
			//when starting just put as balnk 
			currentTextObj.GetComponent<Text> ().text ="__";
			//store textobj in struct array for future use
			textObjectsRef[i].Textobj=currentTextObj;
			textObjectsRef [i].letter = GetCharAtIndex (correctWord, i);
			currentTextObj.GetComponent<TextObjScript> ().correctLetter = textObjectsRef [i].letter;
		}
	}
	public int CalculateNumberOfCharacters(string Word)
	{
		string s = Word;
		return(s.Length);
	}
	public char GetCharAtIndex(string InputWord,int InputIndex)
	{
		char[] cArray=InputWord.ToCharArray ();
		return(cArray[InputIndex]);
	}
	bool CompareInputAndWord(char KeyVal)
	{
		bool bContainsChar = false;
		foreach (char c in correctWord.ToCharArray()) {
			if (KeyVal == c) {
				bContainsChar = true;
				return bContainsChar;
			} else
				bContainsChar = false;
		}
		return bContainsChar;
	}
	void UpdateLetterInOutputPanel(char LetterToBeUpdated)
	{
		foreach (TextObjects t in textObjectsRef) {
			if (t.letter == LetterToBeUpdated)
				t.Textobj.GetComponent<TextObjScript> ().DisplayCorrectLetter ();
		}
	}
	public void OnKeyPressed(GameObject KeyRef,char KeyValue)
	{
		if (CompareInputAndWord (KeyValue)) {
			UpdateLetterInOutputPanel (KeyValue);
			KeyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Highlight);
		} else {
			wrongPressCount = wrongPressCount + 1;
			HangmanManager.EnablePartsOfHangplace (wrongPressCount);
			KeyRef.GetComponent<KeyboardScript> ().HighlightOrDisable (KeyboardScript.ButtonStates.Disable);
			if (wrongPressCount == 10)
				GameManager.GameOver ();}
	}
	}9176781687 tripti
