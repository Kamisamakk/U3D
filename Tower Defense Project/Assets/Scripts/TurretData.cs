using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TurretData
{
    public GameObject TurretPrefabs;
    public int cost;//价格
    public GameObject TurretUpgradedPrefabs;
    public int costUpgraded;//升级价格
    public TurretType type;
}
public enum TurretType
{
    LaserTurret,
    MissileTurret,
    StandardTurret
}
