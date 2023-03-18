using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {

	public void QuitGame(){
		Debug.Log ("Has quit game");
		Application.Quit ();
	}
}