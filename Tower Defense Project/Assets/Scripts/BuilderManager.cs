using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuilderManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;

    public Animator moneyAnimator;
    public Text moneytext;
    private int money = 1000;

    public GameObject upgradeCanvas;

    public Button upgradeButton;

    //表示当前选择的炮台（场景中的物体）
    private MapCube selectedMapCube;

    private Animator UpgradeCanvasAnimator;
    private void Start()
    {
        FindObjectOfType<Toggle>().isOn = false;
        UpgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }
    void ChangeMoney(int change = 0)
    {
        money += change;
        moneytext.text = "￥" + money;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current.IsPointerOverGameObject() == false)//检测鼠标是否按在UI上
            {
                //炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));//距离1000，获取碰撞的层
                if (isCollider)
                {
                    MapCube mapcube = hit.collider.GetComponent<MapCube>();//得到点击的mapcube
                    if (selectedTurretData != null && mapcube.turretGo == null)
                    {
                        //可以创建                     
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapcube.BuildTurret(selectedTurretData);
                           
                        }
                        else
                        {
                            //金钱不足
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapcube.turretGo != null)
                    {
                        //升级创建
                        
                        /*1.方法1
                        if(mapcube.isUpgraded)
                        {
                            ShowUpgradeUI(mapcube.transform.position,true);
                        }
                        else
                        {
                            ShowUpgradeUI(mapcube.transform.position, false);
                        }*/
                        //2.方法2
                        if (mapcube == selectedMapCube && upgradeCanvas.activeInHierarchy)//点击同一个炮台
                        {
                           StartCoroutine( HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgradeUI(mapcube.transform.position, mapcube.isUpgraded);
                        }
                        selectedMapCube = mapcube;
                    }
                }
            }
        }
    }
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpgradeUI()
    {
        UpgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if(money>=selectedMapCube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }
        else moneyAnimator.SetTrigger("Flicker");//播放金币不足动画
        StartCoroutine(HideUpgradeUI());
    }
    public void OnDestoryButtonDown()
    {
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
