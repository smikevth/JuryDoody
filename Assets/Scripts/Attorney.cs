using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attorney : MonoBehaviour
{
    [SerializeField]
    GameData gameData;
    [SerializeField]
    GameObject cam;
    private bool paceSet = true; //to toggle between pace direction and facing camera
    private float range = 2.0f; //pacing range. attorney starts at x=-2 and will pace to x=2 they turn
    private int direction = 1; //should be 1 for moving toward camera right, -1 for left
    private float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData.textIsPrinting)
        {
            //talking
            JimTheCamera();
        }
        else
        {
            //pacing
            if(!paceSet)
            {
                SetPace();
            }
            Pace();
        }
    }

    private void JimTheCamera()
    {
        transform.LookAt(cam.transform);
        paceSet = false;
        //animate mouth if I add one
    }

    private void SetPace()
    {
        Quaternion target = Quaternion.Euler(0, 90.0f*direction, 0);
        transform.rotation = target;
        paceSet = true;
        Debug.Log("set pace");
    }

    private void Pace()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (direction < 0 && transform.position.x < -range) {
            direction = 1;
            SetPace();
        }
        else if (direction > 0 && transform.position.x > range)
        {
            direction = -1;
            SetPace();
        }
    }
}
