using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for character behavior (reactions for now, maybe anim stuff later)
public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField]
    protected GameData gameData;
    [SerializeField]
    protected GameObject happyReaction;
    [SerializeField]
    protected GameObject angryReaction;
    [SerializeField]
    private float reactionHideTime = 2.0f;

    private IEnumerator hideCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void SetReaction()
    {
        if(hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        if (gameData.reactionScore > 0)
        {
            angryReaction.SetActive(false);
            happyReaction.SetActive(true);
            hideCoroutine = HideReaction(happyReaction);
            StartCoroutine(hideCoroutine);
        }
        else
        {
            happyReaction.SetActive(false);
            angryReaction.SetActive(true);
            hideCoroutine = HideReaction(angryReaction);
            StartCoroutine(hideCoroutine);
        }
    }

    protected IEnumerator HideReaction(GameObject reaction)
    {
        yield return new WaitForSeconds(reactionHideTime);
        reaction.SetActive(false);
    }
}
