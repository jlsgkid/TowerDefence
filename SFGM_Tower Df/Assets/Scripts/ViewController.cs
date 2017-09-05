using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float mouseSpeed = 500.0f;
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float yMouse = Input.GetAxis ("Mouse ScrollWheel");
		//WSAD + Mouse ScrollWheel
		transform.Translate (new Vector3 (h * speed, yMouse * mouseSpeed, v * speed) 
			* Time.deltaTime, Space.World);
	}
}
