using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
	private GameObject _mainMenuCanvas;
    private SceneLoader _sceneLoader;
	[SerializeField]
	private Text _highScoreText;
	[SerializeField]
	private string bgSoundName;

	void Start ()
    {
		StartCoroutine ("PlayBgSound");
		InitializeRefrrences();
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
        _gamePlayCanvasPrefab = gameModeScript.gamePlayCanvas;
		_mainMenuCanvas = gameModeScript.mainMenuCanvas;
		_highScoreText = gameModeScript.highScoreText;
        _sceneLoader = SceneLoader.instance;
    }
    public void StartGame()
	{
		gameModeScript.StartGame ();
		_mainMenuCanvas.SetActive (false);
		_gamePlayCanvasPrefab.SetActive (true);
		DisplayHighScore ();
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
		_gameStateCanvas.GetComponent<GameStateCanvasScript>().loseStreakCountText.text = "STREAK : " + gameModeScript.GetComponent<GameMode> ().wordFoundCount;

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

		_gameStateCanvas.GetComponent<GameStateCanvasScript>().wonStreakCountText.text = "STREAK : " + gameModeScript.GetComponent<GameMode> ().wordFoundCount;
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
	IEnumerator PlayBgSound()
	{
		yield return new WaitForSeconds (.1f);
		AudioManager.instance.PlaySound (bgSoundName,true);
		yield return null;
	}
	void DisplayHighScore()
	{
		_highScoreText.text= "HIGHSCORE : " + GetHighScore ().ToString ();
	}

	public void OnClickedMainMenuButton()
	{
		gameModeScript.DeletePreviousWord ();
		gameModeScript.ResetPressedKeys ();
		_hangPlace.GetComponent<HangmanManager>().ResetHangPlace();
		gameModeScript.GetComponent<GameMode>().totalNumberOfWords = 0;
		_gamePlayCanvasPrefab.SetActive (false);
		Destroy (_gameStateCanvas, 0);
		_mainMenuCanvas.SetActive (true);
	}
}
