using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private List<GameObject> enemys=new List<GameObject>();
    private void OnTriggerEnter(Collider other)//进入触发器
    {
        if(other.tag.Equals("Enemy"))
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)//离开触发器
    {
        if (other.tag.Equals("Enemy"))
        {
            enemys.Remove(other.gameObject);
        }
    }

    public float attackRateTime = 1; //多少秒攻击一次
    private float timer = 0;//计时器，判断时间是否到
    public GameObject bulletPrefabs;//炮弹
    public Transform firePsition;//炮弹位置
    public Transform head;
    public bool useLaser = false;//默认不使用激光
    public float DamageRate = 70;//激光伤害值 
    public LineRenderer laserRenderer;
    public GameObject lasereffect;
    private void Start()
    {
        timer = attackRateTime;
    }
    private void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPostition = enemys[0].transform.position;//获取敌人位置
            targetPostition.y = head.position.y;//保持Y轴高度一致
            head.LookAt(targetPostition);//望向敌人

        }
        if (useLaser == false)//
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;//归零
                Attack();
            }
        }
        else if(enemys.Count>0)//激光攻击方式
        {
            if(laserRenderer.enabled == false)
            {
                laserRenderer.enabled = true;
                lasereffect.SetActive(true);
            }
            if (enemys[0] == null)//第一个敌人死亡
            {
                UpdateEnemys();
                //return;
            }
            if (enemys.Count>0)
            {

                //StartCoroutine(LaserAttack());
                laserRenderer.SetPositions(new Vector3[] { firePsition.position, enemys[0].transform.position });//firePsition开始位置，enemys[0]结束位置
                enemys[0].GetComponent<Enemy>().TakeDamage(DamageRate*Time.deltaTime);
                lasereffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;//获取炮台位置
                pos.y = enemys[0].transform.position.y;//炮台高度和敌人一致
                lasereffect.transform.LookAt(pos);
            }

        }
        else
        {
            lasereffect.SetActive(false);
            laserRenderer.enabled = false;
            
        }
        
        
       
    }
    IEnumerator LaserAttack()//激光攻击
    {
      
        laserRenderer.SetPositions(new Vector3[] { firePsition.position, enemys[0].transform.position });//firePsition开始位置，enemys[0]结束位置
        yield return new WaitForSeconds(Time.deltaTime);
        laserRenderer.enabled = false;
    }
    void Attack()
    {
       if(enemys[0]==null)
        {
            UpdateEnemys();
            //return;
        }
       if(enemys.Count>0)
        {
            GameObject bullet = Instantiate(bulletPrefabs, firePsition.position, firePsition.rotation);
            //bulletPrefabs.transform.Translate();
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }else
        {
            timer = attackRateTime;
        }
        
    }
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for(int i=0;i<enemys.Count;i++)
        {
            if(enemys[i]==null)
            {
                emptyIndex.Add(i);//保存空元素的索引号
            }
            
        }
        for(int i=0;i<emptyIndex.Count;i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }
}
