using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class Model : MonoBehaviour
{
    [SerializeField]
    private Controller controller;

    private List<Question> questions;

    // Start is called before the first frame update
    private void Awake() 
    {
        using(var stream = new StreamReader(Application.streamingAssetsPath + "/Questions.json"))
        {
            string json = stream.ReadToEnd();
            questions = JsonUtility.FromJson<QuestionsModel>(json).Questions;
        }

        controller.SetData(questions);
    }
}
