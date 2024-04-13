using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//an asset for each question. 
[CreateAssetMenu(fileName = "QAndA", menuName = "ScriptableObjects/QAndA", order = 1)]
public class QAndA : ScriptableObject
{
    [System.Serializable]
    public class Answer
    {
        public string answer;
        public int value; //positive = more likely to be selected/lose
    }

    public string question;
    public List<Answer> answers;
    public int silliness; //scale of 5 to start
}
