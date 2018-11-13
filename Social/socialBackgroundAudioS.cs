using UnityEngine;
using System.Collections;

public class socialBackgroundAudioS : MonoBehaviour
{
	public AudioClip[] audios;
	
	public void PlayAudio(int _index)
	{
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().clip = audios[_index];
		GetComponent<AudioSource>().Play();
	}
}