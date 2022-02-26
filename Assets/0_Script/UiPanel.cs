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

    private Tween _currentTween;

    [Button("Show")]
    public void Show()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
        }
        if (_canvasGroup.gameObject.activeSelf == true)
        {
            _canvasGroup.alpha = 0;
            _currentTween = _canvasGroup.DOFade(1, 1);
            _button.Select();
        }
    }

    [Button("Hide")]
    public void Hide()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
        }
        if (_canvasGroup.gameObject.activeSelf == true)
        {
            _canvasGroup.alpha = 0;
            _currentTween = _canvasGroup.DOFade(0, 1).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    public void OnDestroy()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
        }
    }
}
