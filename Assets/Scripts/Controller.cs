using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private List<Question> questions;

    [SerializeField]
    private ButtonController buttonController;
    [SerializeField]
    private LanguageController languageController;

    [SerializeField]
    private Transform gamePanel, statisticPanel;

    [SerializeField]
    private Text accuracyText, totalAnswerCountText, correctAnswerCountText;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Dropdown languageDropdown;

    private int currentQuestion;
    private int correctAnswers;

    // Start is called before the first frame update
    private void Start()
    {   
        restartButton.onClick.AddListener(RestartGame);
        languageDropdown.onValueChanged.AddListener(SelectLanguage);

        RecreateButtons();
        languageController.SetQuestion(questions[currentQuestion + 1]);
    }

    public void CheckAnswer(int answer)
    {
        if(questions[currentQuestion].right_answer_id == answer)
        {
            correctAnswers++;
        }

        if((currentQuestion + 1) <= questions.Count - 1)
        {
            RecreateButtons();
            languageController.SetQuestion(questions[currentQuestion + 1]);
        }
        else
        {
            gamePanel.gameObject.SetActive(false);
            statisticPanel.gameObject.SetActive(true);

            double accuracy = (double)correctAnswers / questions.Count * 100;

            accuracyText.text = "Accuracy: " + ((int)accuracy).ToString();
            totalAnswerCountText.text = "Total Answer Count: " + questions.Count.ToString();
            correctAnswerCountText.text = "Correct Answer Count: " + correctAnswers.ToString();
        }
        currentQuestion++;
    }

    private void RecreateButtons()
    {
        var currentLanguage = languageController.GetCurrentLanguage();

        buttonController.ClearButtons();
        buttonController.Init(questions[currentQuestion].options, currentLanguage);
    }

    private void RestartGame()
    {
        currentQuestion = 0;
        correctAnswers = 0;

        gamePanel.gameObject.SetActive(true);
        statisticPanel.gameObject.SetActive(false);

        RecreateButtons();
    }

    private void SelectLanguage(int languageCode)
    {
        var language = (Languages)languageCode;
        languageController.ChangeLanguage(language);
    }

    private void OnDestroy() {
        buttonController.ClearButtons();
        restartButton.onClick.RemoveAllListeners();
        languageDropdown.onValueChanged.RemoveAllListeners();
    }

    public void SetData(List<Question> questions)
    {
        this.questions = questions;
    }
}
