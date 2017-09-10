using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {

	[SerializeField] 
	private List<GameObject> enemyList = new List<GameObject>();
	[SerializeField] private float attackRate = 1;
	private float timer = 0;
	[SerializeField] private GameObject bulletPrefab;
	public Transform firePos;
	public Transform TurretHead;
	//attack type laser
	[SerializeField] private bool isUserlaser = false;
	[SerializeField] private float damageRate = 70;
	public LineRenderer laserRender;
	public GameObject laserEf;

	void Awake(){
		timer = attackRate;
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Enemy")){
			enemyList.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.CompareTag("Enemy")){
			enemyList.Remove(col.gameObject);
		}
	}

	void Update(){
		//set Head
		SetTurretHead();
		//bullet's attack
		if (isUserlaser == false) {
			//attack by rate
			timer += Time.deltaTime;
			if (enemyList.Count > 0 && timer >= attackRate) {
				Attack ();
				timer = 0;
			}
		} else if (enemyList.Count > 0) {
			//laser's attack
			if (laserRender.enabled == false) {
				laserRender.enabled = true;
			}
			laserEf.SetActive (true);
			if (enemyList [0] == null)
				RemoveNullFromList ();
			if (enemyList.Count > 0) {
				laserRender.SetPositions (new Vector3[] { firePos.position, enemyList [0].transform.position });
				enemyList[0].GetComponent<Enemy> ().Damage(damageRate * Time.deltaTime);
				laserEf.transform.position = enemyList [0].transform.position;
				Vector3 pos = transform.position;
				pos.y = enemyList [0].transform.position.y;
				laserEf.transform.LookAt (pos);
			}
		} else {
			laserEf.SetActive (false);
			laserRender.enabled = false;
		}

	}

	void SetTurretHead(){
		if (enemyList.Count > 0 && enemyList [0] != null) {
			//Rotate head
			Vector3 targetPos = enemyList[0].transform.position;
			targetPos.y = TurretHead.position.y;
			TurretHead.LookAt (targetPos);
		}
	}

	void Attack(){
		if (enemyList.Count > 0 && enemyList [0] == null) {
			RemoveNullFromList ();
		}
		if (enemyList.Count > 0) {
			GameObject bullet = GameObject.Instantiate (bulletPrefab, firePos.position, firePos.rotation);
			bullet.GetComponent<Bullet> ().SetTarget (enemyList [0].transform);
		} else {
			timer = attackRate;
		}
	}

	void RemoveNullFromList(){
//		for(int i = 0; i<enemyList.Count; i++){
//			if(enemyList[i] == null || enemyList.Contains(null)){
//				enemyList.Remove (null);
//			}
//		}
		List<int> indexList = new List<int>();
		for(int i = 0; i<enemyList.Count; i++){
			if(enemyList[i] == null || enemyList.Contains(null)){
				indexList.Add (i);
			}
		}
		for(int j = 0; j<indexList.Count; j++){
			enemyList.RemoveAt (indexList [j] - j);
		}
	}
}
