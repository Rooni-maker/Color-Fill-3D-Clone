using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager Instance;

	//public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	List<GameObject> soundsObjects = new List<GameObject>();
	public bool isMute;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		foreach (var VARIABLE in sounds)
		{
			GameObject go = new GameObject();
			go.AddComponent<AudioSource>();
			go.GetComponent<AudioSource>().clip = VARIABLE.clip;
			soundsObjects.Add((go));
			go.transform.parent = transform;
			go.transform.name = VARIABLE.name;
			go.GetComponent<AudioSource>().volume = VARIABLE.volume;

			go.GetComponent<AudioSource>().loop = VARIABLE.loop;
			go.GetComponent<AudioSource>().priority = VARIABLE.prirority;
		}


	}

	public void Play(string sound, float _time = 0)
	{
		if (PlayerPrefs.GetInt("soundOn") == 1)
		{
			return;
		}
		/*Sound s = Array.Find(sounds, item => item.name == sound);
		 
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}*/
		foreach (var VARIABLE in soundsObjects)
		{
			if (sound == VARIABLE.name)
			{

				StartCoroutine(PlayAudio(VARIABLE.GetComponent<AudioSource>(), _time));
			}
		}


		//s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));


	}
	public void Stop(string sound, float _time = 0)
	{
		if (PlayerPrefs.GetInt("soundOn") == 1)
		{
			return;
		}
		/*Sound s = Array.Find(sounds, item => item.name == sound);
		 
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}*/
		foreach (var VARIABLE in soundsObjects)
		{
			if (sound == VARIABLE.name)
			{
				VARIABLE.GetComponent<AudioSource>().Stop();
				//StartCoroutine(PlayAudio(VARIABLE.GetComponent<AudioSource>(), _time));
			}
		}


		//s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));


	}


	IEnumerator PlayAudio(AudioSource source, float delay)
	{
		yield return new WaitForSeconds(delay);
		source.Play();

	}
	public void StopAllAudios()
	{
		for (int i = 0; i < soundsObjects.Count; i++)
		{
			soundsObjects[i].GetComponent<AudioSource>().Stop();
		}
	}





}
