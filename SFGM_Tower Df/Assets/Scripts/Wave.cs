using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//One wave of enemy who are created
[System.Serializable]
public class Wave  {

	public GameObject enemyPrefab;
	public int count = 0;
	public float rate = 0.0f;
	
}
