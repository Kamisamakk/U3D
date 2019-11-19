using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite unlockSprite;
    private Image image;
    public GameObject[] stars;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        //如果第一个子物体名字相等(第一关)
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {
            //上一个关卡
            int beforeLevelNum = int.Parse(gameObject.name)-1;
            //除第一个关卡外，之前的关卡星星数大于1解锁
            if (PlayerPrefs.GetInt("level" + beforeLevelNum.ToString()) >= 1)
            {
                isSelect = true;
            }
        }

        if (isSelect)
        {
            image.overrideSprite = unlockSprite;
            transform.Find("num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("level" + gameObject.name);//获取对应关卡星星的数量
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            //存入选择关卡的名字
            PlayerPrefs.SetString("currentLevel","level"+gameObject.name);
            //加载关卡
            SceneManager.LoadScene(2);
        }
    }

    public void Return()
    {
        SceneManager.LoadScene(1);
    }
}
