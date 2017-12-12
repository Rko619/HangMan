using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;

	[SerializeField]
	private float timeAfterLoad;
	[SerializeField]
	private GameObject loadingCanvas;
    // Use this for initialization
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

	public void LoadScene(string sceneName)
    {
		StartCoroutine(LoadSceneAsync(sceneName));
    }

	private IEnumerator LoadSceneAsync(string sceneName)
	{
		//loadingCanvas.SetActive (true);
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
		//operation.allowSceneActivation = false;

//		while (!operation.isDone) 
//		{
//			//Debug.Log ("FAFAFA");
//			if (operation.progress > 8f)
//				break;
//			yield return null;
//		}
		//Debug.Log ("loaded");
		yield return new WaitForSeconds (0);

	}

}
