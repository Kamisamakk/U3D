using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    private bool isMove = true;//是否可以移动
    private bool isFly=false;//是否飞行
    
    public float maxDis = 1.5f;
    public float smooth = 3f;
    [HideInInspector]
    public SpringJoint2D springJoint;
    protected Rigidbody2D rg;

    public LineRenderer right;
    public LineRenderer left;
    public Transform rightPos;
    public Transform leftPos;

    public AudioClip select;
    public AudioClip fly;
   

    public GameObject boom;//撞击特效
    private void Awake()
    {
        springJoint = transform.GetComponent<SpringJoint2D>();
        rg = transform.GetComponent<Rigidbody2D>();
        springJoint.enabled = false;
    }
    //鼠标按下
    private void OnMouseDown()
    {
        if(isMove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
        
        
    }
    //鼠标抬起
    private void OnMouseUp()
    {
        if (isMove) 
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            isMove = false;
        }
            
    }

    private void Update()
    {
        //如果鼠标一直按下，设置小鸟位置
        if(isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//将鼠标的屏幕坐标转换为世界坐标
            //设置Z轴坐标让其正常显示
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);
            //拉动小鸟的最大长度范围
            if(Vector3.Distance(transform.position,rightPos.position)>maxDis)
            {
                Vector3 pos =(transform.position - rightPos.position).normalized;//向量单位化 1
                pos *= maxDis;//最大长度范围
                transform.position = pos + rightPos.position;
            }
            DrawLine();
        }
        //相机跟随
        float posX = transform.position.x;//记录小鸟初始位置
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15f), Camera.main.transform.position.y,Camera.main.transform.position.z), smooth * Time.deltaTime);//设置相机位置

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Skill();
            }
        }
    }

    private void Fly()
    {
        isFly = true;
        AudioPlay(fly);
        //禁用linerenderer
        right.enabled = false;
        left.enabled = false;
        springJoint.enabled = false;
        Invoke("NextBird", 4f);
    }

    /// <summary>
    /// 画线
    /// </summary>
    private void DrawLine()
    {
        //激活linerenderer
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    private void NextBird()
    {
        GameManager.gameManager.birdList.Remove(this);//列表移除
        Destroy(gameObject);
        GameObject boomObj= ObjectPool.objectPool.GetGameObject(boom);
        boomObj.transform.position = transform.position;
        boomObj.SetActive(true);
        GameManager.gameManager.GameLogic();
    }

    public void AudioPlay(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isFly = false;
    }

    //技能
    public virtual void Skill()
    {
        isFly = false;
    }
}
