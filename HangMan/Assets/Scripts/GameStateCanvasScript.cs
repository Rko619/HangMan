using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;     

public class GameStateCanvasScript : MonoBehaviour {

    // Use this for initialization
    public GameObject gameManager;

    [SerializeField]
    private Text loseStreakCountText;
    [SerializeField]
    private Text wonStreakCountText;
    [SerializeField]
    private GameObject wonPanel;
    [SerializeField]
    private GameObject losePanel;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnGameOver()
    {
        losePanel.SetActive(true);
        wonPanel.SetActive(false);
        loseStreakCountText.text = "STREAK : "+gameManager.GetComponent<GameManager>().gameModeScript.wordFoundCount;
    }

    public void OnCompleted()
    {
        losePanel.SetActive(false);
        wonPanel.SetActive(true);
        wonStreakCountText.text = "STREAK : " + gameManager.GetComponent<GameManager>().gameModeScript.wordFoundCount;
    }
    public void RestartGame()
    {
        gameManager.GetComponent<GameManager>().RestartGame();
    }
    public void NextWord()
    {
        GameManager.instance.NextWord();
    }
    public void OnClickedMainMenuButton()
    {

    }

}
