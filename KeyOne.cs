using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOne : MonoBehaviour {

	public float rotationSpeed = 1f;
	public AudioSource PickUpSFX;

	public GameObject barrier;

	void Update(){
		gameObject.transform.Rotate (0f, rotationSpeed, 0f);
	}

	void OnTriggerStay (Collider col){

		if (Input.GetKeyDown (KeyCode.E) && !CharacterMovement.isDead && col.name == "Hero"){
			PickUpSFX.Play ();
			gameObject.SetActive (false);
			barrier.SetActive (false);
		}
	}
}
