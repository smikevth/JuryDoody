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
    [SerializeField]
    AudioSource sfxPlayer;
    [SerializeField]
    List<AudioClip> clackClips;

    private QAndA currentQandA;
    private int clackTimer = 0;
    private int clackTick = 2; //with the timer, slows down clacking

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
        NextQuestion();
    }

    private void ResetData()
    {
        gameData.selectionScore = 0;
        MakeQuestionsList();
    }

    //makes a list of indexes of questions to use. For now, just all questions in gamedata
    private void MakeQuestionsList()
    {
        gameData.questionsList = new List<QAndA>();
        //2 normal, 1 mildly silly, 1 no brainer, 1 very silly
        gameData.questionsList.Add(RandomQuestionFromList(gameData.normalQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.normalQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.sillyQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.noBrainerQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.sillierQuestions));


        /*for (int i = 0; i < gameData.questions.Count; i++)
        {
            gameData.questionsIndexes.Add(i);
        }*/
    }

    //helper function to get a qanda in a question list at random.
    private QAndA RandomQuestionFromList(List<QAndA> list)
    {
        
        int rando = Random.Range(0, list.Count);
        QAndA question = list[rando];
        //get a new question if the random one was already picked. this will probably break if there are more calls to the function than questions so don't do that
        while (gameData.questionsList.Contains(list[rando]))
        {
            rando = Random.Range(0, list.Count);
            question = list[rando];
        }
        return question;
    }

    //selects the question based on the current index and calls makequestion
    private void NextQuestion()
    {
        //check if any questions are left
        if (gameData.questionsList.Count > 0)
        {
            //select question from gameData list using questionsIndex. Will just go in order for now
            currentQandA = gameData.questionsList[0];
            MakeQuestion(currentQandA);

        }
        else
        {
            //no more questions, end game. Should refactor to own function, and need to jazz it up 
            if (gameData.selectionScore > gameData.scoreThreshold)
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

    //sets the questions text and answer buttons based what gets pased ot it
    private void MakeQuestion(QAndA qanda)
    {
        //null checks
        bool errors = false;
        if (qanda.question == null)
        {
            Debug.Log("missing question text " + qanda);
            errors = true;
        }
        if (qanda.answers[0] == null)
        {
            Debug.Log("missing answer1 text " + qanda);
            errors = true;
        }
        if (qanda.answers[1] == null)
        {
            Debug.Log("missing answer1 text " + qanda);
            errors = true;
        }
        if (!errors)
        {
            //set q and a texts
            StartCoroutine(TypeQuestion(qanda));
        }
        else
        {
            //get a new question maybe
        }
    }

    //types out the question text one letter at a time. Can click to finish. might need to make switch it to change color instead of type if word wrapping is funky
    private IEnumerator TypeQuestion(QAndA qanda)
    {
        questionText.text = "";
        gameData.textIsPrinting = true;
        for (int i=0; i< qanda.question.Length; i++)
        {
            questionText.text += qanda.question[i];
            if(!gameData.skipText)
            {
                clackTimer ++;
                if(clackTimer >= clackTick)
                {
                    int rando = Random.Range(0, clackClips.Count);
                    sfxPlayer.PlayOneShot(clackClips[rando]);
                    clackTimer -= clackTick;
                }
                    
                
                yield return new WaitForSeconds(gameData.textSpeed);
            }
        }
        gameData.textIsPrinting = false;
        gameData.skipText = false;
        answerButtons.SetActive(true);
        answerText1.text = qanda.answers[0].answer;
        answerText2.text = qanda.answers[1].answer;
    }



    public void ButtonAnswer(int index)
    {
        answerButtons.SetActive(false);
        //increment score
        int points = currentQandA.answers[index].value;
        gameData.selectionScore += points;
        gameData.reactionScore = points;
        if(currentQandA.answers[index].triggersFollowUp)
        {
            currentQandA = currentQandA.followUp;
            MakeQuestion(currentQandA);
        }
        else
        {
            //remove question index
            gameData.questionsList.RemoveAt(0);
            //make next question
            NextQuestion();
        }
    }

    public void SkipText(InputAction.CallbackContext context)
    {
        if(context.performed && gameData.textIsPrinting && !gameData.skipText)
        {
            gameData.skipText = true;
        }
    }
}
