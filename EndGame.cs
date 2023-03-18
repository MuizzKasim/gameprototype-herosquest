using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject fadeIn;

	public void endGame(){
		StartCoroutine ("LoadEndScreen");
	}

	IEnumerator LoadEndScreen(){
		yield return new WaitForSeconds (4f);
		fadeIn.SetActive (true);

		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (1);
	}
}
