using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    private Languages currentLanguage;

    [SerializeField]
    private ButtonController buttonController;

    [SerializeField]
    private Text questionText;

    private Question question;

    private void Start() {
        currentLanguage = Languages.Russian;
    }

    private void ChangeButtonsLanguage()
    {
        buttonController.ChangeLanguage(currentLanguage);
    }

    private void ChangeQuestionTextLanguage()
    {
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

    public void ChangeLanguage(Languages language)
    {
        currentLanguage = language;
        ChangeButtonsLanguage();
        ChangeQuestionTextLanguage();
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        ChangeQuestionTextLanguage();
    }

    public Languages GetCurrentLanguage()
    {
        return currentLanguage;
    }
}
