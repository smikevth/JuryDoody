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
        if (gameData.reactionScore > 0)
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

    protected IEnumerator HideReaction(GameObject reaction)
    {
        yield return new WaitForSeconds(reactionHideTime);
        reaction.SetActive(false);
    }
}
