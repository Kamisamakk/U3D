using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform start;
    public float waveRate = 0.2f;
    public static int CountEnemyAlive=0;//敌人存活数

    private Coroutine coroutine;
    void Start()
    {
        coroutine= StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i=0;i<wave.count;i++)//遍历敌人
            {
                Instantiate(wave.enemyprefabs,start.position,Quaternion.identity);//Quaternion.identity无旋转
                CountEnemyAlive++;
                if (i!=wave.count-1)
                yield return new WaitForSeconds(wave.rate);//设置时间间隔
            }
            while(CountEnemyAlive>0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while(CountEnemyAlive>0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }
}
