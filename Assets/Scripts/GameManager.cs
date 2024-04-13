using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Runs the gameplay logic. Uses data in the gameData asset.
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameData gameData;


    // Start is called before the first frame update
    void Start()
    {
        ResetData();
        //make 1st Q and A 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetData()
    {
        gameData.selectionScore = 0;
    }
}
