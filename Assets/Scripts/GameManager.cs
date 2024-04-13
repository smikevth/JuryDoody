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
    GameObject answerButtons;
    [SerializeField]
    TMP_Text answerText1;
    [SerializeField]
    TMP_Text answerText2;
    [SerializeField]
    GameObject restartButton;

    private QAndA currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeGame()
    {
        ResetData();
        ToggleRestartButton(false);
        //make 1st Q and A
        MakeQuestion();
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
        //check if any questions are left
        if(gameData.questionsIndexes.Count > 0)
        {
            //select question from gameData list using questionsIndex. Will just go in order for now
            currentQuestion = gameData.questions[gameData.questionsIndexes[0]];
            //null checks
            bool errors = false;
            if (currentQuestion.question == null)
            {
                Debug.Log("missing question text at index " + gameData.questionsIndexes[0]);
                errors = true;
            }
            if (currentQuestion.answers[0] == null)
            {
                Debug.Log("missing answer1 text at index " + gameData.questionsIndexes[0]);
                errors = true;
            }
            if (currentQuestion.answers[1] == null)
            {
                Debug.Log("missing answer1 text at index " + gameData.questionsIndexes[1]);
                errors = true;
            }
            if (!errors)
            {
                //set q and a texts
                questionText.text = currentQuestion.question;
                answerText1.text = currentQuestion.answers[0].answer;
                answerText2.text = currentQuestion.answers[1].answer;
            }
            else
            {
                //get a new question maybe
            }
        }
        else
        {
            //no more questions, end game
            if(gameData.selectionScore > gameData.scoreThreshold)
            {
                questionText.text = "You have been selected, you lose";
            }
            else
            {
                questionText.text = "You have been excused, you win";
            }
            Debug.Log("final score " + gameData.selectionScore);
            ToggleRestartButton(true);
        }

    }

    //when true, the restart button will be enabled and answer buttons will be disabled
    private void ToggleRestartButton(bool value)
    {
        restartButton.SetActive(value);
        answerButtons.SetActive(!value);
    }

    public void ButtonAnswer(int index)
    {

        Debug.Log("current score" + gameData.selectionScore);
        //increment score
        gameData.selectionScore += currentQuestion.answers[index].value;
        //remove question index
        gameData.questionsIndexes.RemoveAt(0);
        //make next question
        MakeQuestion();
    }
}
