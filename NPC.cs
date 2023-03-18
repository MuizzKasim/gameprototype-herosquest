using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactive {

	public string[] lines;
	public string objName;
	public Sprite sprite;

	void OnTriggerEnter (Collider col){

		if (col.name == "Hero") {
			//Debug.Log ("You have entered the interactive range.");
			Interact ();
		}
	}

	void OnTriggerExit (Collider col){

		if (col.name == "Hero") {
			//Debug.Log ("You have left the interactive range.");
			NotInteract ();
		}
	}

	//If player is within interaction range, then start a dialogue
	public override void Interact(){
		DialogueManager.Instance.AddNewDialogue(lines, objName, sprite);
	}

	//If player leaves the interaction range, then disable the dialogue
	public override void NotInteract(){
		DialogueManager.Instance.CloseDialogue ();
	}
}
