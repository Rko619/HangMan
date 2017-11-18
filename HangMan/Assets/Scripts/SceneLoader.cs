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
	}
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

	private IEnumerator LoadSceneAsync(string sceneName)
	{
        loadingBar.enabled = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
		while (!operation.isDone)
		{
			loadingBar.fillAmount = operation.progress;
			yield return null;
		}
        loadingBar.enabled = false;
	}

}
