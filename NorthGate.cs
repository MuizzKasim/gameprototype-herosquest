using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthGate : MonoBehaviour {

	public QuestManager qm;
	public AudioSource OpenDoorSFX;

	void OnTriggerStay (Collider col){

		if (col.name == "Hero" && Input.GetKey (KeyCode.E)) {

			if (qm.questOne && qm.questTwo && qm.questThree) {

				StartCoroutine ("RemoveDoor");
				OpenDoorSFX.Play ();
			}
		}
	}

	IEnumerator RemoveDoor(){
		yield return new WaitForSeconds(0.5f);
		gameObject.transform.Find ("2partDoor").gameObject.SetActive (false);
	}

}
