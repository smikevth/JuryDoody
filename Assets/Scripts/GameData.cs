using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a single asset of this is used to store data used in gameplay
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameData : ScriptableObject
{
    [HideInInspector]
    public int selectionScore; //player's current score
    public int scoreThreshold = 5; //get selected/lose above threshold
    public List<QAndA> questions; //list of all questions and their answers/data
    [HideInInspector]
    public List<int> questionsIndexes; //a list of the indexes of questions that haven't been asked yet
}