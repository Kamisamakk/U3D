using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//保存每一波敌人生成所需的属性
[System.Serializable]//可序列化,才可以显示在面板
public class Wave
{
    public GameObject enemyprefabs;
    public int count;//个数
    public float rate;//间隔

}
