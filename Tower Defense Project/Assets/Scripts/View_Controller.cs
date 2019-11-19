using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Controller : MonoBehaviour
{
    public float speed = 20f;
    public float mousespeed = 600f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h*speed, mouse*mousespeed, v* speed) * Time.deltaTime,Space.Self);
        
    }
}
