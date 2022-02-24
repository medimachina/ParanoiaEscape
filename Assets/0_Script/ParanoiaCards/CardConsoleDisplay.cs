using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CardConsoleDisplay : SerializedMonoBehaviour
{
    [SerializeField]
    private List<Card> _cardListA;
    [SerializeField]
    private List<Card> _cardListB;
    public Card cardX;
    public int multiplier;

    void Start()
    {
        CardDeck deckA = new CardDeck(_cardListA);
        CardDeck deckB = new CardDeck(_cardListB);
        Debug.Log($"DeckA: {deckA}");
        Debug.Log($"DeckB: {deckB}");
        Debug.Log($"DeckA + DeckB: {deckA + deckB}");
        Debug.Log($"DeckA - DeckB: {deckA - deckB}");
        Debug.Log($"DeckA + CardX: {deckA + cardX}");
        Debug.Log($"DeckA - CardX: {deckA - cardX}");
        Debug.Log($"DeckA: {deckA}");
        Debug.Log($"DeckA * {multiplier}: {deckA * multiplier}");
    }

    
}
