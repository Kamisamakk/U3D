using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool objectPool;//单例对象
    public GameObject[] prefabsType;//预制体种类
    public List<GameObject>[] prefabsTypeList;//每种预制体的集合
    public int[] amountToBuffer;//种类集合长度
    public int defaultBufferAmount = 10;//每种集合的默认长度
    public bool isCanGrow = true;//能否自增

    GameObject pool;
    private void Awake()
    {
        if (objectPool == null)
        {
            objectPool = this;
        }
        else Destroy(gameObject);

        Init(gameObject.transform);
    }
    //初始化
    private void Init(Transform parent)
    {
        pool = new GameObject("ObjectPool");
        pool.transform.SetParent(parent);
        prefabsTypeList = new List<GameObject>[prefabsType.Length];//初始化预制体种类集合
        int index = 0;
        foreach (GameObject objectPrefab in prefabsType)
        {
            prefabsTypeList[index] = new List<GameObject>();//初始化每种预制体集合
            int bufferAmout;//每种预制体集合长度
            if (index < amountToBuffer.Length)
            {
                bufferAmout = amountToBuffer[index];//初始化长度
            }
            else bufferAmout = defaultBufferAmount;//没有设置就默认值
            //初始化回收池
            for (int i = 0; i < bufferAmout; i++)
            {
                GameObject obj = Instantiate(objectPrefab);//生成种类预制体
                obj.tag = objectPrefab.tag;//设置tag
                PushObject(obj);//放入对象池
            }
            index++;
        }
    }
    /// <summary>
    /// 取出预制体
    /// </summary>
    /// <param name="objectType">需要生成的预制体对象</param>
    /// <returns></returns>
    public GameObject GetGameObject(GameObject objectType)
    {
        //遍历预制体种类
        for (int i = 0; i < prefabsType.Length; i++)
        {
            GameObject prefab = prefabsType[i];//预制体种类赋值
            if(prefab.tag.Equals(objectType.tag))
            {
                if(prefabsTypeList[i].Count>0)//如果对应预制体回收池不为空
                {
                    GameObject newObj = prefabsTypeList[i][0];//取出对应预制体集合中的第一个
                    prefabsTypeList[i].RemoveAt(0);//移除取出的预制体
                    newObj.transform.parent = null;
                    return newObj;//返回需要生成的预制体
                }
                else if (isCanGrow)
                {
                    Debug.Log("请求时对象池种类增长: " + objectType + ". 考虑扩充.");
                    //不存在对应的预制体，尝试生成新的预制体
                    GameObject obj = Instantiate(prefabsType[i]) as GameObject;
                    obj.tag = prefabsType[i].tag;
                    return obj;
                }
                break;
            }
        }
        return null;
    }

    /// <summary>
    /// 回收预制体
    /// </summary>
    /// <param name="obj">初始化的预制体</param>
    public void PushObject(GameObject obj)
    {
        //遍历预制体种类，对比名称；相同放入对应的集合
        for (int i = 0; i < prefabsType.Length; i++)
        {
            if(obj.tag.Equals(prefabsType[i].tag))
            {
                obj.SetActive(false);//设置状态为禁用
                obj.transform.parent = pool.transform;//设置父物体
                prefabsTypeList[i].Add(obj);//放入对应的集合
                return;
            }
        }
    }
}
