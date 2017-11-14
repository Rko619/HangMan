using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmanManager : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private GameObject[] HangPlaceParts;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void EnablePartsOfHangplace(int IndexLength)
	{
		for (int i = 0; i <IndexLength; i++) {
			HangPlaceParts [i].SetActive (true);
		}
	}
}
