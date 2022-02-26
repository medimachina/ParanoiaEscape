using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionDisplay : MonoBehaviour
{
    List<CardDisplayBig> _cardDisplays;

    public void Display(CardDeck deck)
    {
        for (int i = 0; i < _cardDisplays.Count && i < deck.Count; i++)
        {
            _cardDisplays[i].SetCard(deck.Get(i));
            _cardDisplays[i].Appear();
        }
    }
}
