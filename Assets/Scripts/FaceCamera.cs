using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the thing always face the camera
public class FaceCamera : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
    }
}
