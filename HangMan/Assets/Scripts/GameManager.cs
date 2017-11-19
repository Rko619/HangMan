using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour {

    // Use this for initializatiom
    public static GameManager instance;

    public GameMode gameModeScript;

    private GameObject _hangPlace;
    private GameObject _gameStatePrefab;
    private GameObject _gameStateCanvas;
    private GameObject _gamePlayCanvasPrefab;
    private GameObject _gamePlayCanvas;
    private SceneLoader _sceneLoader;

	void Start ()
    {
        InitializeRefrrences();
        StartGame();
	}
	void Awake()
    {
        instance = this;
		DontDestroyOnLoad (this.gameObject);
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
    void InitializeRefrrences()
    {
        _hangPlace = gameModeScript.hangPlace;
        _gameStatePrefab = gameModeScript.gameStatePrefab;
        _gamePlayCanvasPrefab = gameModeScript.gamePlayCanvasPrefab;
        _sceneLoader = SceneLoader.instance;
    }
    public void StartGame()
	{
        gameModeScript.ChangeWord();
	}
	public void QuitGame()
	{
		Application.Quit ();
	}
	public void GameOver()
	{
        if (!_gameStateCanvas)
        {
            _gameStateCanvas = Instantiate(_gameStatePrefab);
            _gameStateCanvas.GetComponent<GameStateCanvasScript>().gameManager = this.gameObject;
            _gameStateCanvas.GetComponent<GameStateCanvasScript>().OnGameOver();
        }
        else
        {
            _gameStateCanvas.GetComponent<GameStateCanvasScript>().OnGameOver();
        }

		UpdateHighScore (gameModeScript.GetComponent<GameMode> ().wordFoundCount);

	}

	public void LevelCompleted()
	{
        if (!_gameStateCanvas)
        {
            _gameStateCanvas = Instantiate(_gameStatePrefab);
            _gameStateCanvas.GetComponent<GameStateCanvasScript>().gameManager = this.gameObject;
            _gameStateCanvas.GetComponent<GameStateCanvasScript>().OnCompleted();
        }
        else
        {
            _gameStateCanvas.GetComponent<GameStateCanvasScript>().OnCompleted();
        }

		UpdateHighScore (gameModeScript.GetComponent<GameMode> ().wordFoundCount);
    }

    public void RestartGame()
    {
		gameModeScript.DeletePreviousWord ();
		Destroy(_gameStateCanvas,0f);
		gameModeScript.ResetPressedKeys ();
        _hangPlace.GetComponent<HangmanManager>().ResetHangPlace();
        gameModeScript.GetComponent<GameMode>().totalNumberOfWords = 0;
        gameModeScript.GetComponent<GameMode>().ChangeWord();

    }
    public void NextWord()
    {
		gameModeScript.DeletePreviousWord ();
        Destroy(_gameStateCanvas,0f);
		gameModeScript.ResetPressedKeys ();
        _hangPlace.GetComponent<HangmanManager>().ResetHangPlace();
        gameModeScript.GetComponent<GameMode>().ChangeWord();
    }
	public int GetHighScore()
	{
		return(PlayerPrefs.GetInt ("HighScore"));

	}
	public void UpdateHighScore(int newScore)
	{
		if (newScore > GetHighScore ()) 
		{
			PlayerPrefs.SetInt ("HighScore", newScore);
			PlayerPrefs.Save ();
		}
	}
}
