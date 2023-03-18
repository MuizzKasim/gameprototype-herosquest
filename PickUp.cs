using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactive {

	public string[] lines;
	public string objName;
	public Sprite sprite;

	public QuestGiver qg;
	public GameObject SwordIcon;

	public AudioSource SwordActivateSFX;

	void Awake(){
		SwordIcon.SetActive (false);
	}

	void OnTriggerEnter (Collider col){

		if (col.name == "Hero") {
			//Debug.Log ("You have entered the interactive range.");
			Interact ();
		}
	}

	void OnTriggerStay (Collider col){

		if (Input.GetKeyDown (KeyCode.E) && !CharacterMovement.isDead && col.name == "Hero"){
			SwordBehaviour.EnableSword ();
			SwordIcon.SetActive (true);
			SwordActivateSFX.Play ();

			Destroy (gameObject);
			NotInteract ();
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
