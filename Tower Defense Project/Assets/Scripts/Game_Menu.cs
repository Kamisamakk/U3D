using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_Menu : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExitGame()
    {

    #if UNITY_EDITOR //在编辑器模式下运行
             UnityEditor.EditorApplication.isPlaying=false;
    #else
            Application.Quit();//退出
    #endif

    }
}
