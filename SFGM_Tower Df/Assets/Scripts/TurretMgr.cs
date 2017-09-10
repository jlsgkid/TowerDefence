 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretMgr : MonoBehaviour {

	public Turret laserTurret;
	public Turret missileTurret;
	public Turret standardTurret;
	private int money = 500;
	[SerializeField] private Text moneyText;
	[SerializeField] private Turret selectedTurret;
	[SerializeField] private Animator moneyAnim;

	//UpdUI Contrl
	[SerializeField] private GameObject updCanvas;
	[SerializeField] private Button btnUpd;
	//The selected Turret in the scene
	private MapCube selectedMapcubeInScene;

	public void OnLaserSelected(bool isOn){
		if(isOn){
			selectedTurret = laserTurret;
		}
	}

	public void OnMissileSelected(bool isOn){
		if(isOn){
			selectedTurret = missileTurret;
		}
	}

	public void OnStandardSelected(bool isOn){
		if(isOn){
			selectedTurret = standardTurret;
		}
	}
	private void ChangeMoney(int count){
		money += count;
		moneyText.text = "$" + money.ToString ();
	}

	void Update(){

		if(Input.GetMouseButtonDown(0)){
			if (EventSystem.current.IsPointerOverGameObject () == false) {
				// not on UI
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool isCollided = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
				if(isCollided){
					MapCube mapCube = hit.collider.GetComponent<MapCube> ();
					if(selectedTurret!= null && mapCube.currentTurret == null){
						//can build
						if (money > selectedTurret.cost) {
							ChangeMoney (-selectedTurret.cost);
							if(selectedTurret.turretPrefab!= null){
								mapCube.CreateTurret (selectedTurret);	
							}
						} else {
							//money is not enoungh
							moneyAnim.SetTrigger("flash");
						}
					}else if(mapCube.currentTurret != null){
						//turret update
						if (mapCube == selectedMapcubeInScene && updCanvas.activeInHierarchy) {
							//The Same Turret click twice && UI is Showed
							HideUpdUI ();
						} else {
							ShowUpdUI (mapCube.transform.position, mapCube.isUpded);
						}
						selectedMapcubeInScene = mapCube;
					}
				}
			}
		}
	}

	//UI
	void ShowUpdUI(Vector3 pos, bool isDisableUpd= false){
		updCanvas.SetActive (true);
		updCanvas.transform.position = pos;
		btnUpd.interactable = !isDisableUpd;
	}

	void HideUpdUI(){
		updCanvas.SetActive (false);
	}

	public void OnUpdBtnDown(){
		if (money >= selectedMapcubeInScene.curTurret.updCost) {
			ChangeMoney (-selectedMapcubeInScene.curTurret.updCost);
			selectedMapcubeInScene.UpdateTurret ();
		} else {
			moneyAnim.SetTrigger("flash");
		}
		HideUpdUI ();
	}

	public void OnDgdBtnDown(){
		selectedMapcubeInScene.DestoryTurret ();
		HideUpdUI ();
	}
}
