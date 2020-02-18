using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private Controller controller;

    [SerializeField]
    private ButtonInit firstOption;
    private List<ButtonInit> optionButtons = new List<ButtonInit>();

    [SerializeField]
    private Transform buttonInstanceObject;

    public void Init(List<Option> options, Languages language)
    {
        for(int i = 0; i < options.Count; i++)
        {
            var button = Instantiate(firstOption, buttonInstanceObject);
            button.Init(options[i], language);
            button.onButtonClick += Answer;
            optionButtons.Add(button);
        }
    }

    public void ClearButtons()
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

    public void ChangeLanguage(Languages language)
    {
        for(int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].ChangeLanguage(language);
        }
    }

    private void Answer(int answer)
    {
        controller.CheckAnswer(answer);
    }
}
