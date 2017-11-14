using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmanManager : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private GameObject[] hangPlaceParts;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void EnablePartsOfHangplace(int indexLength)
	{
		for (int i = 0; i <indexLength; i++)
        {
			hangPlaceParts [i].SetActive (true);
		}
	}
}
