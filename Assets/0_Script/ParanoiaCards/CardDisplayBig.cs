using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class CardDisplayBig : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private RectTransform _baseRectTransform;
    [SerializeField]
    private CanvasGroup _cardLayoutGroup;
    [SerializeField]
    private Card _card;

    private TransformResetter transformResetter;

    private void Awake()
    {
        transformResetter = new TransformResetter(_baseRectTransform);
    }

    public void SetCard(Card newCard)
    {
        _card = newCard;
        UpdateUiFromCard();
    }

    [Button("Update from card")]
    public void UpdateUiFromCard()
    {
        Debug.Log($"Name: \"{_card.Name}\", Descr: \"{_card.Description}\"");
        _nameText.text = _card.Name;
        _descriptionText.text = _card.Description;
        _nameText.color = _card.Color;
    }

    [Button("Appear")]
    public void Appear()
    {
        transformResetter.Reset(_baseRectTransform);
        _baseRectTransform.DOScale(0, 1).From();
        _baseRectTransform.DORotate(new Vector3(0,0,90), 1).From();
        _baseRectTransform.DOLocalMove(new Vector3(-1000, 100, 0), 1).From();
    }
}
