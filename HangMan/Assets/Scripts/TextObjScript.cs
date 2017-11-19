using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextObjScript : MonoBehaviour
{

	// Use this for initialization
	public char correctLetter;
    public bool isCorrectLetterUpdated;
	[System.Serializable]
	public struct Alphabets
	{
		public string alphabetName;
		public Sprite alphabetSprite;
	}

	[SerializeField]
	private Alphabets[] keyboardAlphabets;
	[SerializeField]
	private Color correctTextColor;
	[SerializeField]
	private Sprite dashSprite;
	[SerializeField]
	private Image alphabetDisplayImage;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void DisplayCorrectLetter()
	{
		foreach (Alphabets a in keyboardAlphabets)
		{
			if (a.alphabetName == correctLetter.ToString ()) 
			{
				alphabetDisplayImage.sprite = a.alphabetSprite;
				alphabetDisplayImage.SetNativeSize ();
			}
		}
        isCorrectLetterUpdated = true;
	}

	public void SetDashSprite()
	{
		alphabetDisplayImage.sprite = dashSprite;
		alphabetDisplayImage.SetNativeSize ();
	}
}
