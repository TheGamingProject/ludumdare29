using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {
	public float bossMusicDelay;

	AudioSource mainMusic;
	AudioSource bossMusic;

	void Start () {
	}
	
	void Update () {
		if (mainMusic == null) {
			mainMusic = GetComponent<HashAudioScript>().GetAudio("main");
			bossMusic = GetComponent<HashAudioScript>().GetAudio("bosstune");
		}
	}

	public void TriggerBossMusic() {
		Debug.Log("trigger boss music in " + bossMusicDelay);
		mainMusic.Stop();
		bossMusic.PlayDelayed(bossMusicDelay);
	}
}

