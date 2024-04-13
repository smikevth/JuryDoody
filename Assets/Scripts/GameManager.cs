using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Runs the gameplay logic. Uses data in the gameData asset.
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameData gameData;
    [SerializeField]
    TMP_Text questionText;

    //the two buttons are set for now, but I'd like to instantiate them as prefabs later for the option of more buttons and zaniness.
    [SerializeField]
    TMP_Text answerText1;
    [SerializeField]
    TMP_Text answerText2;

    private QAndA currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        ResetData();
        //make 1st Q and A
        MakeQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetData()
    {
        gameData.selectionScore = 0;
        gameData.questionsIndexes = new List<int>();
        for (int i=0; i<gameData.questions.Count; i++)
        {
            gameData.questionsIndexes.Add(i);
        }
    }

    private void MakeQuestion()
    {
        //select question from gameData list using questionsIndex. Will just go in order for now
        currentQuestion = gameData.questions[gameData.questionsIndexes[0]];
        //set q and a texts
        if(currentQuestion.question != null)
        {
            questionText.text = currentQuestion.question;
        }
        else
        {
            Debug.Log("missing question text at index " + gameData.questionsIndexes[0]);
        }
        if(currentQuestion.answers[0] != null)
        {
            answerText1.text = currentQuestion.answers[0].answer;
        }
        else
        {
            Debug.Log("missing answer1 text at index " + gameData.questionsIndexes[0]);
        }
        if (currentQuestion.answers[1] != null)
        {
            answerText2.text = currentQuestion.answers[1].answer;
        }
        else
        {
            Debug.Log("missing answer1 text at index " + gameData.questionsIndexes[1]);
        }
    }

    public void ButtonAnswer(int index)
    {
        //debug to make sure the right thing is happening
        Debug.Log(currentQuestion.answers[index].answer + " was pressed");
        //increment score
        gameData.selectionScore += currentQuestion.answers[index].value;
        //remove question index
        gameData.questionsIndexes.RemoveAt(0);
        //make next question
        MakeQuestion();
    }
}
