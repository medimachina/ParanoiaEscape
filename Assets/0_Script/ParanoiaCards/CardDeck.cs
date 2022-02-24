using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CardDeck
{
    [SerializeField]
    private List<Card> _cardList;

    public List<Card> CardList
    {
        get => _cardList;
        set
        {
            _cardList = new List<Card>(value);
        }
    }

    public CardDeck()
    {
        _cardList = new List<Card>();
    }

    public CardDeck(List<Card> cards)
    {
        _cardList = new List<Card>(cards);
    }

    public static CardDeck operator +(CardDeck a, CardDeck b)
    {
        List<Card> mergedCards = new List<Card>(a.CardList);
        mergedCards.AddRange(b.CardList);
        return new CardDeck(mergedCards);
    }

    public static CardDeck operator +(CardDeck a, Card b)
    {
        List<Card> mergedCards = new List<Card>(a.CardList);
        mergedCards.Add(b);
        return new CardDeck(mergedCards);
    }

    private Dictionary<string, List<int>> GetIdToCardIndexedDict()
    {
        Dictionary<string, List<int>> idToIndex = new Dictionary<string, List<int>>();

        for (int i = 0; i < _cardList.Count; i++)
        {
            string identifier = _cardList[i].Identifier;

            if (!idToIndex.ContainsKey(identifier))
            {
                idToIndex.Add(identifier, new List<int>());
            }

            idToIndex[identifier].Add(i);
        }

        return idToIndex;
    }

    public static CardDeck operator -(CardDeck a, CardDeck b)
    {
        List<Card> cardsLeft = new List<Card>(a.CardList);
        Dictionary<string, List<int>> idToIndex = a.GetIdToCardIndexedDict();
        List<int> indicesToRemove = new List<int>();

        foreach (Card card in b.CardList)
        {
            if (idToIndex.ContainsKey(card.Identifier))
            {
                List<int> indexList = idToIndex[card.Identifier];
                if (indexList.Count > 0)
                {
                    indicesToRemove.Add(indexList[indexList.Count - 1]);
                    indexList.RemoveAt(indexList.Count - 1);
                }
            }
        }

        indicesToRemove.Sort();
        indicesToRemove.Reverse();

        foreach (int index in indicesToRemove)
        {
            cardsLeft.RemoveAt(index);
        }

        return new CardDeck(cardsLeft);
    }

    public static CardDeck operator -(CardDeck a, Card b)
    {
        List<Card> cardsLeft = new List<Card>(a.CardList);

        for (int i = a.CardList.Count - 1; i >= 0; i--)
        {
            if (b.Identifier == cardsLeft[i].Identifier)
            {
                cardsLeft.RemoveAt(i);
                break;
            }
        }

        return new CardDeck(cardsLeft);
    }

    public static CardDeck operator *(CardDeck a, int b)
    {
        List<Card> resultingList = new List<Card>();
        
        for (int i = 0; i < b; i++)
        {
            foreach (Card card in a.CardList)
            {
                resultingList.Add(card);
            }
        }

        return new CardDeck(resultingList);
    }

    public override string ToString()
    {
        string temp = "(";

        foreach (Card card in _cardList)
        {
            temp += card.ToString() + ", ";
        }

        temp += ")";
        return temp;
        //return "(" + String.Join(", ", _cardList) + ")";
    }

}
