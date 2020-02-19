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
    private int id;

    public void Init(int id, string text){
        this.id = id;
        buttonText.text = text;
        currentButton.onClick.AddListener(Click);
    }

    private void Click(){
        onButtonClick(id);
    }

    private void OnDestroy() {
        currentButton.onClick.RemoveAllListeners();
    }
}
