using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //手动设置分辨率
        Screen.SetResolution(1920,1080,true);
        Invoke("Load",2f);
    }

    void Load()
    {
        //异步加载
        SceneManager.LoadSceneAsync(1);
    }
}
