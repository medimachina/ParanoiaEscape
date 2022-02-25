using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CardDisplayBig : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private CanvasGroup _cardLayoutGroup;
    [SerializeField]
    private Card _card;

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
        //_nameText.text = "Test";
        //_descriptionText.text = "Bla blabla bla. Bla blabla bla. Bla blabla bla.";
        //_nameText.color = ColorPalette.BlueDark;
    }
}
