using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float minSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;
    public bool isPig = false;

    public AudioClip hurtCollision;
    public AudioClip dead;
    public AudioClip birdCollision;
    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.relativeVelocity.magnitude);
        if(collision.gameObject.tag=="Bird")
        {
            AudioPlay(birdCollision);
        }
        //相对速度数值化
        //死亡
        if(collision.relativeVelocity.magnitude>maxSpeed)
        {
            Dead();
        }
        //受伤
        else if(collision.relativeVelocity.magnitude>minSpeed&&
            collision.relativeVelocity.magnitude<maxSpeed)
        {
            spriteRenderer.sprite = hurt;
            AudioPlay(hurtCollision);
        }
       
    }

    public void Dead()
    {
        if(isPig)
        {
            GameManager.gameManager.pigList.Remove(this);
        }
        Destroy(gameObject);
        GameObject boomObj= ObjectPool.objectPool.GetGameObject(boom);
        boomObj.transform.position = transform.position;
        boomObj.transform.rotation = Quaternion.identity;
        boomObj.SetActive(true);
        GameObject scoreObj = ObjectPool.objectPool.GetGameObject(score);
        scoreObj.transform.position = transform.position+new Vector3(0,0.5f,0);
        scoreObj.transform.rotation = Quaternion.identity;
        scoreObj.SetActive(true);
        AudioPlay(dead);
    }

    public void AudioPlay(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
