using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private List<Question> questions;

    [SerializeField]
    private ButtonInit firstOption;
    private List<ButtonInit> optionButtons = new List<ButtonInit>();

    [SerializeField]
    private Transform buttonInstanceObject;

    [SerializeField]
    private ResultController resultController;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Dropdown languageDropdown;

    private Languages currentLanguage;
    private int currentQuestion;
    private int correctAnswers;

    // Start is called before the first frame update
    private void Start()
    {   
        languageDropdown.onValueChanged.AddListener(SelectLanguage);
        currentLanguage = Languages.Russian;

        DefaultState();
    }

    public void Answer(int answer)
    {
        if(questions[currentQuestion].right_answer_id == answer)
        {
            correctAnswers++;
        }

        if((currentQuestion + 1) <= questions.Count - 1)
        {
            RecreateButtons();
            var question = questions[currentQuestion];
            switch(currentLanguage)
            {
                case Languages.Russian:
                    questionText.text = question.name_ru;
                    break;
                case Languages.English:
                    questionText.text = question.name_en;
                    break;
                case Languages.Kazakh:
                    questionText.text = question.name_kz;
                    break;
            }
        }
        else
        {
            resultController.ShowResult(questions.Count, correctAnswers);
        }
        currentQuestion++;
    }

    private void RecreateButtons()
    {
        ClearButtons();
        Init();
    }

    public void DefaultState()
    {
        currentQuestion = 0;
        correctAnswers = 0;

        var question = questions[currentQuestion];
        switch(currentLanguage)
        {
            case Languages.Russian:
                questionText.text = question.name_ru;
                break;
            case Languages.English:
                questionText.text = question.name_en;
                break;
            case Languages.Kazakh:
                questionText.text = question.name_kz;
                break;
        }

        RecreateButtons();
    }

    private void SelectLanguage(int languageCode)
    {
        currentLanguage = (Languages)languageCode;

        var question = questions[currentQuestion];
        switch(currentLanguage)
        {
            case Languages.Russian:
                questionText.text = question.name_ru;
                break;
            case Languages.English:
                questionText.text = question.name_en;
                break;
            case Languages.Kazakh:
                questionText.text = question.name_kz;
                break;
        }

        RecreateButtons();
    }

    private void Init()
    {
        foreach(var option in questions[currentQuestion].options)
        {
            var button = Instantiate(firstOption, buttonInstanceObject);
            string optionName = "";
            switch(currentLanguage)
            {
                case Languages.Russian:
                    optionName = option.name_ru;
                    break;
                case Languages.English:
                    optionName = option.name_en;
                    break;
                case Languages.Kazakh:
                    optionName = option.name_kz;
                    break;
            }

            button.Init(option.id, optionName);
            button.onButtonClick += Answer;
            optionButtons.Add(button);
        }
    }

    private void ClearButtons()
    {
        if(optionButtons.Count > 0)
        {
            for(int i = 0; i < optionButtons.Count; i++)
            {
                Destroy(optionButtons[i].gameObject);
            }

            optionButtons.Clear();
        }
    }

    private void OnDestroy() {
        ClearButtons();
        languageDropdown.onValueChanged.RemoveAllListeners();
    }

    public void SetData(List<Question> questions)
    {
        this.questions = questions;
    }
}
