using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private float yAngle = 0.0f; 
    //private float xAngle =0.0f; 
    private float maxX = 30.0f; //x-mouse, look left right
    private float maxY = 20.0f; //y-mouse, look up down
    private float smooth = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //get amount to rotate
        float mouseX = Mathf.Clamp(mousePos.x, 0.0f, Screen.width);
        float mouseY = Mathf.Clamp(mousePos.y, 0.0f, Screen.height);
        float rotX = (mouseX - (Screen.width / 2)) / (Screen.width / 2);
        float rotY = -1*(mouseY - (Screen.height / 2)) / (Screen.height / 2);
        Quaternion target = Quaternion.Euler(rotY * maxY, rotX * maxX, 0);
        //rotate camera
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
