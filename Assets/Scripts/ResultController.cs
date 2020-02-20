using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using sharpPDF;
using sharpPDF.Enumerators;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private Controller controller;

    [SerializeField]
    private Text accuracyText, totalAnswerCountText, correctAnswerCountText;

    [SerializeField]
    private Button restartButton, startButton;

    [SerializeField]
    private Transform gamePanel, statisticPanel, startPanel;

    [SerializeField]
    private InputField nicknameInputField;

    private string nickname;
    private int totalAnswers;
    private int correctAnswers;
    private int accuracy;

    private void Start() {
        restartButton.onClick.AddListener(RestartGame);
        startButton.onClick.AddListener(StartGame);

        startPanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(false);
        statisticPanel.gameObject.SetActive(false);
    }

    public void ShowResult(int totalAnswers, int correctAnswers)
    {
        this.totalAnswers = totalAnswers;
        this.correctAnswers = correctAnswers;

        statisticPanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(false);

        double accuracy = (double)correctAnswers / totalAnswers * 100;

        this.accuracy = (int)accuracy;

        accuracyText.text = "Accuracy: " + ((int)accuracy).ToString();
        totalAnswerCountText.text = "Total Answer Count: " + totalAnswers.ToString();
        correctAnswerCountText.text = "Correct Answer Count: " + correctAnswers.ToString();

        SaveResultsToPdf();
        SaveRateResults();
    }

    private void RestartGame()
    {
        statisticPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(true);

        controller.DefaultState();
    }
    
    private void StartGame()
    {
        nickname = nicknameInputField.text;

        nicknameInputField.text = "";

        startPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);
    }

    private void SaveRateResults()
    {
        string path = Application.streamingAssetsPath + "/Rate.txt";
        string newJsonData = "";
        if(!File.Exists(path))
        {
            using(var stream = new StreamWriter(path))
            {
                var newUser = new User()
                {
                    id = 0,
                    nickname = nickname,
                    totalQuestions = totalAnswers,
                    correctQuestions = correctAnswers,
                    accuracy = this.accuracy,
                    date = DateTime.Now
                };

                var userModel = new UserModel();
                userModel.users = new List<User>();
                userModel.users.Add(newUser);

                newJsonData = JsonUtility.ToJson(userModel);

                stream.Write(newJsonData);
            }
        }

        using(var streamReader = new StreamReader(path))
        {
            string jsonData = streamReader.ReadToEnd();
            var userModel = JsonUtility.FromJson<UserModel>(jsonData);
            int lastIndex = userModel.users.Count - 1;
            
            if(userModel.users.Count < 5)
            {
                var newUser = new User()
                {
                    id = userModel.users.Count,
                    nickname = nickname,
                    totalQuestions = totalAnswers,
                    correctQuestions = correctAnswers,
                    accuracy = accuracy,
                    date = DateTime.Now
                };

                userModel.users.Add(newUser);
            }
            else if(userModel.users[lastIndex].accuracy < accuracy)
            {
                userModel.users[lastIndex].accuracy = accuracy;
                userModel.users[lastIndex].totalQuestions = totalAnswers;
                userModel.users[lastIndex].correctQuestions = correctAnswers;
                userModel.users[lastIndex].date = DateTime.Now;
                userModel.users[lastIndex].nickname = nickname;
            }

            SortRate(userModel.users);

            newJsonData = JsonUtility.ToJson(userModel);
        }

        using(var streamWriter = new StreamWriter(path))
        {
            streamWriter.Write(newJsonData);
        }
    }

    private void SortRate(List<User> users)
    {
        User bufUser;
        for(int i = 0; i < users.Count; i++)
        {
            for(int j = i; j < users.Count; j++)
            {
                if(users[i].accuracy < users[j].accuracy)
                {
                    bufUser = users[i];
                    users[i] = users[j];
                    users[j] = bufUser;
                }
            }
        }
    }

    private void SaveResultsToPdf()
    {
        var path = Application.streamingAssetsPath + "/Results.pdf";
        string nickname = $"Nickname: {this.nickname}\n";
        string totalAnswers = $"Total Answers: {this.totalAnswers}\n";
        string correctAnswers = $"Correct Answers: {this.correctAnswers}\n";
        string accuracy = $"Accuracy: {this.accuracy}%";
        string date = "Date: " + DateTime.Now.ToString("dd/MM/yyyy");

        pdfDocument newDocument = new pdfDocument("Results", nickname);
        pdfPage newPage = newDocument.addPage();
        newPage.addText(nickname, 10, 750, predefinedFont.csHelveticaOblique, 14);
        newPage.addText(totalAnswers, 10, 730, predefinedFont.csHelveticaOblique, 14);
        newPage.addText(correctAnswers, 10, 710, predefinedFont.csHelveticaOblique, 14);
        newPage.addText(accuracy, 10, 690, predefinedFont.csHelveticaOblique, 14);
        newPage.addText(date, 10, 670, predefinedFont.csHelveticaOblique, 14);

        newDocument.createPDF(path);
    }

    private void OnDestroy() {
        restartButton.onClick.RemoveAllListeners();
        startButton.onClick.RemoveAllListeners();
    }
}
