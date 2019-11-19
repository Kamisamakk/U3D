using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public int starNum = 0;
    private bool isSelect = false;
    public GameObject locks;
    public GameObject stars;
    public GameObject panel;
    public GameObject mapPanel;
    public Text starsText;//星星数量

    public int startLevel = 1;//开始关卡
    public int endLevel = 4;//结束关卡
    private void Start()
    {
        //清除数据
        //PlayerPrefs.DeleteAll();
        //星星总数大于当前关卡需要的星星数量
        if (PlayerPrefs.GetInt("totalNum", 0) >= starNum)
        {
            isSelect = true;
            print(PlayerPrefs.GetInt("totalNum"));
        }

        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
            //星星数量显示
            int count = 0;
            for (int i = startLevel; i <= endLevel; i++)
            {
                count += PlayerPrefs.GetInt("level" + i.ToString(),0);
            }
            starsText.text = count.ToString()+"/12";
        }
    }

    public void OnClick()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            mapPanel.SetActive(false);
        }
    }

    public void PanelSelect()
    {
        panel.SetActive(false);
        mapPanel.SetActive(true);
    }
}
