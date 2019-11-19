using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapCube : MonoBehaviour
{
    [HideInInspector]//不显示在面板
    public GameObject turretGo;//保存当前cube身上的炮台
    public GameObject buildeffect;
    private new Renderer renderer;
    [HideInInspector]
    public bool isUpgraded = false;//是否升级过，初始值false
    [HideInInspector]
    public TurretData turretData;//要建造的炮台
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = Instantiate(turretData.TurretPrefabs, transform.position,Quaternion.identity);
        GameObject effect = Instantiate(buildeffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;//已升级不做操作
        Destroy(turretGo);
        isUpgraded = true;
        turretGo = Instantiate(turretData.TurretUpgradedPrefabs, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(buildeffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void DestoryTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;
        GameObject effect = Instantiate(buildeffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    private void OnMouseEnter()
    {
        if(turretGo==null&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            renderer.material.color = Color.red;
            
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;

    }
}
