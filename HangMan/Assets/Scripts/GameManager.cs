using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour {

	// Use this for initializatiom
	public GameObject hangPlace;

    [SerializeField]
    private GameObject gameStatePrefab;
    [SerializeField]
    private GameMode gameModeScript;
    private GameObject gameStateCanvas;
    [SerializeField]
    private GameObject gamePlayCanvasPrefab;
    private GameObject gamePlayCanvas;

	void Start ()
    {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartGame()
	{
	

	}
	public void QuitGame()
	{
		Application.Quit ();
	}
	public void GameOver()
	{
        if (!gameStateCanvas)
        {
            gameStateCanvas = Instantiate(gameStatePrefab);
            gameStateCanvas.GetComponent<GameStateCanvasScript>().gameManager = this.gameObject;
            gameStateCanvas.GetComponent<GameStateCanvasScript>().OnGameOver();
        }
        else
        {
            gameStateCanvas.GetComponent<GameStateCanvasScript>().OnGameOver();
        }

	}

	public void LevelCompleted()
	{
        if (!gameStateCanvas)
        {
            gameStateCanvas = Instantiate(gameStatePrefab);
            gameStateCanvas.GetComponent<GameStateCanvasScript>().OnCompleted();
        }
        else
        {
            gameStateCanvas.GetComponent<GameStateCanvasScript>().OnCompleted();
        }
    }

    public void RestartGame()
    {
        Destroy(gameStateCanvas,0f);
        gameModeScript.GetComponent<GameMode>().ChangeWord();

    }
    public void NextWord()
    {
         Destroy(gameStateCanvas,0f);
        gameModeScript.GetComponent<GameMode>().ChangeWord();
    }
}
