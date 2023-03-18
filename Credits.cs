using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

	public GameObject scrollUpText;
	public GameObject theEnd;

	public AudioSource bgm;

	void Awake (){
		theEnd.SetActive (false);
	}

	void Start () {
		scrollUpText.SetActive (true);
		Invoke ("TheEnd", 21f);
	}
	
	void TheEnd(){
		theEnd.SetActive (true);
	}
}
