using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	private Transform[] position;
	private int index = 0;
	[SerializeField] private float speed = 0.0f;
	[SerializeField] private float life = 100;
	[SerializeField] private GameObject explosionEf;
	private Slider xuetiao;
	private float totalLf = 0;

	// Use this for initialization
	void Awake () {
		position = WayPoints.positions;
		totalLf = life;
		xuetiao = this.GetComponentInChildren<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.life <= 0) {
			Die ();
			return;
		}
		Move ();
	}

	void OnDestroy(){
		EnemySpawner.countOfEnemyIsAlive--;
	}

	void Move(){
		if (index > position.Length - 1) {
			ReachDestination ();
			return;
		}
		this.transform.Translate ((position [index].position - transform.position).normalized 
			* Time.deltaTime * speed);
		if (Vector3.Distance (position [index].position, transform.position) < 0.3f) {
			index++;
		}
	}

	void ReachDestination(){
		//OnDestory ();
		GameMgr.instance.Failed();
		GameObject.Destroy (this.gameObject);
	}


	public void Damage(float damage){
		this.life -= damage;
		xuetiao.value = (float)life / totalLf;
	}
	 
	void Die(){
		//EnemySpawner.countOfEnemyIsAlive--;
		GameObject ef= GameObject.Instantiate (explosionEf, transform.position, transform.rotation);
		Destroy (ef, 1.5f);
		Destroy (this.gameObject);
	}
}
