using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private List<Question> questions;

    [SerializeField]
    private Button firstOption;

    [SerializeField]
    private Transform gamePanel, statisticPanel;

    [SerializeField]
    private Text questionText, accuracyText, totalAnswerCountText, correctAnswerCountText;

    [SerializeField]
    private Text firstOptionText, secondOptionText, thirdOptionText, fourthOptionText, fifthOptionText;

    private int currentQuestion;
    private int correctAnswers;

    // Start is called before the first frame update
    private void Start()
    {   
        currentQuestion = 0;
        correctAnswers = 0;

        SetOptionsToButtons();

        gamePanel.gameObject.SetActive(true);
        statisticPanel.gameObject.SetActive(false);

        questionText.text = questions[currentQuestion].name_ru;
    }

    private void Answer(int answer)
    {   
        if(questions[currentQuestion].right_answer_id == answer)
        {
            correctAnswers++;
        }

        if((currentQuestion + 1) <= questions.Count - 1)
        {
            questionText.text = questions[currentQuestion + 1].name_ru;
            
            // ClearButtons();

            SetOptionsToButtons();
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
    
    private void SetOptionsToButtons()
    {
        for(int i = 0; i < questions[currentQuestion].options.Count; i++)
        {
            var button = Instantiate(firstOption, firstOption.transform.parent);
            button.GetComponent<ButtonInit>().Init(questions[currentQuestion].options[i].name_ru);
            button.onClick.AddListener(() => Answer(questions[currentQuestion].options[i].id));
        }
    }

    // private void ClearButtons()
    // {
    //     firstOption.onClick.RemoveAllListeners();
    //     secondOption.onClick.RemoveAllListeners();
    //     thirdOption.onClick.RemoveAllListeners();
    //     fourthOption.onClick.RemoveAllListeners();
    //     fifthOption.onClick.RemoveAllListeners();
    // }

    private void OnDestroy() {
        // ClearButtons();
    }

    public void SetData(List<Question> questions)
    {
        this.questions = questions;
    }
}
