using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    public List<Pig> enemyList=new List<Pig>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Env") && !other.gameObject.tag.Equals("Bird"))
        {
            enemyList.Add(other.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Env") && !other.gameObject.tag.Equals("Bird"))
        {
            enemyList.Remove(other.gameObject.GetComponent<Pig>());
        }
    }

    public override void Skill()
    {
        base.Skill();
        print("技能");
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Dead();
            }
        }
        OnClear();
    }

    private void OnClear()
    {
        rg.velocity = Vector3.zero;
        GameObject birdBoom = ObjectPool.objectPool.GetGameObject(boom);
        birdBoom.transform.position = transform.position;
        birdBoom.SetActive(true);
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    protected override void NextBird()
    {
        base.NextBird();
        GameManager.gameManager.birdList.Remove(this);//列表移除
        Destroy(gameObject);
        GameManager.gameManager.GameLogic();
    }
}
