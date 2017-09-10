using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 50;
	public float speed = 20;
	private Transform target;
	public GameObject explosionEfPrefab;

	public void SetTarget(Transform t){
		this.target = t;
	}

	void Update(){
		if(target == null){
			AttachedEnemy ();
			return;
		}
		transform.LookAt (target);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		DoOnEnemy ();
	}

	void DoOnEnemy(){
		Vector3 dir = this.target.position - transform.position;
		if(dir.magnitude <= 1.2f){
			target.GetComponent<Enemy> ().Damage(damage);
			AttachedEnemy ();
		}
	}

	void AttachedEnemy(){
		//show explosion
		GameObject ef = GameObject.Instantiate(explosionEfPrefab,this.transform.position, this.transform.rotation);
		Destroy (ef, 1);
		//destory self
		Destroy(this.gameObject);
	}

}
