using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attorney : MonoBehaviour
{
    [SerializeField]
    GameData gameData;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    SpriteRenderer mouth;
    [SerializeField]
    List<Sprite> mouthList; //1st in the list is the default
    [SerializeField]
    GameObject happyReaction;
    [SerializeField]
    GameObject angryReaction;

    private bool paceSet = true; //to toggle between pace direction and facing camera
    private float range = 2.0f; //pacing range. attorney starts at x=-2 and will pace to x=2 they turn
    private int direction = 1; //should be 1 for moving toward camera right, -1 for left
    private float moveSpeed = 1.0f;

    private float mouthTimer = 0.0f; //to pace mouth animation
    private float reactionHideTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameData.OnReactionChange.AddListener(SetReaction);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameData.isGameOver)
        {
            if (gameData.textIsPrinting)
            {
                //talking
                JimTheCamera();
            }
            else
            {
                //pacing
                if (!paceSet)
                {
                    SetPace();
                }
                Pace();
            }
        }
    }

    private void SetReaction()
    {
        Debug.Log("set reaction");
        if(gameData.reactionScore > 0)
        {
            happyReaction.SetActive(true);
            StartCoroutine(HideReaction(happyReaction));
        }
        else
        {
            angryReaction.SetActive(true);
            StartCoroutine(HideReaction(angryReaction));
        }
    }

    private IEnumerator HideReaction(GameObject reaction)
    {
        yield return new WaitForSeconds(reactionHideTime);
        reaction.SetActive(false);
    }

    private void JimTheCamera()
    {
        transform.LookAt(cam.transform);
        paceSet = false;
        mouthTimer += Time.deltaTime;
        if(mouthTimer >= gameData.textSpeed*2)
        {
            mouthTimer -= gameData.textSpeed*2;
            int rand = Random.Range(0, mouthList.Count);
            mouth.sprite = mouthList[rand];
        }
        //animate mouth if I add one
    }

    private void SetPace()
    {
        mouth.sprite = mouthList[0];
        Quaternion target = Quaternion.Euler(0, 90.0f*direction, 0);
        transform.rotation = target;
        paceSet = true;
    }

    private void Pace()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (direction < 0 && transform.position.x < -range) {
            direction = 1;
            SetPace();
        }
        else if (direction > 0 && transform.position.x > range)
        {
            direction = -1;
            SetPace();
        }
    }
}
