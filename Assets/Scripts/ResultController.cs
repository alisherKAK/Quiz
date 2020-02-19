using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private Controller controller;

    [SerializeField]
    private Text accuracyText, totalAnswerCountText, correctAnswerCountText;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Transform gamePanel, statisticPanel;

    private void Start() {
        restartButton.onClick.AddListener(RestartGame);

        gamePanel.gameObject.SetActive(true);
        statisticPanel.gameObject.SetActive(false);
    }

    public void ShowResult(int totalAnswers, int correctAnswers)
    {
        statisticPanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(false);

        double accuracy = (double)correctAnswers / totalAnswers * 100;

        accuracyText.text = "Accuracy: " + ((int)accuracy).ToString();
        totalAnswerCountText.text = "Total Answer Count: " + totalAnswers.ToString();
        correctAnswerCountText.text = "Correct Answer Count: " + correctAnswers.ToString();
    }

    private void RestartGame()
    {
        statisticPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);

        controller.DefaultState();
    }
    
    private void OnDestroy() {
        restartButton.onClick.RemoveAllListeners();
    }
}
