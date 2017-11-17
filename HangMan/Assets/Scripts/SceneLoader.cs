using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;

	[SerializeField]
	private Image loadingBar; 
    // Use this for initialization
    void Awake()
    {
        instance = this;
		DontDestroyOnLoad (this.gameObject);
		//StartCoroutine ("LoadSceneAsync","n");
	}
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	public IEnumerator LoadSceneAsync(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync ("GamePlayScene");
		while (!operation.isDone)
		{
			loadingBar.fillAmount = operation.progress;
			yield return null;
		}
		operation.allowSceneActivation = false;
	}

}
