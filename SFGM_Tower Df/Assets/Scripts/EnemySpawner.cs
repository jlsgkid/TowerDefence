using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Wave[] waves;
	public Transform startPos;
	public float timeBetweenSpawn = 0.2f;
	public static int countOfEnemyIsAlive = 0;

	void Start(){
		StartCoroutine (SpawnEnemy ());
	}

	IEnumerator SpawnEnemy(){

		foreach (Wave wave in waves) {

			for(int i = 0; i < wave.count; i++){
				GameObject.Instantiate (wave.enemyPrefab, startPos.position, Quaternion.identity);
				countOfEnemyIsAlive++;
				if(i != wave.count-1)
					yield return new WaitForSeconds (wave.rate);
			}
			while (countOfEnemyIsAlive > 0) {
				yield return 0;
			}
			yield return new WaitForSeconds (timeBetweenSpawn);
		}
	}

	void Update(){
		Debug.Log (countOfEnemyIsAlive);
	}
}
