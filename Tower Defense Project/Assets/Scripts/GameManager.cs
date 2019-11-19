using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject EndUI;
    public Text endtext;
    public static GameManager Instance;
    private Enemy_Spawner enemy_Spawner;

    private void Awake()
    {
        Instance = this;
        
    }
    public void Win()
    {
        EndUI.SetActive(true);
        endtext.text = "胜 利";

    }

    public void Failed()
    {
        enemy_Spawner.Stop();
        EndUI.SetActive(true);
        endtext.text = "失 败";
    }

    public void OnButtonRetry()
    {       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加载游戏场景
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
