using System.Collections.Generic;

[System.Serializable]
public class QuestionsModel
{
    public List<Question> Questions;
}

[System.Serializable]
public class Question
{
    public int id;
    public string name_ru;
    public string name_en;
    public string name_kz;
    public int right_answer_id;
    public List<Option> options;
}

[System.Serializable]
public class Option
{
    public int id;
    public string name_ru;
    public string name_en;
    public string name_kz;
}

public enum Languages
{
    Russian,
    English, 
    Kazakh
}