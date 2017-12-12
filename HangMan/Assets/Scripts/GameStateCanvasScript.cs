using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;     

public class GameStateCanvasScript : MonoBehaviour {

    // Use this for initialization
    public GameObject gameManager;
	public Text loseStreakCountText;
	public Text wonStreakCountText;

    [SerializeField]
    private GameObject wonPanel;
    [SerializeField]
    private GameObject losePanel;


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
		gameManager.GetComponent<GameManager> ().OnClickedMainMenuButton ();
    }

}
