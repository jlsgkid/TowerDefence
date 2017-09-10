using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
	
	[HideInInspector]
	public GameObject currentTurret;
	[SerializeField] private GameObject buildEffect;
	private Renderer render;
	[HideInInspector]
	public bool isUpded = false;

	//current Turret
	[HideInInspector]
	public Turret curTurret;

	void Awake(){
		render = GetComponent<Renderer> ();
	}

	public void CreateTurret(Turret turret){
		curTurret = turret;
		isUpded = false;
		currentTurret = Instantiate (turret.turretPrefab,transform.position,Quaternion.identity);
		GameObject effect = Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1);
	}	

	public void UpdateTurret(){
		if(isUpded == true){
			return;
		}
		Destroy (currentTurret);
		isUpded = true;
		currentTurret = Instantiate (curTurret.turrentPrefabUpd,transform.position,Quaternion.identity);
		GameObject effect = Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1);
	}

	public void DestoryTurret(){
		Destroy (currentTurret);
		isUpded = false;
		currentTurret = null;
		curTurret = null;	
		GameObject effect = Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1);
	}

	void OnMouseEnter(){
		if(currentTurret ==null && EventSystem.current.IsPointerOverGameObject()== false){
			render.material.color = Color.red;
		}
	}

	void OnMouseExit(){
		render.material.color = Color.white;
	}
}
