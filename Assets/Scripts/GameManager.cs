using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        restartButton.SetActive(false);
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
                StartCoroutine(TypeQuestion(currentQuestion.question));
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
            restartButton.SetActive(true);
        }

    }

    //types out the question text one letter at a time. Can click to finish. might need to make switch it to change color instead of type if word wrapping is funky
    private IEnumerator TypeQuestion(string theText)
    {
        questionText.text = "";
        gameData.textIsPrinting = true;
        for (int i=0; i<theText.Length; i++)
        {
            questionText.text += theText[i];
            if(!gameData.skipText)
            {
                yield return new WaitForSeconds(gameData.textSpeed);
            }
        }
        gameData.textIsPrinting = false;
        gameData.skipText = false;
        answerButtons.SetActive(true);
        answerText1.text = currentQuestion.answers[0].answer;
        answerText2.text = currentQuestion.answers[1].answer;
    }



    public void ButtonAnswer(int index)
    {
        answerButtons.SetActive(false);
        //increment score
        gameData.selectionScore += currentQuestion.answers[index].value;
        Debug.Log("current score" + gameData.selectionScore);
        //remove question index
        gameData.questionsIndexes.RemoveAt(0);
        //make next question
        MakeQuestion();
    }

    public void SkipText(InputAction.CallbackContext context)
    {
        if(context.performed && gameData.textIsPrinting && !gameData.skipText)
        {
            gameData.skipText = true;
        }
    }
}
