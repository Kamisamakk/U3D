using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;//速度
    public int damage = 50;//伤害
    private Transform target;//敌人位置
    public GameObject explosionEffectPrefabs;//爆炸特效
    private float distanceArriveTarget=1.25f;//子弹到目标距离
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {

        if(target==null)
        {
            Die();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)//向量长度magnitude
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }

        
    }

    void Die()
    {
        GameObject effect = Instantiate(explosionEffectPrefabs, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }

}
