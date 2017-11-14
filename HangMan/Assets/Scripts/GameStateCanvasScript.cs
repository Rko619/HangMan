using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStateCanvasScript : MonoBehaviour {

    // Use this for initialization
    public GameObject gameManager;

    [SerializeField]
    private Text gameStateText;
    [SerializeField]
    private Color gameOverTextColor;
    [SerializeField]
    private Color gameCompletedTextColor;

    [SerializeField]
    private GameObject nextButton;

    [SerializeField]
    private GameObject restartButton;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnGameOver()
    {
        restartButton.SetActive(true);
        nextButton.SetActive(false);
        gameStateText.color = gameOverTextColor;
        gameStateText.text = "Game Over :(";
    }

    public void OnCompleted()
    {
        restartButton.SetActive(false);
        nextButton.SetActive(true);
        gameStateText.color = gameCompletedTextColor;
        gameStateText.text = "Completed !!!";
    }
    public void RestartGame()
    {
        gameManager.GetComponent<GameManager>().RestartGame();
    }
    public void NextWord()
    {
         gameManager.GetComponent<GameManager>().NextWord();
    }

}
