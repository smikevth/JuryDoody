using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//a single asset of this is used to store data used in gameplay
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameData : ScriptableObject
{
    public UnityEvent OnScoreChange;
    private int SelectionScore; //player's current score
    [HideInInspector]
    public int selectionScore {
        get => SelectionScore;
        set
        {
            SelectionScore = value;
            OnScoreChange?.Invoke();
        }
    }
    public int scoreThreshold = 5; //get selected/lose above threshold

    //lists of all questions and their answers/data
    public List<QAndA> normalQuestions; //realish
    public List<QAndA> sillyQuestions; //weird but not too far out i guess
    public List<QAndA> noBrainerQuestions; //super easy for gags if there's time
    public List<QAndA> sillierQuestions; //I'm old greeeeeeeg

    [HideInInspector]
    public List<QAndA> questionsList; //a list of questions to use in current game
    
    public float textSpeed = 0.05f; //s to wait between letters
    [HideInInspector]
    public bool skipText = false; //set to true to click through text printing
    [HideInInspector]
    public bool textIsPrinting = false; //set to true while printing text

}
