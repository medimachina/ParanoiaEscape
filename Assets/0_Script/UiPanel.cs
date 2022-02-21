using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;


public class UiPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _messageBackground;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Button _buttonImage;
    [SerializeField]
    private TextMeshProUGUI _headlineText;
    [SerializeField]
    private TextMeshProUGUI _messageText;
    [SerializeField]
    private TextMeshProUGUI _buttonText;


    [Button("Show")]
    public void Show()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 1);
    }

    [Button("Hide")]
    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(0, 1).onComplete = () =>
        {
            gameObject.SetActive(false);
        };
    }
}
