using System;
using System.Collections.Generic;

[System.Serializable]
public class UserModel
{
    public List<User> users;
}

[System.Serializable]
public class User
{
    public int id;
    public string nickname;
    public int totalQuestions;
    public int correctQuestions;
    public int accuracy;
    public DateTime date;
}
