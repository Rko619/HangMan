
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public  class GameManager : MonoBehaviour {

    // Use this for initializatiom
    public static GameManager instance;
	public delegate void OnHighScoreChanged(int updatedHighScore);
	public OnHighScoreChanged onHighScoreChanged;

	public delegate void OnHintValueChanged(int updatedHintValue);
	public OnHighScoreChanged onHintValueChanged;

    public GameMode gameModeScript;

    private GameObject _hangPlace;
    private GameObject _gameStatePrefab;
    private GameObject _gameStateCanvas;
    private GameObject _gamePlayCanvasPrefab;
    private GameObject _gamePlayCanvas;
	private GameObject _mainMenuCanvas;
    private SceneLoader _sceneLoader;

	[SerializeField]
	private string bgSoundName;

	[Header("These are for Loading MainMenuLevel")]
	[SerializeField]
	private string MainMenuLevel;
	[SerializeField]
	private float timeAfterLoad;
	void Start ()
    {
		StartCoroutine ("PlayBgSound");
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		//InitializeRefrrences();
		DontDestroyOnLoad (gameObject);
	}
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);
	}
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void InitializeRefrrences()
    {
        _hangPlace = gameModeScript.hangPlace;
        _gameStatePrefab = gameModeScript.gameStatePrefab;
        _gamePlayCanvasPrefab = gameModeScript.gamePlayCanvas;
		_mainMenuCanvas = gameModeScript.mainMenuCanvas;
        _sceneLoader = SceneLoader.instance;
    }
    public void StartGame()
	{
		gameModeScript.StartGame ();
		_mainMenuCanvas.SetActive (false);
		_gamePlayCanvasPrefab.SetActive (true);
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
        gameModeScript.GetComponent<GameMode>().totalNumberOfWordsFound = 0;
        gameModeScript.GetComponent<GameMode>().StartGame();

    }
    public void NextWord()
    {
		gameModeScript.DeletePreviousWord ();
        Destroy(_gameStateCanvas,0f);
		gameModeScript.ResetPressedKeys ();
        _hangPlace.GetComponent<HangmanManager>().ResetHangPlace();
        gameModeScript.GetComponent<GameMode>().StartGame();
    }
	public int GetHighScore()
	{
		return(PlayerPrefs.GetInt ("HighScore"));
	}

	public int GetHintValue()
	{
		int hintVal=PlayerPrefs.GetInt ("HintValue");
		return hintVal;
	}

	public void UpdateHintCount(int newHintVal)
	{
		PlayerPrefs.SetInt ("HintValue", newHintVal);
		onHintValueChanged (newHintVal);
	}

	public void UpdateHighScore(int newScore)
	{
		if (newScore > GetHighScore ()) 
		{
			PlayerPrefs.SetInt ("HighScore", newScore);
			PlayerPrefs.Save ();
			onHighScoreChanged (newScore);
		}
	}
	IEnumerator PlayBgSound()
	{
		yield return new WaitForSeconds (.1f);
		AudioManager.instance.PlaySound (bgSoundName,true);
		yield return null;
	}


	public void OnClickedMainMenuButton()
	{
//		gameModeScript.DeletePreviousWord ();
//		gameModeScript.ResetPressedKeys ();
//		_hangPlace.GetComponent<HangmanManager>().ResetHangPlace();
//		gameModeScript.GetComponent<GameMode>().totalNumberOfWordsFound = 0;
//		_gamePlayCanvasPrefab.SetActive (false);
//		Destroy (_gameStateCanvas, 0);
//		_mainMenuCanvas.SetActive (true);
		_sceneLoader.LoadScene(MainMenuLevel);

	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void UnPauseGame()
	{
		Time.timeScale = 1;
	}


	
}
