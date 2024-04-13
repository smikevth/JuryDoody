using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
[CreateAssetMenu(fileName = "QAndA", menuName = "ScriptableObjects/QAndA", order = 1)]
public class QAndA : ScriptableObject
{
    [System.Serializable]
    public class Answer
    {
        public string answer;
        public int value;
    }

    public string question;
    
    public List<Answer> answers;
}
