using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour {

	public GameObject endUI;
	public Text msg;
	public static GameMgr instance;
	private EnemySpawner enemySp;

	void Awake(){
		if(instance == null){
			instance = this;
		}
		enemySp = GetComponent<EnemySpawner> ();
	}

	public void Win(){
		msg.text = "You Win".ToString();
		endUI.SetActive (true);
	}

	public void Failed(){
		enemySp.StopSpawn ();
		msg.text = "You Lose".ToString();
		endUI.SetActive (true);
	}

	public void OnBtnRestart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void OnBtnMenu(){
		SceneManager.LoadScene (0);
	}

}
