﻿using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public AudioClip[] BGM;
	private new AudioSource audio;
	private int i=0;
	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource> ();
		audio.clip = BGM [i];
		audio.Play ();
	}

	// Update is called once per frame
	void Update () {

	}
	public void ChangeBGM(){
		if(i+1==BGM.Length){
			i=0;
		}
		else
			i++;
		audio.clip = BGM [i];
		audio.Play ();
	}
}
