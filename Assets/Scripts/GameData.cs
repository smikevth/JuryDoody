using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a single asset of this is used to store data used in gameplay
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameData : ScriptableObject
{
    [HideInInspector]
    public int selectionScore;
    public int scoreThreshold; //get selected/lose above threshold



}
