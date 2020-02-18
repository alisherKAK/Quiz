using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonInit : MonoBehaviour
{
    [SerializeField]
    private Text buttonText;

    [SerializeField]
    private Button currentButton;

    public Action<int> onButtonClick;
    private Option option;

    public void Init(Option option, Languages language){
        this.option = option;
        ChangeLanguage(language);
        currentButton.onClick.AddListener(Click);
    }

    private void Click(){
        onButtonClick(option.id);
    }

    public void ChangeLanguage(Languages language)
    {
        switch(language)
        {
            case Languages.Russian:
                buttonText.text = option.name_ru;
                break;
            case Languages.English:
                buttonText.text = option.name_en;
                break;
            case Languages.Kazakh:
                buttonText.text = option.name_kz;
                break;
        }
    }

    private void OnDestroy() {
        currentButton.onClick.RemoveAllListeners();
    }
}
