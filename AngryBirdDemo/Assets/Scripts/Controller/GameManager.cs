using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<Bird> birdList;
    public List<Pig> pigList;
    private Vector3 originPos;//初始化的位置
    public static GameManager gameManager;

   
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;
    private void Awake()
    {
        if(gameManager==null)
        {
            gameManager = this;
        }
        //解决切换小鸟时突然弹起的问题
        if(birdList.Count>0)
        {
            originPos = birdList[0].transform.position;
        }
        
    }
    private void Start()
    {
        Init();
    }
    /// <summary>
    /// 初始化,第一只小鸟才可以启用弹簧组件
    /// </summary>
    private void Init()
    {
        for (int i = 0; i < birdList.Count; i++)
        {
            if (i == 0)//第一只小鸟
            {
                birdList[i].transform.position = originPos;
                birdList[i].enabled = true;
                birdList[i].springJoint.enabled = true;
            }
            else
            {
                birdList[i].enabled = false;
                birdList[i].springJoint.enabled = false;
            }
        }
    }
    /// <summary>
    /// 游戏逻辑
    /// </summary>
    public void GameLogic()
    {
        if(pigList.Count>0)
        {
            if(birdList.Count>0)
            {
                //下一只小鸟飞
                Init();
            }
            //用完小鸟输
            else
            {
                lose.SetActive(true);
            }
        }
        //猪打完赢
        else
        {
            //赢
            win.SetActive(true);
        }
    }

    public void ShowStar()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        //遍历当前小鸟个数
        for (int i = 0; i <=birdList.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log(i);
            stars[i].SetActive(true);
        }
    }
    public void Resume()
    {

    }
    public void Repaly()
    {
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {

    }
}
