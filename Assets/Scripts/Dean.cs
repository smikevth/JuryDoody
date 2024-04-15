using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//watches a variable and sets reaction.
public class Dean : BaseCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        gameData.OnDeanChange.AddListener(SetReaction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SetReaction()
    {
        if(!gameData.isGameOver)
        {
            happyReaction.SetActive(true);
            StartCoroutine(HideReaction(happyReaction));
        }
    }
}
