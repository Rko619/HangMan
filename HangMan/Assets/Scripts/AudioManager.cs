using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HangManSounds
{
    public string soundName;
    public AudioClip sound;
    public AudioSource audioSource;
    [Range(0.0F, 1.0F)]
    public float volume;
    [Range(0.0F, 1.0F)]
    public float pitch;
    public GameObject relatedGameobject;
    public string note; 
    
    public void SetAudioSource(AudioSource asource)
    {
        audioSource = asource;
        audioSource.clip = sound;
    } 
    public void CustomPlaySound(bool islooping)
    {
		audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = islooping;
        audioSource.Play();
    }
		
	public void CustomStopSound()
	{
		audioSource.Stop ();
	}
}


public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public bool isAudioMuted=false;

    [Header("Sounds for the Game")]
    public HangManSounds[] soundsForGame;
	// Use this for initialization
    void Awake()
    {
        instance = this;
		DontDestroyOnLoad (this.gameObject);
    }
	void Start ()
    {
        for (int i=0;i < soundsForGame.Length;i++)
        {
            GameObject s = new GameObject(soundsForGame[i].soundName);
            soundsForGame[i].relatedGameobject = s;
            soundsForGame[i].SetAudioSource(s.AddComponent<AudioSource>());
			s.GetComponent<AudioSource> ().playOnAwake = false;
            s.transform.SetParent(this.transform);

        }
	}

    public void PlaySound(string soundName,bool canLoop)
    {
        foreach(HangManSounds soundObj in soundsForGame)
        {
            if(soundName== soundObj.soundName)
            {
                soundObj.CustomPlaySound(canLoop);
                return;
            }
        }
    }

	public void MuteSounds()
	{
		foreach(HangManSounds soundObj in soundsForGame)
		{
			if (soundObj.audioSource.isPlaying) 
			{
				soundObj.audioSource.mute = true;
                isAudioMuted = true;
            }
		}
	}
	public void UnmuteSounds()
	{
		foreach(HangManSounds soundObj in soundsForGame)
		{
			if (soundObj.audioSource.isPlaying) 
			{
				soundObj.audioSource.mute = false;
			}
		}
        isAudioMuted = false;
}

}
