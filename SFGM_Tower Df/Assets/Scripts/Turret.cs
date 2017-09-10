using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turret  {
	//Turret Data
	public GameObject turretPrefab;
	public int cost = 0;
	public GameObject turrentPrefabUpd;
	public int updCost = 0;
	public TurretType type;
}
public enum TurretType{

	LaserTurret,
	MissileTurret,
	StandardTurret,
	UndefinedTurret
}