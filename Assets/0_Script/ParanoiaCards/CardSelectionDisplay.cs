using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionDisplay : MonoBehaviour
{
    [SerializeField]
    List<CardDisplayBig> _cardDisplays;

    public void Display(CardDeck deck)
    {

        Debug.Log($"_cardDisplays {_cardDisplays != null}");
        Debug.Log($"deck {deck != null}");
        if (deck != null)
        { 
            Debug.Log($"deck.Count {deck.Count}");
        }
        for (int i = 0; i < _cardDisplays.Count && i < deck.Count; i++)
        {
            _cardDisplays[i].gameObject.SetActive(true);
            _cardDisplays[i].SetCard(deck.Get(i));
            _cardDisplays[i].Appear();
        }
    }
}
