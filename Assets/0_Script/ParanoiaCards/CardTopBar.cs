using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class CardTopBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform _layoutGroupParent;
    [SerializeField]
    private GameObject _cardPrefab;
    [SerializeField]
    Vector3 _correctionPosition;
    [SerializeField]
    float rightCorrection;

    public bool HasSelectedThisRound = false;

    [SerializeField]
    CardDisplayBig _displayBig1;
    [SerializeField]
    CardDisplayBig _displayBig2;

    private CardDeck _deck;

    private List<CardDisplaySmall> _cardDisplays;

    public CardDeck Deck { get => _deck; }

    public void Awake()
    {
        _cardDisplays = new List<CardDisplaySmall>();
        _deck = new CardDeck();
    }

    public void SetDeck(CardDeck deck)
    {
        _deck = new CardDeck();

        foreach (Card card in deck.CardList)
        {
            AddCardModel(card);
        }
    }

    private void AddCard(Card card)
    {
        _deck.Add(card);
    }

    public CardDisplaySmall AddCardModel(Card card)
    {
        GameObject newObj = Instantiate<GameObject>(_cardPrefab, _layoutGroupParent);
        CardDisplaySmall cardDisplay = newObj.GetComponent<CardDisplaySmall>();
        cardDisplay.SetCard(card);
        AddCard(card);
        _cardDisplays.Add(cardDisplay);
        return cardDisplay;
    }

    [Button("Add Card")]
    public CardDisplaySmall AddAndFadeCardModel(Card card)
    {
        CardDisplaySmall cardDisplay = AddCardModel(card);
        cardDisplay.Appear();
        return cardDisplay;
    }

    [Button("Add Card display")]
    public CardDisplaySmall AddCardDisplayBig(CardDisplayBig sourceDisplay)
    {
        CardDisplaySmall targetDisplay = AddAndFadeCardModel(sourceDisplay.Card);
        sourceDisplay.MoveTo(targetDisplay.transform.position + _correctionPosition + new Vector3(rightCorrection * (_cardDisplays.Count - 1), 0, 0));
        return targetDisplay;
    }

    internal void ActivateParanoias()
    {
        foreach (Card card in _deck.CardList)
        {
            card.Start();
        }
    }

    public void SelectFirstCard()
    {
        if (!HasSelectedThisRound)
        {
            HasSelectedThisRound = true;
            AddCardDisplayBig(_displayBig1);
        }
    }
    public void SelectSecondCard()
    {
        if (!HasSelectedThisRound)
        {
            HasSelectedThisRound = true;
            AddCardDisplayBig(_displayBig2);
        }
    }
}
