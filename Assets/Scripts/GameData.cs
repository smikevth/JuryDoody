using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//a single asset of this is used to store data used in gameplay
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameData : ScriptableObject
{
    [HideInInspector]
    public int selectionScore = 0; //player's current score
    public int scoreThreshold = 4; //get selected/lose above threshold

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


    public UnityEvent OnReactionChange;
    private int ReactionScore = 0; //amount score changed with last answer
    public int reactionScore
    {
        get => ReactionScore;
        set
        {
            ReactionScore = value;
            OnReactionChange?.Invoke();
        }
    }
    public UnityEvent OnDeanChange;
    private bool DeanReact; // bool to trigger the dean's reaction to a question
    public bool deanReact
    {
        get => DeanReact;
        set
        {
            DeanReact = value;
            OnDeanChange?.Invoke();
        }
    }

    [HideInInspector]
    public bool isGameOver = true;

}
