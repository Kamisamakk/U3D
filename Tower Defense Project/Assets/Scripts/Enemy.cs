using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private static Transform[] positions;
    private int index = 0;//索引编号
    public float speed = 10;
    public float hp = 150;
    private float totalHp;
    private Slider HPslider;
    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        totalHp = hp;
        positions = WayPoints.positions;
        HPslider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (index > positions.Length - 1) return;//判断索引编号是否越界
        transform.Translate((positions[index].position - transform.position).normalized*Time.deltaTime*speed);//单位化normalized
        if(Vector3.Distance(positions[index].position,transform.position)<0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
            
    }
    void ReachDestination()//敌人到达终点
    {
        GameManager.Instance.Failed();
        Destroy(this.gameObject);

    }
    private void OnDestroy()
    {
        Enemy_Spawner.CountEnemyAlive--;
    }
    public void TakeDamage(float damage)//受到伤害
    {
        if (hp <= 0) return;
        hp -= damage;
        HPslider.value = (float)hp / totalHp;
        if(hp<=0)
        {
            Die();
        }
    }
    void Die()
    {
       GameObject effect= Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.1f);
        Destroy(this.gameObject);
    }
}
