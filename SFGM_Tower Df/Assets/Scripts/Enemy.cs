using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform[] position;
	private int index = 0;
	[SerializeField] private float speed = 0.0f;

	// Use this for initialization
	void Start () {
		position = WayPoints.positions;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		
		if (index > position.Length - 1) {
			ReachDestination ();
			return;
		}
		this.transform.Translate ((position [index].position - transform.position).normalized 
			* Time.deltaTime * speed);
		if (Vector3.Distance (position [index].position, transform.position) < 0.2f) {
			index++;
		}
	}

	void ReachDestination(){
		OnDestory ();
		GameObject.Destroy (this.gameObject);
	}

	void OnDestory(){
		EnemySpawner.countOfEnemyIsAlive--;
	}
}
