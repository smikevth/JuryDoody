using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    private float swayTimer = 0.0f;
    [SerializeField]
    float swayTick = 3.0f; //when timer above tick do thing
    [SerializeField]

    private Quaternion initialRotation;
    private Quaternion swayTarget;




    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        swayTarget = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        swayTimer += Time.deltaTime;
        if(swayTimer >= swayTick)
        {
            //GetTargetRotation();
            
        }
    }

    private void GetTargetRotation()
    {

    }
}
