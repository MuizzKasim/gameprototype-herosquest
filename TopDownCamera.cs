using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

	public Transform PlayerTransform;

	void Update () {
		transform.position = PlayerTransform.position;
		transform.position = new Vector3 (transform.position.x, transform.position.y + 6f, transform.position.z-3.5f);
	}
}
