using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    //动画播放完显示星星
    private void Show()
    {
        GameManager.gameManager.ShowStar();
    }
}
