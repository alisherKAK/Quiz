using UnityEngine;
using UnityEngine.UI;

public class ButtonInit : MonoBehaviour
{
    [SerializeField]
    private Text buttonText;

    public void Init(string text)
    {
        buttonText.text = text;
    }
}
