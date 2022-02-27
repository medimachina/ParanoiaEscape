using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class CardDisplaySmall : SerializedMonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _shortText;
    [SerializeField]
    private RectTransform _baseRectTransform;
    [SerializeField]
    private CanvasGroup _cardLayoutGroup;
    [SerializeField]
    private Card _card;

    public Card Card => _card;

    private void Awake()
    {
    }

    public void SetCard(Card newCard)
    {
        _card = newCard;
        UpdateUiFromCard();
    }

    [Button("Update from card")]
    public void UpdateUiFromCard()
    {
        _shortText.text = _card.Short;
        _shortText.color = _card.Color;
    }

    [Button("Appear")]
    public void Appear()
    {
        _cardLayoutGroup.alpha = 0;
        _cardLayoutGroup.DOFade(1, 1);
    }
}
