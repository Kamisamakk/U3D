﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PausePanel : MonoBehaviour
{
    public Animator animator;//暂停动画
    public GameObject button;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    //继续
    public void Resume()
    {
        Time.timeScale = 1;
        animator.SetBool("isPause", false);
        if (GameManager.gameManager.birdList.Count > 0)
        {
            if (GameManager.gameManager.birdList[0].isRelease == false)
            {
                GameManager.gameManager.birdList[0].isMove = true;
            }
        }
    }

    public void Pause()
    {
        animator.SetBool("isPause", true);
        button.SetActive(false);

        if (GameManager.gameManager.birdList.Count > 0)
        {
            if (GameManager.gameManager.birdList[0].isRelease == false)//如果没有飞出
            {
                GameManager.gameManager.birdList[0].isMove = false;
            }
        }
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        //Time.timeScale = 1;
        button.SetActive(true);
    }
}
