using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundResources : MonoBehaviour {
	private AudioClip clip;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void play (string str){
		clip = (AudioClip)Resources.Load ("sounds/"+str, typeof(AudioClip));
		PlayAudioClip ();
	}
	public void PlayAudioClip(){
		if (clip == null) {
			Debug.LogError ("1");
			return;
		}			
		AudioSource source = gameObject.GetComponent<AudioSource> ();
		if (source == null) 
			source = gameObject.AddComponent<AudioSource> ();
	
		source.clip = clip;
		source.Play ();		
	}


}
