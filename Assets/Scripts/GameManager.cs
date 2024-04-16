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
    TMP_Text textBox;

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
    [SerializeField]
    GameObject winMessage;
    [SerializeField]
    GameObject loseMessage;

    [SerializeField]
    Button lowButton;
    [SerializeField]
    Button medButton;
    [SerializeField]
    Button highButton;

    private QAndA currentQandA;
    private int clackTimer = 0;
    private int clackTick = 2; //with the timer, slows down clacking
    private float textWait = 1.5f; //little wait between texts

    // Start is called before the first frame update
    void Start()
    {
        gameData.isGameOver = true;
        StartCoroutine(IntroSequence());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IntroSequence()
    {
        yield return TypeWords("Gavel! Gavel! Gavel!");
        yield return new WaitForSeconds(textWait);
        yield return TypeWords("OK counsel, you may begin questioning Juror #1.");
        yield return new WaitForSeconds(textWait);
        InitializeGame();
    }

    public void InitializeGame()
    {
        ResetData();
        restartButton.SetActive(false);
        loseMessage.SetActive(false);
        winMessage.SetActive(false);
        gameData.isGameOver = false;
        //make 1st Q and A
        NextQuestion();
    }

    private void ResetData()
    {
        gameData.selectionScore = 0;
        gameData.deanReact = false;
        MakeQuestionsList();
    }

    //makes a list of indexes of questions to use. For now, just all questions in gamedata
    private void MakeQuestionsList()
    {
        gameData.questionsList = new List<QAndA>();
        //2 normal, 2 mildly silly, 1 very silly. Make sure to not get more questions than there are
        gameData.questionsList.Add(RandomQuestionFromList(gameData.normalQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.normalQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.sillyQuestions));
        gameData.questionsList.Add(RandomQuestionFromList(gameData.sillyQuestions));
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
        //get a new question if the random one was already picked. this will probably break if there are more calls to the function using the same list than questions in that list so don't do that
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
            gameData.isGameOver = true;
            StartCoroutine(EndingSequence());


            
        }
    }

    private IEnumerator EndingSequence()
    {
        yield return TypeWords("Gavel! Gavel! Gavel!");
        yield return new WaitForSeconds(textWait);
        if (gameData.selectionScore > gameData.scoreThreshold)
        {
            StartCoroutine(TypeWords("The prosecution sees no reason to dismiss Juror #1."));
            yield return new WaitForSeconds(textWait);
            loseMessage.SetActive(true);
        }
        else
        {
            StartCoroutine(TypeWords("The prosecution would like to thank and dismiss Juror #1."));
            yield return new WaitForSeconds(textWait);
            winMessage.SetActive(true);
        }
        yield return new WaitForSeconds(textWait/2);
        restartButton.SetActive(true);
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
        yield return TypeWords(qanda.question);

        answerButtons.SetActive(true);
        answerText1.text = qanda.answers[0].answer;
        answerText2.text = qanda.answers[1].answer;
    }

    private IEnumerator TypeWords(string words)
    {
        textBox.text = "";
        gameData.textIsPrinting = true;
        for (int i = 0; i < words.Length; i++)
        {
            textBox.text += words[i];
            if (!gameData.skipText)
            {
                clackTimer++;
                if (clackTimer >= clackTick)
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

    }

    public void ButtonAnswer(int index)
    {
        answerButtons.SetActive(false);
        //increment score
        int points = currentQandA.answers[index].value;
        gameData.selectionScore += points;
        gameData.reactionScore = points;
        if(currentQandA.silliness == 69 && points < 0)
            //question that triggers the dean's reaction
        {
            gameData.deanReact = true;
        }

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

    public void SetQuality(string quality)
    {
        lowButton.interactable = true;
        medButton.interactable = true;
        highButton.interactable = true;
        switch (quality)
        {
            case "Low":
                QualitySettings.SetQualityLevel(0, true);
                lowButton.interactable = false;
                break;
            case "Medium":
                QualitySettings.SetQualityLevel(1, true);
                medButton.interactable = false;
                break;
            case "High":
                QualitySettings.SetQualityLevel(2, true);
                highButton.interactable = false;
                break;
        }
    }
}
