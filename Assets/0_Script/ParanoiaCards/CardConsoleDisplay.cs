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

    private void DebugList(List<Card> list)
    {
        string output = "";
        foreach (Card card in list)
        {
            output += card.Identifier + ", ";
        }

        Debug.Log(output);
    }

    [Button("Debug A")]
    public void DebugListA()
    {
        DebugList(_cardListA);
    }
    [Button("Debug B")]
    public void DebugListB()
    {
        DebugList(_cardListA);
    }
    [Button("Debug X")]
    public void DebugCardX()
    {
        Debug.Log(cardX.Identifier);
    }
    [Button("Debug X type")]
    public void DebugCardXType()
    {
        Debug.Log(cardX.GetType());
    }

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
        Debug.Log($"DeckA.Shuffle(): {deckA.Shuffle()}");
        CardDeck deckC = deckA.Shuffle() + deckB.Shuffle();
        Debug.Log($"DeckC: {deckC}");
        Debug.Log($"Draw card from C: {deckC.DrawCard()}");
        Debug.Log($"Draw 3 cards from C: {deckC.DrawCards(3)}");
        Debug.Log($"DeckC: {deckC}");
        Debug.Log($"Remove all CantWalkRed: {deckC.RemoveAllCardsOfType<CantWalkRedCard>()}");

    }


}
